using Microsoft.EntityFrameworkCore;
using TravelGuide.Data;
using TravelGuide.Models.Entities;
using TravelGuide.Services.Interfaces;

namespace TravelGuide.Services;

/// <summary>
/// Сервис авторизации пользователей
/// </summary>
public class AccountService : IAccountService
{
    private readonly TravelGuideContext _context;

    public AccountService(TravelGuideContext context)
    {
        _context = context;
    }

    public async Task<User?> AuthenticateAsync(string email, string password)
    {
        var user = await _context.Users
            .Include(u => u.Agency)
            .FirstOrDefaultAsync(u => u.Email == email && !u.IsBlocked);

        if (user == null)
            return null;

        if (!BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
            return null;

        return user;
    }

    public async Task<bool> RegisterAsync(User user, string password)
    {
        if (await UserExistsAsync(user.Email))
            return false;

        user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(password);
        user.RegistrationDate = DateTime.Now;
        user.Role = UserRole.Tourist;

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> UserExistsAsync(string email)
    {
        return await _context.Users.AnyAsync(u => u.Email == email);
    }

    public async Task<User?> GetCurrentUserAsync(int userId)
    {
        return await _context.Users
            .Include(u => u.Agency)
            .FirstOrDefaultAsync(u => u.Id == userId);
    }

    public async Task UpdatePasswordAsync(int userId, string newPassword)
    {
        var user = await _context.Users.FindAsync(userId);
        if (user != null)
        {
            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(newPassword);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<bool> UpdateProfileAsync(User user)
    {
        var existingUser = await _context.Users.FindAsync(user.Id);
        if (existingUser == null)
            return false;

        existingUser.FullName = user.FullName;
        existingUser.Phone = user.Phone;

        await _context.SaveChangesAsync();
        return true;
    }
}
