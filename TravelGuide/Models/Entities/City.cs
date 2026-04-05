using System.ComponentModel.DataAnnotations;

namespace TravelGuide.Models.Entities;

/// <summary>
/// Город
/// </summary>
public class City : BaseEntity
{
    /// <summary>
    /// Название города
    /// </summary>
    [Required(ErrorMessage = "Название города обязательно")]
    [StringLength(100, ErrorMessage = "Название не может превышать 100 символов")]
    public string Name { get; set; } = string.Empty;
    
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
    /// Коллекция отелей
    /// </summary>
    public virtual ICollection<Hotel> Hotels { get; set; } = new List<Hotel>();
    
    /// <summary>
    /// Коллекция достопримечательностей
    /// </summary>
    public virtual ICollection<Sight> Sights { get; set; } = new List<Sight>();
}