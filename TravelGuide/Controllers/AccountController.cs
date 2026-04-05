using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TravelGuide.Data;
using TravelGuide.Models.Entities;
using TravelGuide.Services.Interfaces;
using TravelGuide.ViewModels;
using System.IO;

namespace TravelGuide.Controllers;

/// <summary>
/// Контроллер авторизации
/// </summary>
public class AccountController : Controller
{
    private readonly IAccountService _accountService;
    private readonly TravelGuideContext _context;
    private readonly IWebHostEnvironment _webHostEnvironment;

    public AccountController(IAccountService accountService, TravelGuideContext context, IWebHostEnvironment webHostEnvironment)
    {
        _accountService = accountService;
        _context = context;
        _webHostEnvironment = webHostEnvironment;
    }

    // GET: /Account/Login
    [HttpGet]
    public IActionResult Login(string? returnUrl = null)
    {
        ViewData["ReturnUrl"] = returnUrl;
        return View();
    }

    // POST: /Account/Login
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginViewModel model, string? returnUrl = null)
    {
        ViewData["ReturnUrl"] = returnUrl;

        if (!ModelState.IsValid)
            return View(model);

        var user = await _accountService.AuthenticateAsync(model.Email, model.Password);

        if (user == null)
        {
            ModelState.AddModelError(string.Empty, "Неверный email или пароль");
            return View(model);
        }

        // Сохраняем данные пользователя в сессии
        HttpContext.Session.SetInt32("UserId", user.Id);
        HttpContext.Session.SetString("UserEmail", user.Email);
        HttpContext.Session.SetString("UserName", user.FullName);
        HttpContext.Session.SetString("UserRole", user.Role.ToString());

        if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
            return Redirect(returnUrl);

        return RedirectToAction("Index", "Home");
    }

    // GET: /Account/Register
    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }

    // POST: /Account/Register
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Register(RegisterViewModel model)
    {
        if (!ModelState.IsValid)
            return View(model);

        if (await _accountService.UserExistsAsync(model.Email))
        {
            ModelState.AddModelError("Email", "Пользователь с таким email уже существует");
            return View(model);
        }

        var user = new User
        {
            FullName = model.FullName,
            Email = model.Email,
            Phone = model.Phone
        };

        var result = await _accountService.RegisterAsync(user, model.Password);

        if (!result)
        {
            ModelState.AddModelError(string.Empty, "Ошибка при регистрации");
            return View(model);
        }

        // Автоматический вход после регистрации
        var authenticatedUser = await _accountService.AuthenticateAsync(model.Email, model.Password);
        if (authenticatedUser != null)
        {
            HttpContext.Session.SetInt32("UserId", authenticatedUser.Id);
            HttpContext.Session.SetString("UserEmail", authenticatedUser.Email);
            HttpContext.Session.SetString("UserName", authenticatedUser.FullName);
            HttpContext.Session.SetString("UserRole", authenticatedUser.Role.ToString());
        }

        return RedirectToAction("Index", "Home");
    }

    // GET: /Account/Profile
    [HttpGet]
    public async Task<IActionResult> Profile()
    {
        var userId = HttpContext.Session.GetInt32("UserId");
        if (userId == null)
            return RedirectToAction("Login");

        var user = await _accountService.GetCurrentUserAsync(userId.Value);
        if (user == null)
            return RedirectToAction("Login");

        var model = new ProfileViewModel
        {
            Id = user.Id,
            FullName = user.FullName,
            Email = user.Email,
            Phone = user.Phone,
            Role = user.Role.ToString(),
            AgencyName = user.Agency?.Name,
            RegistrationDate = user.RegistrationDate,
            AvatarUrl = user.AvatarUrl
        };

        // Статистика
        ViewBag.FavoritesCount = await _context.FavoriteTours.CountAsync(f => f.UserId == userId.Value);
        ViewBag.ChatsCount = await _context.Chats.CountAsync(c => c.UserId == userId.Value);
        ViewBag.RecentFavorites = await _context.FavoriteTours
            .Include(f => f.Tour!).ThenInclude(t => t.Country)
            .Where(f => f.UserId == userId.Value)
            .OrderByDescending(f => f.AddedDate)
            .Take(3)
            .ToListAsync();

        return View(model);
    }

    // GET: /Account/Edit
    [HttpGet]
    public async Task<IActionResult> Edit()
    {
        var userId = HttpContext.Session.GetInt32("UserId");
        if (userId == null)
            return RedirectToAction("Login");

        var user = await _accountService.GetCurrentUserAsync(userId.Value);
        if (user == null)
            return RedirectToAction("Login");

        var model = new EditProfileViewModel
        {
            FullName = user.FullName,
            Phone = user.Phone
        };

        return View(model);
    }

    // POST: /Account/Edit
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(EditProfileViewModel model)
    {
        var userId = HttpContext.Session.GetInt32("UserId");
        if (userId == null)
            return RedirectToAction("Login");

        if (!ModelState.IsValid)
            return View(model);

        var user = await _accountService.GetCurrentUserAsync(userId.Value);
        if (user == null)
            return RedirectToAction("Login");

        user.FullName = model.FullName;
        user.Phone = model.Phone;

        var result = await _accountService.UpdateProfileAsync(user);

        if (result)
        {
            HttpContext.Session.SetString("UserName", user.FullName);
            TempData["SuccessMessage"] = "Профиль успешно обновлен";
            return RedirectToAction(nameof(Profile));
        }

        ModelState.AddModelError(string.Empty, "Ошибка при обновлении профиля");
        return View(model);
    }

    // POST: /Account/UploadAvatar
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> UploadAvatar(IFormFile avatar)
    {
        var userId = HttpContext.Session.GetInt32("UserId");
        if (userId == null)
            return Json(new { success = false, error = "Не авторизован" });

        if (avatar == null || avatar.Length == 0)
            return Json(new { success = false, error = "Файл не выбран" });

        // Проверяем тип файла
        var allowedTypes = new[] { "image/jpeg", "image/png", "image/gif" };
        if (!allowedTypes.Contains(avatar.ContentType.ToLower()))
            return Json(new { success = false, error = "Разрешены только JPG, PNG, GIF" });

        // Проверяем размер (максимум 5 МБ)
        if (avatar.Length > 5 * 1024 * 1024)
            return Json(new { success = false, error = "Максимальный размер файла - 5 МБ" });

        var user = await _context.Users.FindAsync(userId.Value);
        if (user == null)
            return Json(new { success = false, error = "Пользователь не найден" });

        // Создаём уникальное имя файла
        var extension = Path.GetExtension(avatar.FileName);
        var fileName = $"{userId.Value}_{Guid.NewGuid()}{extension}";
        var uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "uploads", "avatars");

        // Удаляем старый аватар если есть
        if (!string.IsNullOrEmpty(user.AvatarUrl))
        {
            var oldPath = Path.Combine(_webHostEnvironment.WebRootPath, user.AvatarUrl.TrimStart('/'));
            if (System.IO.File.Exists(oldPath))
                System.IO.File.Delete(oldPath);
        }

        // Сохраняем новый файл
        var filePath = Path.Combine(uploadsFolder, fileName);
        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await avatar.CopyToAsync(stream);
        }

        // Обновляем путь в БД
        user.AvatarUrl = $"/uploads/avatars/{fileName}";
        await _context.SaveChangesAsync();

        return Json(new { success = true, avatarUrl = user.AvatarUrl });
    }

    // POST: /Account/Logout
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Logout()
    {
        HttpContext.Session.Clear();
        return RedirectToAction("Index", "Home");
    }
}
