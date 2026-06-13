using Microsoft.EntityFrameworkCore;
using TravelGuide.Data;
using TravelGuide.Models.Entities;
using TravelGuide.Services;
using Xunit;

namespace TravelGuide.Tests;

/// <summary>
/// Unit-тесты для AccountService
/// </summary>
public class AccountServiceTests
{
    private TravelGuideContext GetInMemoryContext()
    {
        var options = new DbContextOptionsBuilder<TravelGuideContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
        return new TravelGuideContext(options);
    }

    [Fact]
    public async Task RegisterAsync_ValidUser_ReturnsTrue()
    {
        // Arrange
        await using var context = GetInMemoryContext();
        var service = new AccountService(context);
        var user = new User
        {
            Email = "test@test.com",
            FullName = "Test User",
            Phone = "+1234567890"
        };

        // Act
        var result = await service.RegisterAsync(user, "password123");

        // Assert
        Assert.True(result);
        Assert.NotNull(user.PasswordHash);
        Assert.Equal(UserRole.Tourist, user.Role);
    }

    [Fact]
    public async Task RegisterAsync_DuplicateEmail_ReturnsFalse()
    {
        // Arrange
        await using var context = GetInMemoryContext();
        var service = new AccountService(context);
        
        var user1 = new User { Email = "same@test.com", FullName = "User 1" };
        await service.RegisterAsync(user1, "pass1");
        
        var user2 = new User { Email = "same@test.com", FullName = "User 2" };

        // Act
        var result = await service.RegisterAsync(user2, "pass2");

        // Assert
        Assert.False(result);
    }

    [Fact]
    public async Task AuthenticateAsync_ValidCredentials_ReturnsUser()
    {
        // Arrange
        await using var context = GetInMemoryContext();
        var service = new AccountService(context);
        
        var user = new User { Email = "auth@test.com", FullName = "Auth User" };
        await service.RegisterAsync(user, "correctpassword");

        // Act
        var result = await service.AuthenticateAsync("auth@test.com", "correctpassword");

        // Assert
        Assert.NotNull(result);
        Assert.Equal("auth@test.com", result.Email);
    }

    [Fact]
    public async Task AuthenticateAsync_WrongPassword_ReturnsNull()
    {
        // Arrange
        await using var context = GetInMemoryContext();
        var service = new AccountService(context);
        
        var user = new User { Email = "wrong@test.com", FullName = "Wrong User" };
        await service.RegisterAsync(user, "correctpassword");

        // Act
        var result = await service.AuthenticateAsync("wrong@test.com", "wrongpassword");

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task UserExistsAsync_ExistingUser_ReturnsTrue()
    {
        // Arrange
        await using var context = GetInMemoryContext();
        var service = new AccountService(context);
        
        var user = new User { Email = "exists@test.com", FullName = "Exists User" };
        await service.RegisterAsync(user, "password");

        // Act
        var result = await service.UserExistsAsync("exists@test.com");

        // Assert
        Assert.True(result);
    }

    [Fact]
    public async Task UserExistsAsync_NonExistingUser_ReturnsFalse()
    {
        // Arrange
        await using var context = GetInMemoryContext();
        var service = new AccountService(context);

        // Act
        var result = await service.UserExistsAsync("notexists@test.com");

        // Assert
        Assert.False(result);
    }

    [Fact]
    public async Task UpdatePasswordAsync_ChangesPassword()
    {
        // Arrange
        await using var context = GetInMemoryContext();
        var service = new AccountService(context);
        
        var user = new User { Email = "update@test.com", FullName = "Update User" };
        await service.RegisterAsync(user, "oldpassword");
        var oldHash = user.PasswordHash;

        // Act
        await service.UpdatePasswordAsync(user.Id, "newpassword");

        // Assert
        var updatedUser = await context.Users.FindAsync(user.Id);
        Assert.NotNull(updatedUser);
        Assert.NotEqual(oldHash, updatedUser.PasswordHash);
    }

    [Fact]
    public async Task UpdateProfileAsync_UpdatesUserFields()
    {
        // Arrange
        await using var context = GetInMemoryContext();
        var service = new AccountService(context);
        
        var user = new User { Email = "profile@test.com", FullName = "Old Name", Phone = "111" };
        await service.RegisterAsync(user, "password");

        // Act
        var updateData = new User { Id = user.Id, FullName = "New Name", Phone = "222" };
        var result = await service.UpdateProfileAsync(updateData);

        // Assert
        Assert.True(result);
        var updatedUser = await context.Users.FindAsync(user.Id);
        Assert.NotNull(updatedUser);
        Assert.Equal("New Name", updatedUser.FullName);
        Assert.Equal("222", updatedUser.Phone);
    }

    [Fact]
    public async Task AuthenticateAsync_BlockedUser_ReturnsNull()
    {
        // Arrange
        await using var context = GetInMemoryContext();
        var service = new AccountService(context);
        
        var user = new User { Email = "blocked@test.com", FullName = "Blocked User", IsBlocked = true };
        user.PasswordHash = BCrypt.Net.BCrypt.HashPassword("password");
        context.Users.Add(user);
        await context.SaveChangesAsync();

        // Act
        var result = await service.AuthenticateAsync("blocked@test.com", "password");

        // Assert
        Assert.Null(result);
    }
}