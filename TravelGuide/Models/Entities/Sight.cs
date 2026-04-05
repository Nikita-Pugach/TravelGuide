using System.ComponentModel.DataAnnotations;

namespace TravelGuide.Models.Entities;

/// <summary>
/// Достопримечательность
/// </summary>
public class Sight : BaseEntity
{
    /// <summary>
    /// Название достопримечательности
    /// </summary>
    [Required(ErrorMessage = "Название достопримечательности обязательно")]
    [StringLength(200, ErrorMessage = "Название не может превышать 200 символов")]
    public string Name { get; set; } = string.Empty;
    
    /// <summary>
    /// Идентификатор города
    /// </summary>
    [Display(Name = "Город")]
    public int CityId { get; set; }
    
    /// <summary>
    /// Город
    /// </summary>
    public virtual City? City { get; set; }
    
    /// <summary>
    /// Тип достопримечательности
    /// </summary>
    [Display(Name = "Тип")]
    public SightType Type { get; set; }
    
    /// <summary>
    /// Описание
    /// </summary>
    [Display(Name = "Описание")]
    public string? Description { get; set; }
    
    /// <summary>
    /// Адрес
    /// </summary>
    [Display(Name = "Адрес")]
    public string? Address { get; set; }
    
    /// <summary>
    /// URL главного изображения
    /// </summary>
    [Display(Name = "Изображение")]
    public string? PhotoUrl { get; set; }
    
    /// <summary>
    /// Широта
    /// </summary>
    [Display(Name = "Широта")]
    public double? Latitude { get; set; }
    
    /// <summary>
    /// Долгота
    /// </summary>
    [Display(Name = "Долгота")]
    public double? Longitude { get; set; }
    
    /// <summary>
    /// Связь с турами
    /// </summary>
    public virtual ICollection<TourSight> TourSights { get; set; } = new List<TourSight>();
    
    /// <summary>
    /// Отзывы о достопримечательности
    /// </summary>
    public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();
}