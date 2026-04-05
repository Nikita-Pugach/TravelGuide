using TravelGuide.Data;
using TravelGuide.Models.Entities;
using TravelGuide.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace TravelGuide.Services;

public interface IChatService
{
    Task<Chat> CreateChatAsync(int userId, string firstMessage);
    Task<List<ChatItemViewModel>> GetUserChatsAsync(int userId, UserRole role);
    Task<ChatConversationViewModel?> GetChatAsync(int chatId, int currentUserId, UserRole role);
    Task<bool> SendMessageAsync(int chatId, int senderId, string text);
    Task<bool> CloseChatAsync(int chatId, int userId, UserRole role);
    Task<int> GetUnreadCountAsync(int userId, UserRole role);
    Task MarkAsReadAsync(int chatId, int userId);
}

public class ChatService : IChatService
{
    private readonly TravelGuideContext _context;

    public ChatService(TravelGuideContext context)
    {
        _context = context;
    }

    public async Task<Chat> CreateChatAsync(int userId, string firstMessage)
    {
        var chat = new Chat
        {
            UserId = userId,
            StartTime = DateTime.Now,
            Status = ChatStatus.Active
        };

        _context.Chats.Add(chat);
        await _context.SaveChangesAsync();

        var message = new Message
        {
            ChatId = chat.Id,
            SenderId = userId,
            Text = firstMessage,
            Timestamp = DateTime.Now,
            IsRead = false
        };

        _context.Messages.Add(message);
        await _context.SaveChangesAsync();

        return chat;
    }

    public async Task<List<ChatItemViewModel>> GetUserChatsAsync(int userId, UserRole role)
    {
        var query = _context.Chats
            .Include(c => c.User)
            .Include(c => c.Manager)
            .Include(c => c.Messages)
            .AsQueryable();

        // Турист видит только свои чаты
        if (role == UserRole.Tourist)
        {
            query = query.Where(c => c.UserId == userId);
        }
        // Менеджер и админ видят все чаты (или те, где они назначены)

        var chats = await query
            .OrderByDescending(c => c.Messages.Any() ? c.Messages.Max(m => m.Timestamp) : c.StartTime)
            .ToListAsync();

        var result = chats.Select(c =>
        {
            var lastMessage = c.Messages.OrderByDescending(m => m.Timestamp).FirstOrDefault();
            var unreadCount = role == UserRole.Tourist
                ? c.Messages.Count(m => !m.IsRead && m.SenderId != userId)
                : c.Messages.Count(m => !m.IsRead && m.SenderId != userId && (c.ManagerId == null || c.ManagerId == userId));

            return new ChatItemViewModel
            {
                ChatId = c.Id,
                UserName = c.User?.FullName ?? "Пользователь",
                UserAvatar = c.User?.AvatarUrl,
                LastMessage = lastMessage?.Text ?? "Нет сообщений",
                LastMessageTime = lastMessage?.Timestamp ?? c.StartTime,
                UnreadCount = unreadCount,
                Status = c.Status,
                HasManager = c.ManagerId.HasValue
            };
        }).ToList();

        return result;
    }

    public async Task<ChatConversationViewModel?> GetChatAsync(int chatId, int currentUserId, UserRole role)
    {
        var chat = await _context.Chats
            .Include(c => c.User)
            .Include(c => c.Manager)
            .Include(c => c.Messages)
                .ThenInclude(m => m.Sender)
            .FirstOrDefaultAsync(c => c.Id == chatId);

        if (chat == null) return null;

        // Проверка доступа
        if (role == UserRole.Tourist && chat.UserId != currentUserId)
            return null;

        // Менеджер назначается автоматически при входе в чат
        if (role == UserRole.Manager && chat.ManagerId == null)
        {
            chat.ManagerId = currentUserId;
            await _context.SaveChangesAsync();
        }

        // Отмечаем сообщения как прочитанные
        await MarkAsReadAsync(chatId, currentUserId);

        var messages = chat.Messages
            .OrderBy(m => m.Timestamp)
            .Select(m => new MessageViewModel
            {
                MessageId = m.Id,
                SenderName = m.Sender?.FullName ?? "Пользователь",
                SenderAvatar = m.Sender?.AvatarUrl,
                Text = m.Text,
                Timestamp = m.Timestamp,
                IsRead = m.IsRead,
                IsMyMessage = m.SenderId == currentUserId
            }).ToList();

        return new ChatConversationViewModel
        {
            ChatId = chat.Id,
            Status = chat.Status,
            UserName = chat.User?.FullName ?? "Пользователь",
            UserAvatar = chat.User?.AvatarUrl,
            UserId = chat.UserId,
            ManagerName = chat.Manager?.FullName,
            ManagerId = chat.ManagerId,
            Messages = messages
        };
    }

    public async Task<bool> SendMessageAsync(int chatId, int senderId, string text)
    {
        var chat = await _context.Chats.FindAsync(chatId);
        if (chat == null || chat.Status == ChatStatus.Closed)
            return false;

        var message = new Message
        {
            ChatId = chatId,
            SenderId = senderId,
            Text = text,
            Timestamp = DateTime.Now,
            IsRead = false
        };

        _context.Messages.Add(message);
        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> CloseChatAsync(int chatId, int userId, UserRole role)
    {
        var chat = await _context.Chats.FindAsync(chatId);
        if (chat == null) return false;

        // Проверка прав
        if (role == UserRole.Tourist && chat.UserId != userId)
            return false;

        chat.Status = ChatStatus.Closed;
        chat.EndTime = DateTime.Now;

        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<int> GetUnreadCountAsync(int userId, UserRole role)
    {
        var query = _context.Messages
            .Include(m => m.Chat)
            .Where(m => !m.IsRead && m.SenderId != userId);

        if (role == UserRole.Tourist)
        {
            query = query.Where(m => m.Chat!.UserId == userId);
        }
        else
        {
            query = query.Where(m => m.Chat!.ManagerId == null || m.Chat.ManagerId == userId);
        }

        return await query.CountAsync();
    }

    public async Task MarkAsReadAsync(int chatId, int userId)
    {
        var messages = await _context.Messages
            .Where(m => m.ChatId == chatId && !m.IsRead && m.SenderId != userId)
            .ToListAsync();

        foreach (var message in messages)
        {
            message.IsRead = true;
        }

        await _context.SaveChangesAsync();
    }
}