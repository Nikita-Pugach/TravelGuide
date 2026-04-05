using System.ComponentModel.DataAnnotations;

namespace TravelGuide.Models.Entities;

/// <summary>
/// Сообщение в чате
/// </summary>
public class Message : BaseEntity
{
    /// <summary>
    /// Идентификатор чата
    /// </summary>
    [Display(Name = "Чат")]
    public int ChatId { get; set; }
    
    /// <summary>
    /// Чат
    /// </summary>
    public virtual Chat? Chat { get; set; }
    
    /// <summary>
    /// Идентификатор отправителя
    /// </summary>
    [Display(Name = "Отправитель")]
    public int SenderId { get; set; }
    
    /// <summary>
    /// Отправитель
    /// </summary>
    public virtual User? Sender { get; set; }
    
    /// <summary>
    /// Текст сообщения
    /// </summary>
    [Required(ErrorMessage = "Сообщение не может быть пустым")]
    [StringLength(5000, ErrorMessage = "Сообщение не может превышать 5000 символов")]
    [Display(Name = "Сообщение")]
    public string Text { get; set; } = string.Empty;
    
    /// <summary>
    /// Время отправки
    /// </summary>
    [Display(Name = "Время")]
    public DateTime Timestamp { get; set; } = DateTime.Now;
    
    /// <summary>
    /// Признак прочтения
    /// </summary>
    [Display(Name = "Прочитано")]
    public bool IsRead { get; set; }
}