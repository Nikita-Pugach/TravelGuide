using System.ComponentModel.DataAnnotations;

namespace TravelGuide.Models.Entities;

/// <summary>
/// Пользователь системы
/// </summary>
public class User : BaseEntity
{
    /// <summary>
    /// ФИО пользователя
    /// </summary>
    [Required(ErrorMessage = "ФИО обязательно")]
    [StringLength(200, ErrorMessage = "ФИО не может превышать 200 символов")]
    [Display(Name = "ФИО")]
    public string FullName { get; set; } = string.Empty;
    
    /// <summary>
    /// Электронная почта
    /// </summary>
    [Required(ErrorMessage = "Email обязателен")]
    [EmailAddress(ErrorMessage = "Некорректный формат email")]
    [StringLength(100, ErrorMessage = "Email не может превышать 100 символов")]
    [Display(Name = "Email")]
    public string Email { get; set; } = string.Empty;
    
    /// <summary>
    /// Хеш пароля
    /// </summary>
    [Required]
    public string PasswordHash { get; set; } = string.Empty;
    
    /// <summary>
    /// Роль пользователя
    /// </summary>
    [Display(Name = "Роль")]
    public UserRole Role { get; set; } = UserRole.Tourist;
    
    /// <summary>
    /// Дата регистрации
    /// </summary>
    [Display(Name = "Дата регистрации")]
    public DateTime RegistrationDate { get; set; } = DateTime.Now;
    
    /// <summary>
    /// Признак блокировки
    /// </summary>
    [Display(Name = "Заблокирован")]
    public bool IsBlocked { get; set; }
    
    /// <summary>
    /// Контактный телефон
    /// </summary>
    [Phone(ErrorMessage = "Некорректный формат телефона")]
    [Display(Name = "Телефон")]
    public string? Phone { get; set; }
    
    /// <summary>
    /// URL аватара
    /// </summary>
    [Display(Name = "Аватар")]
    public string? AvatarUrl { get; set; }
    
    /// <summary>
    /// Идентификатор агентства (для менеджеров)
    /// </summary>
    [Display(Name = "Агентство")]
    public int? AgencyId { get; set; }
    
    /// <summary>
    /// Агентство (для менеджеров)
    /// </summary>
    public virtual Agency? Agency { get; set; }
    
    /// <summary>
    /// Отзывы пользователя
    /// </summary>
    public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();
    
    /// <summary>
    /// Избранные туры
    /// </summary>
    public virtual ICollection<FavoriteTour> FavoriteTours { get; set; } = new List<FavoriteTour>();
    
    /// <summary>
    /// Чаты пользователя
    /// </summary>
    public virtual ICollection<Chat> Chats { get; set; } = new List<Chat>();
    
    /// <summary>
    /// Сообщения пользователя
    /// </summary>
    public virtual ICollection<Message> Messages { get; set; } = new List<Message>();
    
    /// <summary>
    /// Чаты, которые менеджер ведёт
    /// </summary>
    public virtual ICollection<Chat> ManagedChats { get; set; } = new List<Chat>();
}