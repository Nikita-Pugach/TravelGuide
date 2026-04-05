using System.ComponentModel.DataAnnotations;

namespace TravelGuide.Models.Entities;

/// <summary>
/// Отель
/// </summary>
public class Hotel : BaseEntity
{
    /// <summary>
    /// Название отеля
    /// </summary>
    [Required(ErrorMessage = "Название отеля обязательно")]
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
    /// Звёздность (1-5)
    /// </summary>
    [Range(1, 5, ErrorMessage = "Звёздность должна быть от 1 до 5")]
    [Display(Name = "Звёздность")]
    public int Stars { get; set; }
    
    /// <summary>
    /// Тип питания
    /// </summary>
    [Display(Name = "Тип питания")]
    public MealType MealType { get; set; }
    
    /// <summary>
    /// Расстояние до пляжа (метры)
    /// </summary>
    [Display(Name = "Расстояние до пляжа (м)")]
    public int? DistanceToBeach { get; set; }
    
    /// <summary>
    /// Расстояние до центра (км)
    /// </summary>
    [Display(Name = "Расстояние до центра (км)")]
    public int? DistanceToCenter { get; set; }
    
    /// <summary>
    /// Описание удобств
    /// </summary>
    [Display(Name = "Описание")]
    public string? Description { get; set; }
    
    /// <summary>
    /// Цена за ночь (в рублях)
    /// </summary>
    [Display(Name = "Цена за ночь")]
    public decimal? PricePerNight { get; set; }
    
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
    public virtual ICollection<TourHotel> TourHotels { get; set; } = new List<TourHotel>();
    
    /// <summary>
    /// Отзывы об отеле
    /// </summary>
    public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();
}