using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TravelGuide.Data;
using TravelGuide.Models.Entities;
using TravelGuide.ViewModels;

namespace TravelGuide.Controllers;

public class HotelsController : Controller
{
    private readonly TravelGuideContext _context;
    private const int PageSize = 9;

    public HotelsController(TravelGuideContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index(
        string? search, 
        int? stars, 
        int? cityId,
        decimal? minPrice,
        decimal? maxPrice,
        MealType? mealType,
        string? sortBy,
        int page = 1)
    {
        var hotels = _context.Hotels
            .Include(h => h.City)
                .ThenInclude(c => c!.Country)
            .AsQueryable();

        if (!string.IsNullOrEmpty(search))
        {
            hotels = hotels.Where(h => h.Name.Contains(search) || 
                                       (h.Description != null && h.Description.Contains(search)));
        }

        if (stars.HasValue)
        {
            hotels = hotels.Where(h => h.Stars == stars.Value);
        }

        if (cityId.HasValue)
        {
            hotels = hotels.Where(h => h.CityId == cityId.Value);
        }

        if (minPrice.HasValue)
        {
            hotels = hotels.Where(h => h.PricePerNight >= minPrice.Value);
        }

        if (maxPrice.HasValue)
        {
            hotels = hotels.Where(h => h.PricePerNight <= maxPrice.Value);
        }

        if (mealType.HasValue)
        {
            hotels = hotels.Where(h => h.MealType == mealType.Value);
        }

        // Sorting
        hotels = sortBy switch
        {
            "price_asc" => hotels.OrderBy(h => h.PricePerNight),
            "price_desc" => hotels.OrderByDescending(h => h.PricePerNight),
            "stars_desc" => hotels.OrderByDescending(h => h.Stars),
            "name" => hotels.OrderBy(h => h.Name),
            _ => hotels.OrderBy(h => h.Name)
        };

        ViewBag.Cities = await _context.Cities.Include(c => c.Country).ToListAsync();
        ViewBag.MealTypes = Enum.GetValues(typeof(MealType)).Cast<MealType>().ToList();
        ViewBag.CurrentSearch = search;
        ViewBag.CurrentStars = stars;
        ViewBag.CurrentCityId = cityId;
        ViewBag.CurrentMinPrice = minPrice;
        ViewBag.CurrentMaxPrice = maxPrice;
        ViewBag.CurrentMealType = mealType;
        ViewBag.CurrentSortBy = sortBy;

        var totalItems = await hotels.CountAsync();
        var items = await hotels
            .Skip((page - 1) * PageSize)
            .Take(PageSize)
            .ToListAsync();

        return View(new PaginatedList<Hotel>(items, totalItems, page, PageSize));
    }

    public async Task<IActionResult> Details(int? id)
    {
        if (id == null) return NotFound();

        var hotel = await _context.Hotels
            .Include(h => h.City)
                .ThenInclude(c => c!.Country)
            .Include(h => h.Reviews!)
                .ThenInclude(r => r.User)
            .FirstOrDefaultAsync(h => h.Id == id);

        if (hotel == null) return NotFound();

        return View(hotel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> AddReview(CreateReviewViewModel model)
    {
        var userId = HttpContext.Session.GetInt32("UserId");
        if (userId == null)
            return RedirectToAction("Login", "Account");

        if (!model.HotelId.HasValue)
            return BadRequest();

        if (!ModelState.IsValid)
        {
            TempData["ReviewError"] = "Проверьте правильность заполнения формы";
            return RedirectToAction(nameof(Details), new { id = model.HotelId.Value });
        }

        var hotelExists = await _context.Hotels.AnyAsync(h => h.Id == model.HotelId.Value);
        if (!hotelExists)
            return NotFound();

        var existingReview = await _context.Reviews
            .FirstOrDefaultAsync(r => r.UserId == userId.Value && r.HotelId == model.HotelId.Value);

        if (existingReview != null)
        {
            TempData["ReviewError"] = "Вы уже оставляли отзыв об этом отеле";
            return RedirectToAction(nameof(Details), new { id = model.HotelId.Value });
        }

        var review = new Review
        {
            UserId = userId.Value,
            HotelId = model.HotelId.Value,
            Rating = model.Rating,
            Text = model.Text,
            Date = DateTime.Now,
            Status = ReviewStatus.Approved
        };

        _context.Reviews.Add(review);
        await _context.SaveChangesAsync();

        TempData["ReviewSuccess"] = "Спасибо за ваш отзыв!";
        return RedirectToAction(nameof(Details), new { id = model.HotelId.Value });
    }
}