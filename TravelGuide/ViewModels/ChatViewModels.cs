using System.ComponentModel.DataAnnotations;
using TravelGuide.Models.Entities;

namespace TravelGuide.ViewModels;

/// <summary>
/// Модель для списка чатов
/// </summary>
public class ChatListViewModel
{
    public List<ChatItemViewModel> Chats { get; set; } = new();
    public int UnreadCount { get; set; }
}

/// <summary>
/// Модель элемента чата в списке
/// </summary>
public class ChatItemViewModel
{
    public int ChatId { get; set; }
    public string UserName { get; set; } = string.Empty;
    public string? UserAvatar { get; set; }
    public string LastMessage { get; set; } = string.Empty;
    public DateTime LastMessageTime { get; set; }
    public int UnreadCount { get; set; }
    public ChatStatus Status { get; set; }
    public bool HasManager { get; set; }
}

/// <summary>
/// Модель для переписки
/// </summary>
public class ChatConversationViewModel
{
    public int ChatId { get; set; }
    public ChatStatus Status { get; set; }
    public string UserName { get; set; } = string.Empty;
    public string? UserAvatar { get; set; }
    public int UserId { get; set; }
    public string? ManagerName { get; set; }
    public int? ManagerId { get; set; }
    public List<MessageViewModel> Messages { get; set; } = new();
    public SendMessageViewModel NewMessage { get; set; } = new();
}

/// <summary>
/// Модель сообщения
/// </summary>
public class MessageViewModel
{
    public int MessageId { get; set; }
    public string SenderName { get; set; } = string.Empty;
    public string? SenderAvatar { get; set; }
    public string Text { get; set; } = string.Empty;
    public DateTime Timestamp { get; set; }
    public bool IsRead { get; set; }
    public bool IsMyMessage { get; set; }
}

/// <summary>
/// Модель для отправки сообщения
/// </summary>
public class SendMessageViewModel
{
    public int ChatId { get; set; }
    
    [Required(ErrorMessage = "Сообщение не может быть пустым")]
    [StringLength(5000, ErrorMessage = "Сообщение не может превышать 5000 символов")]
    [Display(Name = "Сообщение")]
    public string Text { get; set; } = string.Empty;
}

/// <summary>
/// Модель для создания нового чата
/// </summary>
public class CreateChatViewModel
{
    [Display(Name = "Первое сообщение")]
    [Required(ErrorMessage = "Напишите первое сообщение")]
    [StringLength(5000, ErrorMessage = "Сообщение не может превышать 5000 символов")]
    public string FirstMessage { get; set; } = string.Empty;
}