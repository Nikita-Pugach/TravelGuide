using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TravelGuide.Data;
using TravelGuide.Models.Entities;
using TravelGuide.ViewModels;

namespace TravelGuide.Controllers;

/// <summary>
/// Контроллер отзывов
/// </summary>
public class ReviewsController : Controller
{
    private readonly TravelGuideContext _context;

    public ReviewsController(TravelGuideContext context)
    {
        _context = context;
    }

    // POST: /Reviews/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CreateReviewViewModel model)
    {
        var userId = HttpContext.Session.GetInt32("UserId");
        if (userId == null)
            return RedirectToAction("Login", "Account");

        if (!ModelState.IsValid)
        {
            // Возвращаемся на предыдущую страницу с ошибками
            if (model.TourId.HasValue)
                return RedirectToAction("Details", "Tours", new { id = model.TourId.Value });
            if (model.HotelId.HasValue)
                return RedirectToAction("Details", "Hotels", new { id = model.HotelId.Value });
            if (model.SightId.HasValue)
                return RedirectToAction("Details", "Sights", new { id = model.SightId.Value });
            
            return RedirectToAction("Index", "Home");
        }

        // Проверяем, что указан хотя бы один объект
        if (!model.TourId.HasValue && !model.HotelId.HasValue && !model.SightId.HasValue)
        {
            TempData["ErrorMessage"] = "Не указан объект для отзыва";
            return RedirectToAction("Index", "Home");
        }

        // Проверяем, что пользователь ещё не оставлял отзыв на этот объект
        var existingReview = await _context.Reviews
            .FirstOrDefaultAsync(r => r.UserId == userId.Value &&
                ((model.TourId.HasValue && r.TourId == model.TourId.Value) ||
                 (model.HotelId.HasValue && r.HotelId == model.HotelId.Value) ||
                 (model.SightId.HasValue && r.SightId == model.SightId.Value)));

        if (existingReview != null)
        {
            TempData["ErrorMessage"] = "Вы уже оставляли отзыв на этот объект";
            if (model.TourId.HasValue)
                return RedirectToAction("Details", "Tours", new { id = model.TourId.Value });
            if (model.HotelId.HasValue)
                return RedirectToAction("Details", "Hotels", new { id = model.HotelId.Value });
            if (model.SightId.HasValue)
                return RedirectToAction("Details", "Sights", new { id = model.SightId.Value });
        }

        var review = new Review
        {
            UserId = userId.Value,
            TourId = model.TourId,
            HotelId = model.HotelId,
            SightId = model.SightId,
            Rating = model.Rating,
            Text = model.Text,
            Date = DateTime.Now,
            Status = ReviewStatus.Pending
        };

        _context.Reviews.Add(review);
        await _context.SaveChangesAsync();

        TempData["SuccessMessage"] = "Отзыв отправлен на модерацию";
        
        if (model.TourId.HasValue)
            return RedirectToAction("Details", "Tours", new { id = model.TourId.Value });
        if (model.HotelId.HasValue)
            return RedirectToAction("Details", "Hotels", new { id = model.HotelId.Value });
        if (model.SightId.HasValue)
            return RedirectToAction("Details", "Sights", new { id = model.SightId.Value });
        
        return RedirectToAction("Index", "Home");
    }

    // GET: /Reviews/My
    public async Task<IActionResult> My()
    {
        var userId = HttpContext.Session.GetInt32("UserId");
        if (userId == null)
            return RedirectToAction("Login", "Account");

        var reviews = await _context.Reviews
            .Include(r => r.Tour!).ThenInclude(t => t.Country)
            .Include(r => r.Hotel!).ThenInclude(h => h.City)
            .Include(r => r.Sight!).ThenInclude(s => s.City)
            .Where(r => r.UserId == userId.Value)
            .OrderByDescending(r => r.Date)
            .ToListAsync();

        return View(reviews);
    }
}