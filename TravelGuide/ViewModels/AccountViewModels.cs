using System.ComponentModel.DataAnnotations;

namespace TravelGuide.ViewModels;

/// <summary>
/// Модель входа
/// </summary>
public class LoginViewModel
{
    [Required(ErrorMessage = "Введите email")]
    [EmailAddress(ErrorMessage = "Некорректный email")]
    [Display(Name = "Email")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Введите пароль")]
    [DataType(DataType.Password)]
    [Display(Name = "Пароль")]
    public string Password { get; set; } = string.Empty;

    [Display(Name = "Запомнить меня")]
    public bool RememberMe { get; set; }
}

/// <summary>
/// Модель регистрации
/// </summary>
public class RegisterViewModel
{
    [Required(ErrorMessage = "Введите ФИО")]
    [StringLength(200, ErrorMessage = "ФИО не может превышать 200 символов")]
    [Display(Name = "ФИО")]
    public string FullName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Введите email")]
    [EmailAddress(ErrorMessage = "Некорректный email")]
    [Display(Name = "Email")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Введите пароль")]
    [StringLength(100, MinimumLength = 6, ErrorMessage = "Пароль должен быть от 6 до 100 символов")]
    [DataType(DataType.Password)]
    [Display(Name = "Пароль")]
    public string Password { get; set; } = string.Empty;

    [DataType(DataType.Password)]
    [Compare("Password", ErrorMessage = "Пароли не совпадают")]
    [Display(Name = "Подтвердите пароль")]
    public string ConfirmPassword { get; set; } = string.Empty;

    [Phone(ErrorMessage = "Некорректный номер телефона")]
    [Display(Name = "Телефон")]
    public string? Phone { get; set; }
}

/// <summary>
/// Модель профиля пользователя
/// </summary>
public class ProfileViewModel
{
    public int Id { get; set; }
    
    [Display(Name = "ФИО")]
    public string FullName { get; set; } = string.Empty;
    
    [Display(Name = "Email")]
    public string Email { get; set; } = string.Empty;
    
    [Display(Name = "Телефон")]
    public string? Phone { get; set; }
    
    [Display(Name = "Роль")]
    public string Role { get; set; } = string.Empty;
    
    [Display(Name = "Агентство")]
    public string? AgencyName { get; set; }
    
    [Display(Name = "Дата регистрации")]
    public DateTime RegistrationDate { get; set; }
    
    [Display(Name = "Аватар")]
    public string? AvatarUrl { get; set; }
}

/// <summary>
/// Модель редактирования профиля
/// </summary>
public class EditProfileViewModel
{
    [Required(ErrorMessage = "Введите ФИО")]
    [StringLength(200, ErrorMessage = "ФИО не может превышать 200 символов")]
    [Display(Name = "ФИО")]
    public string FullName { get; set; } = string.Empty;

    [Phone(ErrorMessage = "Некорректный номер телефона")]
    [Display(Name = "Телефон")]
    public string? Phone { get; set; }
}
