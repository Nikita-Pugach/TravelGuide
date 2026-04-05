namespace TravelGuide.Models.Entities;

/// <summary>
/// Связь тура и достопримечательности (many-to-many)
/// </summary>
public class TourSight
{
    /// <summary>
    /// Идентификатор тура
    /// </summary>
    public int TourId { get; set; }
    
    /// <summary>
    /// Тур
    /// </summary>
    public virtual Tour? Tour { get; set; }
    
    /// <summary>
    /// Идентификатор достопримечательности
    /// </summary>
    public int SightId { get; set; }
    
    /// <summary>
    /// Достопримечательность
    /// </summary>
    public virtual Sight? Sight { get; set; }
    
    /// <summary>
    /// День посещения
    /// </summary>
    public int? DayNumber { get; set; }
}