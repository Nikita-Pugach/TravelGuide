using TravelGuide.Models.Entities;

namespace TravelGuide.Services.Interfaces;

/// <summary>
/// Интерфейс сервиса авторизации
/// </summary>
public interface IAccountService
{
    Task<User?> AuthenticateAsync(string email, string password);
    Task<bool> RegisterAsync(User user, string password);
    Task<bool> UserExistsAsync(string email);
    Task<User?> GetCurrentUserAsync(int userId);
    Task UpdatePasswordAsync(int userId, string newPassword);
    Task<bool> UpdateProfileAsync(User user);
}
