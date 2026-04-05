using System.ComponentModel.DataAnnotations;

namespace TravelGuide.Models.Entities;

/// <summary>
/// Чат-сессия
/// </summary>
public class Chat : BaseEntity
{
    /// <summary>
    /// Идентификатор пользователя
    /// </summary>
    [Display(Name = "Пользователь")]
    public int UserId { get; set; }
    
    /// <summary>
    /// Пользователь
    /// </summary>
    public virtual User? User { get; set; }
    
    /// <summary>
    /// Идентификатор менеджера (может быть null до назначения)
    /// </summary>
    [Display(Name = "Менеджер")]
    public int? ManagerId { get; set; }
    
    /// <summary>
    /// Менеджер
    /// </summary>
    public virtual User? Manager { get; set; }
    
    /// <summary>
    /// Время начала чата
    /// </summary>
    [Display(Name = "Время начала")]
    public DateTime StartTime { get; set; } = DateTime.Now;
    
    /// <summary>
    /// Время завершения чата
    /// </summary>
    [Display(Name = "Время окончания")]
    public DateTime? EndTime { get; set; }
    
    /// <summary>
    /// Статус чата
    /// </summary>
    [Display(Name = "Статус")]
    public ChatStatus Status { get; set; } = ChatStatus.Active;
    
    /// <summary>
    /// Сообщения в чате
    /// </summary>
    public virtual ICollection<Message> Messages { get; set; } = new List<Message>();
}