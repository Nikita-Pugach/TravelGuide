namespace TravelGuide.Models.Entities;

/// <summary>
/// Роли пользователей в системе
/// </summary>
public enum UserRole
{
    /// <summary>
    /// Гость - незарегистрированный пользователь (для совместимости)
    /// </summary>
    Guest = 0,
    
    /// <summary>
    /// Турист - зарегистрированный пользователь
    /// </summary>
    Tourist = 1,
    
    /// <summary>
    /// Менеджер агентства
    /// </summary>
    Manager = 2,
    
    /// <summary>
    /// Администратор платформы
    /// </summary>
    Admin = 3
}

/// <summary>
/// Тип тура
/// </summary>
public enum TourType
{
    Excursion = 1,      // Экскурсионный
    Beach = 2,          // Пляжный
    Combined = 3,       // Комбинированный
    Bus = 4,            // Автобусный
    Cruise = 5,         // Круиз
    Ski = 6,            // Лыжный
    Health = 7,         // Санаторно-курортный
    Adventure = 8       // Приключенческий
}

/// <summary>
/// Тип питания в отеле
/// </summary>
public enum MealType
{
    RO = 0,     // Без питания
    BB = 1,     // Только завтрак
    HB = 2,     // Завтрак + ужин
    FB = 3,     // Полный пансион
    AI = 4,     // Всё включено
    UAI = 5     // Ультра всё включено
}

/// <summary>
/// Статус отзыва
/// </summary>
public enum ReviewStatus
{
    Pending = 0,    // На модерации
    Approved = 1,   // Опубликован
    Rejected = 2    // Отклонён
}

/// <summary>
/// Статус чат-сессии
/// </summary>
public enum ChatStatus
{
    Active = 0,     // Активен
    Closed = 1      // Закрыт
}

/// <summary>
/// Тип достопримечательности
/// </summary>
public enum SightType
{
    Museum = 1,         // Музей
    Monument = 2,       // Памятник
    Park = 3,           // Парк
    Cathedral = 4,      // Собор/Церковь
    Historical = 5,     // Историческое место
    Nature = 6,         // Природная достопримечательность
    Entertainment = 7   // Развлекательный объект
}

/// <summary>
/// Методы расширения для перечислений
/// </summary>
public static class EnumExtensions
{
    public static string GetDisplayName(this TourType type) => type switch
    {
        TourType.Excursion => "Экскурсионный",
        TourType.Beach => "Пляжный",
        TourType.Combined => "Комбинированный",
        TourType.Bus => "Автобусный",
        TourType.Cruise => "Круиз",
        TourType.Ski => "Лыжный",
        TourType.Health => "Оздоровительный",
        TourType.Adventure => "Приключенческий",
        _ => type.ToString()
    };

    public static string GetIcon(this TourType type) => type switch
    {
        TourType.Excursion => "🏛️",
        TourType.Beach => "🏖️",
        TourType.Combined => "🎯",
        TourType.Bus => "🚌",
        TourType.Cruise => "🚢",
        TourType.Ski => "⛷️",
        TourType.Health => "🏥",
        TourType.Adventure => "⛰️",
        _ => "📍"
    };

    public static string GetDisplayName(this SightType type) => type switch
    {
        SightType.Museum => "Музей",
        SightType.Monument => "Памятник",
        SightType.Park => "Парк",
        SightType.Cathedral => "Собор/Церковь",
        SightType.Historical => "Историческое место",
        SightType.Nature => "Природа",
        SightType.Entertainment => "Развлечения",
        _ => type.ToString()
    };

    public static string GetIcon(this SightType type) => type switch
    {
        SightType.Museum => "🏛️",
        SightType.Monument => "🗿",
        SightType.Park => "🌳",
        SightType.Cathedral => "⛪",
        SightType.Historical => "🏰",
        SightType.Nature => "🏔️",
        SightType.Entertainment => "🎢",
        _ => "📍"
    };

    public static string GetDisplayName(this MealType type) => type switch
    {
        MealType.RO => "Без питания",
        MealType.BB => "Завтрак",
        MealType.HB => "Завтрак + ужин",
        MealType.FB => "Полный пансион",
        MealType.AI => "Всё включено",
        MealType.UAI => "Ультра всё включено",
        _ => type.ToString()
    };
}
