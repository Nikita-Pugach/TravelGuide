using System.ComponentModel.DataAnnotations;
using TravelGuide.Models.Entities;

namespace TravelGuide.ViewModels;

/// <summary>
/// ViewModel для создания бронирования
/// </summary>
public class BookingCreateViewModel
{
    public int TourId { get; set; }
    public string TourName { get; set; } = string.Empty;
    public decimal TourPrice { get; set; }
    public int Duration { get; set; }
    public string? TourPhotoUrl { get; set; }
    public string CountryName { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "Укажите дату тура")]
    [Display(Name = "Желаемая дата тура")]
    [DataType(DataType.Date)]
    public DateTime BookingDate { get; set; } = DateTime.Today.AddDays(7);
    
    [Required(ErrorMessage = "Укажите количество туристов")]
    [Range(1, 100, ErrorMessage = "Количество туристов должно быть от 1 до 100")]
    [Display(Name = "Количество туристов")]
    public int GuestsCount { get; set; } = 1;
    
    [Required(ErrorMessage = "Укажите контактный телефон")]
    [Phone(ErrorMessage = "Некорректный формат телефона")]
    [Display(Name = "Контактный телефон")]
    [StringLength(20, ErrorMessage = "Телефон не может превышать 20 символов")]
    public string Phone { get; set; } = string.Empty;
    
    [EmailAddress(ErrorMessage = "Некорректный формат email")]
    [Display(Name = "Email")]
    [StringLength(100, ErrorMessage = "Email не может превышать 100 символов")]
    public string? Email { get; set; }
    
    [Display(Name = "Комментарий к заявке")]
    [StringLength(500, ErrorMessage = "Комментарий не может превышать 500 символов")]
    public string? Notes { get; set; }
    
    public decimal TotalPrice => TourPrice * GuestsCount;
}

/// <summary>
/// ViewModel для списка бронирований пользователя
/// </summary>
public class BookingListViewModel
{
    public int Id { get; set; }
    public int TourId { get; set; }
    public string TourName { get; set; } = string.Empty;
    public string? TourPhotoUrl { get; set; }
    public string CountryName { get; set; } = string.Empty;
    public DateTime BookingDate { get; set; }
    public int GuestsCount { get; set; }
    public decimal TotalPrice { get; set; }
    public BookingStatus Status { get; set; }
    public DateTime CreatedAt { get; set; }
    
    public string StatusClass => Status switch
    {
        BookingStatus.New => "warning",
        BookingStatus.Confirmed => "success",
        BookingStatus.Cancelled => "danger",
        _ => "secondary"
    };
    
    public string StatusText => Status switch
    {
        BookingStatus.New => "Новая заявка",
        BookingStatus.Confirmed => "Подтверждена",
        BookingStatus.Cancelled => "Отменена",
        _ => Status.ToString()
    };
}

/// <summary>
/// ViewModel для управления бронированием (менеджер/админ)
/// </summary>
public class BookingManageViewModel
{
    public int Id { get; set; }
    
    // Информация о туре
    public int TourId { get; set; }
    public string TourName { get; set; } = string.Empty;
    public string? TourPhotoUrl { get; set; }
    public string CountryName { get; set; } = string.Empty;
    public int Duration { get; set; }
    public decimal TourPrice { get; set; }
    
    // Информация о пользователе
    public int UserId { get; set; }
    public string UserFullName { get; set; } = string.Empty;
    public string UserEmail { get; set; } = string.Empty;
    public string UserPhone { get; set; } = string.Empty;
    
    // Данные бронирования
    public DateTime BookingDate { get; set; }
    public int GuestsCount { get; set; }
    public decimal TotalPrice { get; set; }
    public BookingStatus Status { get; set; }
    public string? Notes { get; set; }
    public DateTime CreatedAt { get; set; }
    
    // Обработка
    public string? ProcessedByUserName { get; set; }
    public DateTime? ProcessedAt { get; set; }
    public string? ManagerNotes { get; set; }
    
    // Для изменения статуса
    [StringLength(500, ErrorMessage = "Комментарий не может превышать 500 символов")]
    public string? NewManagerNotes { get; set; }
    
    public string StatusClass => Status switch
    {
        BookingStatus.New => "warning",
        BookingStatus.Confirmed => "success",
        BookingStatus.Cancelled => "danger",
        _ => "secondary"
    };
    
    public string StatusText => Status switch
    {
        BookingStatus.New => "Новая заявка",
        BookingStatus.Confirmed => "Подтверждена",
        BookingStatus.Cancelled => "Отменена",
        _ => Status.ToString()
    };
}