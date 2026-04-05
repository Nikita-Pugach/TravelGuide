using System.ComponentModel.DataAnnotations;

namespace TravelGuide.Models.Entities;

/// <summary>
/// Страна
/// </summary>
public class Country : BaseEntity
{
    /// <summary>
    /// Название страны
    /// </summary>
    [Required(ErrorMessage = "Название страны обязательно")]
    [StringLength(100, ErrorMessage = "Название не может превышать 100 символов")]
    public string Name { get; set; } = string.Empty;
    
    /// <summary>
    /// Краткое описание (визовые требования, климат)
    /// </summary>
    [Display(Name = "Описание")]
    public string? Description { get; set; }
    
    /// <summary>
    /// URL изображения (флаг или фото)
    /// </summary>
    [Display(Name = "Изображение")]
    public string? PhotoUrl { get; set; }
    
    /// <summary>
    /// Коллекция городов
    /// </summary>
    public virtual ICollection<City> Cities { get; set; } = new List<City>();
    
    /// <summary>
    /// Коллекция туров
    /// </summary>
    public virtual ICollection<Tour> Tours { get; set; } = new List<Tour>();
    
    /// <summary>
    /// Возвращает эмодзи флага страны
    /// </summary>
    public string GetFlag()
    {
        return Name?.ToLower() switch
        {
            var n when n.Contains("россия") || n.Contains("russia") => "🇷🇺",
            var n when n.Contains("турция") || n.Contains("turkey") => "🇹🇷",
            var n when n.Contains("египет") || n.Contains("egypt") => "🇪🇬",
            var n when n.Contains("тайланд") || n.Contains("таиланд") || n.Contains("thailand") => "🇹🇭",
            var n when n.Contains("оаэ") || n.Contains("эмират") => "🇦🇪",
            var n when n.Contains("греция") || n.Contains("greece") => "🇬🇷",
            var n when n.Contains("испания") || n.Contains("spain") => "🇪🇸",
            var n when n.Contains("италия") || n.Contains("italy") => "🇮🇹",
            var n when n.Contains("франция") || n.Contains("france") => "🇫🇷",
            var n when n.Contains("германия") || n.Contains("germany") => "🇩🇪",
            var n when n.Contains("кипр") || n.Contains("cyprus") => "🇨🇾",
            var n when n.Contains("черногория") || n.Contains("montenegro") => "🇲🇪",
            var n when n.Contains("хорватия") || n.Contains("croatia") => "🇭🇷",
            var n when n.Contains("тунис") || n.Contains("tunisia") => "🇹🇳",
            var n when n.Contains("вьетнам") || n.Contains("vietnam") => "🇻🇳",
            var n when n.Contains("индия") || n.Contains("india") => "🇮🇳",
            var n when n.Contains("шри-ланка") || n.Contains("sri lanka") => "🇱🇰",
            var n when n.Contains("мальдивы") || n.Contains("maldives") => "🇲🇻",
            var n when n.Contains("куба") || n.Contains("cuba") => "🇨🇺",
            var n when n.Contains("доминикана") || n.Contains("dominican") => "🇩🇴",
            var n when n.Contains("мексика") || n.Contains("mexico") => "🇲🇽",
            var n when n.Contains("сша") || n.Contains("america") => "🇺🇸",
            var n when n.Contains("япония") || n.Contains("japan") => "🇯🇵",
            var n when n.Contains("китай") || n.Contains("china") => "🇨🇳",
            _ => "🌍"
        };
    }
}
