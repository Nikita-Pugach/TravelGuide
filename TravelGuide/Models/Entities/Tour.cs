using System.ComponentModel.DataAnnotations;

namespace TravelGuide.Models.Entities;

/// <summary>
/// Тур
/// </summary>
public class Tour : BaseEntity
{
    /// <summary>
    /// Название тура
    /// </summary>
    [Required(ErrorMessage = "Название тура обязательно")]
    [StringLength(200, ErrorMessage = "Название не может превышать 200 символов")]
    public string Name { get; set; } = string.Empty;
    
    /// <summary>
    /// Тип тура
    /// </summary>
    [Display(Name = "Тип тура")]
    public TourType TourType { get; set; }
    
    /// <summary>
    /// Описание тура
    /// </summary>
    [Display(Name = "Описание")]
    public string? Description { get; set; }
    
    /// <summary>
    /// Продолжительность (дней)
    /// </summary>
    [Required(ErrorMessage = "Продолжительность обязательна")]
    [Range(1, 365, ErrorMessage = "Продолжительность должна быть от 1 до 365 дней")]
    [Display(Name = "Продолжительность (дней)")]
    public int Duration { get; set; }
    
    /// <summary>
    /// Базовая цена на одного человека
    /// </summary>
    [Required(ErrorMessage = "Цена обязательна")]
    [Range(0, 10000000, ErrorMessage = "Цена должна быть положительным числом")]
    [Display(Name = "Цена")]
    public decimal Price { get; set; }
    
    /// <summary>
    /// Идентификатор страны
    /// </summary>
    [Display(Name = "Страна")]
    public int CountryId { get; set; }
    
    /// <summary>
    /// Страна
    /// </summary>
    public virtual Country? Country { get; set; }
    
    /// <summary>
    /// Идентификатор агентства
    /// </summary>
    [Display(Name = "Агентство")]
    public int AgencyId { get; set; }
    
    /// <summary>
    /// Агентство
    /// </summary>
    public virtual Agency? Agency { get; set; }
    
    /// <summary>
    /// Маршрут по дням (текстовое описание)
    /// </summary>
    [Display(Name = "Маршрут")]
    public string? Route { get; set; }
    
    /// <summary>
    /// Дата начала (если фиксированная)
    /// </summary>
    [Display(Name = "Дата начала")]
    public DateTime? StartDate { get; set; }
    
    /// <summary>
    /// Дата окончания (если фиксированная)
    /// </summary>
    [Display(Name = "Дата окончания")]
    public DateTime? EndDate { get; set; }
    
    /// <summary>
    /// Признак сезонности
    /// </summary>
    [Display(Name = "Сезонный тур")]
    public bool IsSeasonal { get; set; }
    
    /// <summary>
    /// Месяцы сезонности (через запятую, например "6,7,8")
    /// </summary>
    public string? SeasonalMonths { get; set; }
    
    /// <summary>
    /// URL главного изображения
    /// </summary>
    [Display(Name = "Изображение")]
    public string? PhotoUrl { get; set; }
    
    /// <summary>
    /// Количество просмотров
    /// </summary>
    [Display(Name = "Просмотров")]
    public int ViewCount { get; set; }
    
    /// <summary>
    /// Средняя оценка
    /// </summary>
    [Display(Name = "Рейтинг")]
    public double? Rating { get; set; }
    
    /// <summary>
    /// Дата создания
    /// </summary>
    [Display(Name = "Дата создания")]
    public DateTime CreatedDate { get; set; } = DateTime.Now;
    
    /// <summary>
    /// Отели в туре
    /// </summary>
    public virtual ICollection<TourHotel> TourHotels { get; set; } = new List<TourHotel>();
    
    /// <summary>
    /// Достопримечательности в туре
    /// </summary>
    public virtual ICollection<TourSight> TourSights { get; set; } = new List<TourSight>();
    
    /// <summary>
    /// Отзывы о туре
    /// </summary>
    public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();
    
    /// <summary>
    /// Избранное у пользователей
    /// </summary>
    public virtual ICollection<FavoriteTour> Favorites { get; set; } = new List<FavoriteTour>();
}