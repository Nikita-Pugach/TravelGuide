using Microsoft.AspNetCore.Mvc;
using TravelGuide.Models.Entities;
using TravelGuide.Services;
using TravelGuide.Services.Interfaces;
using TravelGuide.ViewModels;

namespace TravelGuide.Controllers;

public class ChatController : Controller
{
    private readonly IChatService _chatService;
    private readonly IRepository<User> _userRepository;

    public ChatController(IChatService chatService, IRepository<User> userRepository)
    {
        _chatService = chatService;
        _userRepository = userRepository;
    }

    private int? GetCurrentUserId()
    {
        return HttpContext.Session.GetInt32("UserId");
    }

    private async Task<UserRole?> GetCurrentUserRole()
    {
        var userId = GetCurrentUserId();
        if (userId == null) return null;
        
        var user = await _userRepository.GetByIdAsync(userId.Value);
        return user?.Role;
    }

    // GET: /Chat
    public async Task<IActionResult> Index()
    {
        var userId = GetCurrentUserId();
        var role = await GetCurrentUserRole();
        
        if (userId == null || role == null)
            return RedirectToAction("Login", "Account");

        var chats = await _chatService.GetUserChatsAsync(userId.Value, role.Value);
        var unreadCount = await _chatService.GetUnreadCountAsync(userId.Value, role.Value);

        var viewModel = new ChatListViewModel
        {
            Chats = chats,
            UnreadCount = unreadCount
        };

        return View(viewModel);
    }

    // GET: /Chat/Create
    public IActionResult Create()
    {
        if (GetCurrentUserId() == null)
            return RedirectToAction("Login", "Account");

        return View(new CreateChatViewModel());
    }

    // POST: /Chat/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CreateChatViewModel model)
    {
        var userId = GetCurrentUserId();
        if (userId == null)
            return RedirectToAction("Login", "Account");

        if (!ModelState.IsValid)
            return View(model);

        var chat = await _chatService.CreateChatAsync(userId.Value, model.FirstMessage);
        return RedirectToAction("Conversation", new { id = chat.Id });
    }

    // GET: /Chat/Conversation/5
    public async Task<IActionResult> Conversation(int id)
    {
        var userId = GetCurrentUserId();
        var role = await GetCurrentUserRole();
        
        if (userId == null || role == null)
            return RedirectToAction("Login", "Account");

        var chat = await _chatService.GetChatAsync(id, userId.Value, role.Value);
        
        if (chat == null)
            return NotFound();

        return View(chat);
    }

    // POST: /Chat/SendMessage
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> SendMessage(SendMessageViewModel model)
    {
        var userId = GetCurrentUserId();
        if (userId == null)
            return Json(new { success = false, message = "Не авторизован" });

        if (!ModelState.IsValid)
            return Json(new { success = false, message = "Сообщение не может быть пустым" });

        var success = await _chatService.SendMessageAsync(model.ChatId, userId.Value, model.Text);
        
        if (success)
        {
            // Если это AJAX запрос, возвращаем JSON
            if (Request.Headers.XRequestedWith == "XMLHttpRequest")
            {
                return Json(new { 
                    success = true, 
                    message = new {
                        text = model.Text,
                        timestamp = DateTime.Now.ToString("dd.MM.yyyy HH:mm"),
                        isMyMessage = true
                    }
                });
            }
            return RedirectToAction("Conversation", new { id = model.ChatId });
        }

        return Json(new { success = false, message = "Ошибка отправки" });
    }

    // POST: /Chat/Close/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Close(int id)
    {
        var userId = GetCurrentUserId();
        var role = await GetCurrentUserRole();
        
        if (userId == null || role == null)
            return RedirectToAction("Login", "Account");

        await _chatService.CloseChatAsync(id, userId.Value, role.Value);
        return RedirectToAction("Index");
    }

    // GET: /Chat/GetUnreadCount (для AJAX)
    [HttpGet]
    public async Task<IActionResult> GetUnreadCount()
    {
        var userId = GetCurrentUserId();
        var role = await GetCurrentUserRole();
        
        if (userId == null || role == null)
            return Json(new { count = 0 });

        var count = await _chatService.GetUnreadCountAsync(userId.Value, role.Value);
        return Json(new { count });
    }
}