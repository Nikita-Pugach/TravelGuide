using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TravelGuide.Data;

namespace TravelGuide.Controllers;

/// <summary>
/// Контроллер избранных туров
/// </summary>
public class FavoritesController : Controller
{
    private readonly TravelGuideContext _context;

    public FavoritesController(TravelGuideContext context)
    {
        _context = context;
    }

    // GET: /Favorites
    public async Task<IActionResult> Index()
    {
        var userId = HttpContext.Session.GetInt32("UserId");
        if (userId == null)
            return RedirectToAction("Login", "Account");

        var favorites = await _context.FavoriteTours
            .Include(ft => ft.Tour!)
                .ThenInclude(t => t.Country)
            .Include(ft => ft.Tour!)
                .ThenInclude(t => t.Agency)
            .Where(ft => ft.UserId == userId.Value)
            .OrderByDescending(ft => ft.AddedDate)
            .ToListAsync();

        return View(favorites);
    }

    // POST: /Favorites/Add/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Add(int id)
    {
        var userId = HttpContext.Session.GetInt32("UserId");
        if (userId == null)
            return Json(new { success = false, needLogin = true });

        var exists = await _context.FavoriteTours
            .AnyAsync(ft => ft.UserId == userId.Value && ft.TourId == id);

        if (!exists)
        {
            _context.FavoriteTours.Add(new Models.Entities.FavoriteTour
            {
                UserId = userId.Value,
                TourId = id,
                AddedDate = DateTime.Now
            });
            await _context.SaveChangesAsync();
        }

        return Json(new { success = true });
    }

    // POST: /Favorites/Remove/5 (для AJAX из каталога туров)
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Remove(int id)
    {
        var userId = HttpContext.Session.GetInt32("UserId");
        if (userId == null)
            return Json(new { success = false, needLogin = true });

        var favorite = await _context.FavoriteTours
            .FirstOrDefaultAsync(ft => ft.UserId == userId.Value && ft.TourId == id);

        if (favorite != null)
        {
            _context.FavoriteTours.Remove(favorite);
            await _context.SaveChangesAsync();
        }

        return Json(new { success = true });
    }

    // POST: /Favorites/RemoveFromList/5 (для удаления со страницы избранного)
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> RemoveFromList(int id)
    {
        var userId = HttpContext.Session.GetInt32("UserId");
        if (userId == null)
            return RedirectToAction("Login", "Account");

        var favorite = await _context.FavoriteTours
            .FirstOrDefaultAsync(ft => ft.UserId == userId.Value && ft.TourId == id);

        if (favorite != null)
        {
            _context.FavoriteTours.Remove(favorite);
            await _context.SaveChangesAsync();
            TempData["SuccessMessage"] = "Тур удалён из избранного";
        }

        return RedirectToAction(nameof(Index));
    }
}
