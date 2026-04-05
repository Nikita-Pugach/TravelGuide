using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TravelGuide.Data;
using TravelGuide.Models.Entities;
using TravelGuide.ViewModels;

namespace TravelGuide.Controllers;

public class SightsController : Controller
{
    private readonly TravelGuideContext _context;
    private const int PageSize = 9;

    public SightsController(TravelGuideContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index(
        string? search, 
        int? type, 
        int? cityId,
        string? sortBy,
        int page = 1)
    {
        var sights = _context.Sights
            .Include(s => s.City)
                .ThenInclude(c => c!.Country)
            .AsQueryable();

        if (!string.IsNullOrEmpty(search))
        {
            sights = sights.Where(s => s.Name.Contains(search) || 
                                       (s.Description != null && s.Description.Contains(search)));
        }

        if (type.HasValue)
        {
            sights = sights.Where(s => (int)s.Type == type.Value);
        }

        if (cityId.HasValue)
        {
            sights = sights.Where(s => s.CityId == cityId.Value);
        }

        // Sorting
        sights = sortBy switch
        {
            "name" => sights.OrderBy(s => s.Name),
            "type" => sights.OrderBy(s => s.Type),
            _ => sights.OrderBy(s => s.Name)
        };

        ViewBag.Cities = await _context.Cities.Include(c => c.Country).ToListAsync();
        ViewBag.SightTypes = Enum.GetValues(typeof(SightType)).Cast<SightType>().ToList();
        ViewBag.CurrentSearch = search;
        ViewBag.CurrentType = type;
        ViewBag.CurrentCityId = cityId;
        ViewBag.CurrentSortBy = sortBy;

        var totalItems = await sights.CountAsync();
        var items = await sights
            .Skip((page - 1) * PageSize)
            .Take(PageSize)
            .ToListAsync();

        return View(new PaginatedList<Sight>(items, totalItems, page, PageSize));
    }

    public async Task<IActionResult> Details(int? id)
    {
        if (id == null) return NotFound();

        var sight = await _context.Sights
            .Include(s => s.City)
                .ThenInclude(c => c!.Country)
            .Include(s => s.Reviews!)
                .ThenInclude(r => r.User)
            .FirstOrDefaultAsync(s => s.Id == id);

        if (sight == null) return NotFound();

        return View(sight);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> AddReview(CreateReviewViewModel model)
    {
        var userId = HttpContext.Session.GetInt32("UserId");
        if (userId == null)
            return RedirectToAction("Login", "Account");

        if (!model.SightId.HasValue)
            return BadRequest();

        if (!ModelState.IsValid)
        {
            TempData["ReviewError"] = "Проверьте правильность заполнения формы";
            return RedirectToAction(nameof(Details), new { id = model.SightId.Value });
        }

        var sightExists = await _context.Sights.AnyAsync(s => s.Id == model.SightId.Value);
        if (!sightExists)
            return NotFound();

        var existingReview = await _context.Reviews
            .FirstOrDefaultAsync(r => r.UserId == userId.Value && r.SightId == model.SightId.Value);

        if (existingReview != null)
        {
            TempData["ReviewError"] = "Вы уже оставляли отзыв об этой достопримечательности";
            return RedirectToAction(nameof(Details), new { id = model.SightId.Value });
        }

        var review = new Review
        {
            UserId = userId.Value,
            SightId = model.SightId.Value,
            Rating = model.Rating,
            Text = model.Text,
            Date = DateTime.Now,
            Status = ReviewStatus.Approved
        };

        _context.Reviews.Add(review);
        await _context.SaveChangesAsync();

        TempData["ReviewSuccess"] = "Спасибо за ваш отзыв!";
        return RedirectToAction(nameof(Details), new { id = model.SightId.Value });
    }
}