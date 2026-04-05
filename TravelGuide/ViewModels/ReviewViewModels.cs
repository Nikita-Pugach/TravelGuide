using System.ComponentModel.DataAnnotations;

namespace TravelGuide.ViewModels;

/// <summary>
/// Модель создания отзыва
/// </summary>
public class CreateReviewViewModel
{
    /// <summary>
    /// ID тура (если отзыв о туре)
    /// </summary>
    public int? TourId { get; set; }
    
    /// <summary>
    /// ID отеля (если отзыв об отеле)
    /// </summary>
    public int? HotelId { get; set; }
    
    /// <summary>
    /// ID достопримечательности (если отзыв о достопримечательности)
    /// </summary>
    public int? SightId { get; set; }
    
    /// <summary>
    /// Оценка от 1 до 5
    /// </summary>
    [Required(ErrorMessage = "Поставьте оценку")]
    [Range(1, 5, ErrorMessage = "Оценка должна быть от 1 до 5")]
    [Display(Name = "Оценка")]
    public int Rating { get; set; } = 5;
    
    /// <summary>
    /// Текст отзыва
    /// </summary>
    [Required(ErrorMessage = "Напишите отзыв")]
    [StringLength(5000, MinimumLength = 10, ErrorMessage = "Отзыв должен быть от 10 до 5000 символов")]
    [Display(Name = "Ваш отзыв")]
    public string Text { get; set; } = string.Empty;
}

/// <summary>
/// Модель отображения отзыва
/// </summary>
public class ReviewViewModel
{
    public int Id { get; set; }
    public string UserName { get; set; } = string.Empty;
    public int Rating { get; set; }
    public string Text { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public string? PhotoUrl { get; set; }
}