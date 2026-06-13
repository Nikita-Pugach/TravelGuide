using System.ComponentModel.DataAnnotations;

namespace TravelGuide.Models.Entities;

/// <summary>
/// Бронирование тура
/// </summary>
public class Booking : BaseEntity
{
    /// <summary>
    /// Идентификатор тура
    /// </summary>
    [Display(Name = "Тур")]
    public int TourId { get; set; }
    
    /// <summary>
    /// Тур
    /// </summary>
    public virtual Tour? Tour { get; set; }
    
    /// <summary>
    /// Идентификатор пользователя
    /// </summary>
    [Display(Name = "Турист")]
    public int UserId { get; set; }
    
    /// <summary>
    /// Пользователь
    /// </summary>
    public virtual User? User { get; set; }
    
    /// <summary>
    /// Желаемая дата тура
    /// </summary>
    [Required(ErrorMessage = "Укажите дату тура")]
    [Display(Name = "Дата тура")]
    [DataType(DataType.Date)]
    public DateTime BookingDate { get; set; }
    
    /// <summary>
    /// Количество туристов
    /// </summary>
    [Required(ErrorMessage = "Укажите количество туристов")]
    [Range(1, 100, ErrorMessage = "Количество туристов должно быть от 1 до 100")]
    [Display(Name = "Количество туристов")]
    public int GuestsCount { get; set; }
    
    /// <summary>
    /// Итоговая цена
    /// </summary>
    [Display(Name = "Итого")]
    public decimal TotalPrice { get; set; }
    
    /// <summary>
    /// Статус бронирования
    /// </summary>
    [Display(Name = "Статус")]
    public BookingStatus Status { get; set; } = BookingStatus.New;
    
    /// <summary>
    /// Контактный телефон
    /// </summary>
    [Required(ErrorMessage = "Укажите контактный телефон")]
    [Phone(ErrorMessage = "Некорректный формат телефона")]
    [Display(Name = "Телефон")]
    [StringLength(20, ErrorMessage = "Телефон не может превышать 20 символов")]
    public string Phone { get; set; } = string.Empty;
    
    /// <summary>
    /// Email для связи
    /// </summary>
    [EmailAddress(ErrorMessage = "Некорректный формат email")]
    [Display(Name = "Email")]
    [StringLength(100, ErrorMessage = "Email не может превышать 100 символов")]
    public string? Email { get; set; }
    
    /// <summary>
    /// Комментарий туриста
    /// </summary>
    [Display(Name = "Комментарий")]
    [StringLength(500, ErrorMessage = "Комментарий не может превышать 500 символов")]
    public string? Notes { get; set; }
    
    /// <summary>
    /// Дата создания заявки
    /// </summary>
    [Display(Name = "Дата заявки")]
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    
    /// <summary>
    /// Идентификатор пользователя, обработавшего заявку
    /// </summary>
    public int? ProcessedByUserId { get; set; }
    
    /// <summary>
    /// Пользователь, обработавший заявку
    /// </summary>
    public virtual User? ProcessedByUser { get; set; }
    
    /// <summary>
    /// Дата обработки заявки
    /// </summary>
    [Display(Name = "Дата обработки")]
    public DateTime? ProcessedAt { get; set; }
    
    /// <summary>
    /// Комментарий менеджера при обработке
    /// </summary>
    [Display(Name = "Ответ менеджера")]
    [StringLength(500, ErrorMessage = "Комментарий не может превышать 500 символов")]
    public string? ManagerNotes { get; set; }
}