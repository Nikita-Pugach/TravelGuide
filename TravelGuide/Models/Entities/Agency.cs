using System.ComponentModel.DataAnnotations;

namespace TravelGuide.Models.Entities;

/// <summary>
/// Туристическое агентство
/// </summary>
public class Agency : BaseEntity
{
    /// <summary>
    /// Название агентства
    /// </summary>
    [Required(ErrorMessage = "Название агентства обязательно")]
    [StringLength(200, ErrorMessage = "Название не может превышать 200 символов")]
    public string Name { get; set; } = string.Empty;
    
    /// <summary>
    /// Контактная информация
    /// </summary>
    [Required(ErrorMessage = "Контактная информация обязательна")]
    [StringLength(500, ErrorMessage = "Контактная информация не может превышать 500 символов")]
    [Display(Name = "Контактная информация")]
    public string ContactInfo { get; set; } = string.Empty;
    
    /// <summary>
    /// Описание агентства
    /// </summary>
    [Display(Name = "Описание")]
    public string? Description { get; set; }
    
    /// <summary>
    /// Рейтинг (средняя оценка)
    /// </summary>
    [Display(Name = "Рейтинг")]
    public double? Rating { get; set; }
    
    /// <summary>
    /// URL логотипа
    /// </summary>
    [Display(Name = "Логотип")]
    public string? LogoUrl { get; set; }
    
    /// <summary>
    /// Туры агентства
    /// </summary>
    public virtual ICollection<Tour> Tours { get; set; } = new List<Tour>();
    
    /// <summary>
    /// Менеджеры агентства
    /// </summary>
    public virtual ICollection<User> Managers { get; set; } = new List<User>();
}