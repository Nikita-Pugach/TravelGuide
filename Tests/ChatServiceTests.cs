using Microsoft.EntityFrameworkCore;
using TravelGuide.Data;
using TravelGuide.Models.Entities;
using TravelGuide.Services;
using Xunit;

namespace TravelGuide.Tests;

/// <summary>
/// Unit-тесты для ChatService
/// </summary>
public class ChatServiceTests
{
    private TravelGuideContext GetInMemoryContext()
    {
        var options = new DbContextOptionsBuilder<TravelGuideContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
        return new TravelGuideContext(options);
    }

    private async Task<(User user, User manager, ChatService service)> SeedChatData(TravelGuideContext context)
    {
        var user = new User { Email = "tourist@test.com", FullName = "Турист Туристов", Role = UserRole.Tourist, RegistrationDate = DateTime.Now };
        var manager = new User { Email = "manager@test.com", FullName = "Менеджер Менеджеров", Role = UserRole.Manager, RegistrationDate = DateTime.Now };
        context.Users.AddRange(user, manager);
        await context.SaveChangesAsync();

        var service = new ChatService(context);
        return (user, manager, service);
    }

    [Fact]
    public async Task CreateChatAsync_CreatesChatWithFirstMessage()
    {
        await using var context = GetInMemoryContext();
        var (user, _, service) = await SeedChatData(context);

        var chat = await service.CreateChatAsync(user.Id, "Здравствуйте, нужна помощь");

        Assert.NotNull(chat);
        Assert.Equal(user.Id, chat.UserId);
        Assert.Equal(ChatStatus.Active, chat.Status);
        Assert.True(chat.Id > 0);

        // Проверяем, что первое сообщение создалось
        var messages = await context.Messages.Where(m => m.ChatId == chat.Id).ToListAsync();
        Assert.Single(messages);
        Assert.Equal("Здравствуйте, нужна помощь", messages[0].Text);
        Assert.Equal(user.Id, messages[0].SenderId);
    }

    [Fact]
    public async Task SendMessageAsync_ValidChat_ReturnsTrue()
    {
        await using var context = GetInMemoryContext();
        var (user, _, service) = await SeedChatData(context);

        var chat = await service.CreateChatAsync(user.Id, "Привет");
        var result = await service.SendMessageAsync(chat.Id, user.Id, "Ещё вопрос");

        Assert.True(result);
        var messages = await context.Messages.Where(m => m.ChatId == chat.Id).ToListAsync();
        Assert.Equal(2, messages.Count);
    }

    [Fact]
    public async Task SendMessageAsync_ClosedChat_ReturnsFalse()
    {
        await using var context = GetInMemoryContext();
        var (user, _, service) = await SeedChatData(context);

        var chat = await service.CreateChatAsync(user.Id, "Привет");
        await service.CloseChatAsync(chat.Id, user.Id, UserRole.Tourist);

        var result = await service.SendMessageAsync(chat.Id, user.Id, "Ещё сообщение");

        Assert.False(result);
    }

    [Fact]
    public async Task SendMessageAsync_NonExistentChat_ReturnsFalse()
    {
        await using var context = GetInMemoryContext();
        var (_, _, service) = await SeedChatData(context);

        var result = await service.SendMessageAsync(999, 1, "Текст");

        Assert.False(result);
    }

    [Fact]
    public async Task CloseChatAsync_OwnerCanClose()
    {
        await using var context = GetInMemoryContext();
        var (user, _, service) = await SeedChatData(context);

        var chat = await service.CreateChatAsync(user.Id, "Привет");
        var result = await service.CloseChatAsync(chat.Id, user.Id, UserRole.Tourist);

        Assert.True(result);
        var closedChat = await context.Chats.FindAsync(chat.Id);
        Assert.Equal(ChatStatus.Closed, closedChat!.Status);
        Assert.NotNull(closedChat.EndTime);
    }

    [Fact]
    public async Task CloseChatAsync_TouristCannotCloseOthersChat()
    {
        await using var context = GetInMemoryContext();
        var (user, manager, service) = await SeedChatData(context);

        var chat = await service.CreateChatAsync(user.Id, "Привет");
        // Менеджер пытается закрыть чат туриста
        var result = await service.CloseChatAsync(chat.Id, manager.Id, UserRole.Tourist);

        Assert.False(result);
    }

    [Fact]
    public async Task CloseChatAsync_ManagerCanCloseAnyChat()
    {
        await using var context = GetInMemoryContext();
        var (user, manager, service) = await SeedChatData(context);

        var chat = await service.CreateChatAsync(user.Id, "Привет");
        var result = await service.CloseChatAsync(chat.Id, manager.Id, UserRole.Manager);

        Assert.True(result);
    }

