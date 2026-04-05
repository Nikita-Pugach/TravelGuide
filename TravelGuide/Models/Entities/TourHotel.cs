namespace TravelGuide.Models.Entities;

/// <summary>
/// Связь тура и отеля (many-to-many)
/// </summary>
public class TourHotel
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
    /// Идентификатор отеля
    /// </summary>
    public int HotelId { get; set; }
    
    /// <summary>
    /// Отель
    /// </summary>
    public virtual Hotel? Hotel { get; set; }
    
    /// <summary>
    /// День маршрута, в который происходит размещение
    /// </summary>
    public int? DayNumber { get; set; }
}