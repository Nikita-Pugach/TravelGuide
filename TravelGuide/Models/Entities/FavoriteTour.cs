namespace TravelGuide.Models.Entities;

/// <summary>
/// Избранный тур пользователя
/// </summary>
public class FavoriteTour
{
    /// <summary>
    /// Идентификатор пользователя
    /// </summary>
    public int UserId { get; set; }
    
    /// <summary>
    /// Пользователь
    /// </summary>
    public virtual User? User { get; set; }
    
    /// <summary>
    /// Идентификатор тура
    /// </summary>
    public int TourId { get; set; }
    
    /// <summary>
    /// Тур
    /// </summary>
    public virtual Tour? Tour { get; set; }
    
    /// <summary>
    /// Дата добавления в избранное
    /// </summary>
    public DateTime AddedDate { get; set; } = DateTime.Now;
}