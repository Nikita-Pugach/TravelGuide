using System.ComponentModel.DataAnnotations;

namespace TravelGuide.Models.Entities;

/// <summary>
/// Отзыв
/// </summary>
public class Review : BaseEntity
{
    /// <summary>
    /// Идентификатор автора
    /// </summary>
    [Display(Name = "Автор")]
    public int UserId { get; set; }
    
    /// <summary>
    /// Автор отзыва
    /// </summary>
    public virtual User? User { get; set; }
    
    /// <summary>
    /// Идентификатор тура (может быть null)
    /// </summary>
    public int? TourId { get; set; }
    
    /// <summary>
    /// Тур
    /// </summary>
    public virtual Tour? Tour { get; set; }
    
    /// <summary>
    /// Идентификатор отеля (может быть null)
    /// </summary>
    public int? HotelId { get; set; }
    
    /// <summary>
    /// Отель
    /// </summary>
    public virtual Hotel? Hotel { get; set; }
    
    /// <summary>
    /// Идентификатор достопримечательности (может быть null)
    /// </summary>
    public int? SightId { get; set; }
    
    /// <summary>
    /// Достопримечательность
    /// </summary>
    public virtual Sight? Sight { get; set; }
    
    /// <summary>
    /// Оценка (1-5)
    /// </summary>
    [Required(ErrorMessage = "Оценка обязательна")]
    [Range(1, 5, ErrorMessage = "Оценка должна быть от 1 до 5")]
    [Display(Name = "Оценка")]
    public int Rating { get; set; }
    
    /// <summary>
    /// Текст отзыва
    /// </summary>
    [Required(ErrorMessage = "Текст отзыва обязателен")]
    [StringLength(5000, ErrorMessage = "Текст не может превышать 5000 символов")]
    [Display(Name = "Текст отзыва")]
    public string Text { get; set; } = string.Empty;
    
    /// <summary>
    /// Дата написания
    /// </summary>
    [Display(Name = "Дата")]
    public DateTime Date { get; set; } = DateTime.Now;
    
    /// <summary>
    /// Псевдоним для совместимости с View
    /// </summary>
    public DateTime CreatedAt => Date;
    
    /// <summary>
    /// Статус модерации
    /// </summary>
    [Display(Name = "Статус")]
    public ReviewStatus Status { get; set; } = ReviewStatus.Pending;
    
    /// <summary>
    /// URL фотографии
    /// </summary>
    [Display(Name = "Фотография")]
    public string? PhotoUrl { get; set; }
}