    [Fact]
    public async Task GetChatAsync_TouristCanSeeOwnChat()
    {
        await using var context = GetInMemoryContext();
        var (user, _, service) = await SeedChatData(context);

        var chat = await service.CreateChatAsync(user.Id, "Привет");
        var result = await service.GetChatAsync(chat.Id, user.Id, UserRole.Tourist);

        Assert.NotNull(result);
        Assert.Equal(chat.Id, result.ChatId);
        Assert.Single(result.Messages);
    }

    [Fact]
    public async Task GetChatAsync_TouristCannotSeeOthersChat()
    {
        await using var context = GetInMemoryContext();
        var (user, manager, service) = await SeedChatData(context);

        var chat = await service.CreateChatAsync(user.Id, "Привет");
        // Другой пользователь не должен видеть этот чат
        var result = await service.GetChatAsync(chat.Id, manager.Id, UserRole.Tourist);

        Assert.Null(result);
    }

    [Fact]
    public async Task GetChatAsync_ManagerAutoAssigned()
    {
        await using var context = GetInMemoryContext();
        var (user, manager, service) = await SeedChatData(context);

        var chat = await service.CreateChatAsync(user.Id, "Привет");
        var result = await service.GetChatAsync(chat.Id, manager.Id, UserRole.Manager);

        Assert.NotNull(result);
        Assert.Equal(manager.Id, result.ManagerId);
    }

    [Fact]
    public async Task GetChatAsync_NonExistentChat_ReturnsNull()
    {
        await using var context = GetInMemoryContext();
        var (_, _, service) = await SeedChatData(context);

        var result = await service.GetChatAsync(999, 1, UserRole.Manager);

        Assert.Null(result);
    }

    [Fact]
    public async Task GetUserChatsAsync_TouristSeesOnlyOwnChats()
    {
        await using var context = GetInMemoryContext();
        var (user, _, service) = await SeedChatData(context);

        // Создаём 2 чата от одного пользователя
        await service.CreateChatAsync(user.Id, "Чат 1");
        await service.CreateChatAsync(user.Id, "Чат 2");

        var result = await service.GetUserChatsAsync(user.Id, UserRole.Tourist);

        Assert.Equal(2, result.Count);
    }

    [Fact]
    public async Task GetUserChatsAsync_ManagerSeesAllChats()
    {
        await using var context = GetInMemoryContext();
        var (user, manager, service) = await SeedChatData(context);

        await service.CreateChatAsync(user.Id, "Чат 1");
        await service.CreateChatAsync(user.Id, "Чат 2");

        var result = await service.GetUserChatsAsync(manager.Id, UserRole.Manager);

        Assert.Equal(2, result.Count);
    }

    [Fact]
    public async Task GetUnreadCountAsync_ReturnsCorrectCount()
    {
        await using var context = GetInMemoryContext();
        var (user, manager, service) = await SeedChatData(context);

        var chat = await service.CreateChatAsync(user.Id, "Привет");
        // Сообщение от менеджера (непрочитанное для туриста)
        await service.SendMessageAsync(chat.Id, manager.Id, "Ответ менеджера");

        var unreadForTourist = await service.GetUnreadCountAsync(user.Id, UserRole.Tourist);

        Assert.Equal(1, unreadForTourist);
    }

    [Fact]
    public async Task MarkAsReadAsync_MarksMessagesAsRead()
    {
        await using var context = GetInMemoryContext();
        var (user, manager, service) = await SeedChatData(context);

        var chat = await service.CreateChatAsync(user.Id, "Привет");
        await service.SendMessageAsync(chat.Id, manager.Id, "Ответ");

        await service.MarkAsReadAsync(chat.Id, user.Id);

        var messages = await context.Messages.Where(m => m.ChatId == chat.Id && m.SenderId == manager.Id).ToListAsync();
        Assert.All(messages, m => Assert.True(m.IsRead));
    }

    [Fact]
    public async Task GetChatAsync_NullChatId_ReturnsNull()
    {
        await using var context = GetInMemoryContext();
        var (_, _, service) = await SeedChatData(context);

        var result = await service.GetChatAsync(0, 1, UserRole.Tourist);

        Assert.Null(result);
    }

    [Fact]
    public async Task SendMessageAsync_EmptyText_StillSends()
    {
        await using var context = GetInMemoryContext();
        var (user, _, service) = await SeedChatData(context);

        var chat = await service.CreateChatAsync(user.Id, "Привет");
        var result = await service.SendMessageAsync(chat.Id, user.Id, "");

        Assert.True(result);
        var messages = await context.Messages.Where(m => m.ChatId == chat.Id).ToListAsync();
        Assert.Equal(2, messages.Count);
        Assert.Equal("", messages[1].Text);
    }
}
