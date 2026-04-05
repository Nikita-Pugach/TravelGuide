using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TravelGuide.Data;
using TravelGuide.Models.Entities;
using TravelGuide.ViewModels;

namespace TravelGuide.Controllers;

/// <summary>
/// Контроллер туров
/// </summary>
public class ToursController : Controller
{
    private readonly TravelGuideContext _context;
    private const int PageSize = 9;

    public ToursController(TravelGuideContext context)
    {
        _context = context;
    }

    // GET: /Tours
    public async Task<IActionResult> Index(
        string? search, 
        int? countryId, 
        decimal? minPrice, 
        decimal? maxPrice, 
        int? tourType,
        int? durationMin,
        int? durationMax,
        string? sortBy,
        int page = 1)
    {
        var tours = _context.Tours
            .Include(t => t.Country)
            .Include(t => t.Agency)
            .Include(t => t.TourHotels!).ThenInclude(th => th.Hotel)
            .Include(t => t.TourSights!).ThenInclude(ts => ts.Sight)
            .AsQueryable();

        // Фильтрация
        if (!string.IsNullOrEmpty(search))
        {
            tours = tours.Where(t => t.Name.Contains(search) || 
                                     (t.Description != null && t.Description.Contains(search)));
        }

        if (countryId.HasValue)
        {
            tours = tours.Where(t => t.CountryId == countryId.Value);
        }

        if (minPrice.HasValue)
        {
            tours = tours.Where(t => t.Price >= minPrice.Value);
        }

        if (maxPrice.HasValue)
        {
            tours = tours.Where(t => t.Price <= maxPrice.Value);
        }

        if (tourType.HasValue)
        {
            tours = tours.Where(t => t.TourType == (TourType)tourType.Value);
        }

        if (durationMin.HasValue)
        {
            tours = tours.Where(t => t.Duration >= durationMin.Value);
        }

        if (durationMax.HasValue)
        {
            tours = tours.Where(t => t.Duration <= durationMax.Value);
        }

        // Сортировка
        tours = sortBy switch
        {
            "price_asc" => tours.OrderBy(t => t.Price),
            "price_desc" => tours.OrderByDescending(t => t.Price),
            "duration_asc" => tours.OrderBy(t => t.Duration),
            "duration_desc" => tours.OrderByDescending(t => t.Duration),
            "name" => tours.OrderBy(t => t.Name),
            "popular" => tours.OrderByDescending(t => t.ViewCount),
            "rating" => tours.OrderByDescending(t => t.Reviews != null ? t.Reviews.Average(r => r.Rating) : 0),
            _ => tours.OrderByDescending(t => t.CreatedDate)
        };

        // Передаем данные для фильтров
        ViewBag.Countries = await _context.Countries.ToListAsync();
        ViewBag.TourTypes = Enum.GetValues(typeof(TourType)).Cast<TourType>().ToList();
        ViewBag.CurrentSearch = search;
        ViewBag.CurrentCountryId = countryId;
        ViewBag.CurrentMinPrice = minPrice;
        ViewBag.CurrentMaxPrice = maxPrice;
        ViewBag.CurrentTourType = tourType;
        ViewBag.CurrentDurationMin = durationMin;
        ViewBag.CurrentDurationMax = durationMax;
        ViewBag.CurrentSortBy = sortBy;

        // Пагинация
        var totalItems = await tours.CountAsync();
        var items = await tours
            .Skip((page - 1) * PageSize)
            .Take(PageSize)
            .ToListAsync();

        var paginatedList = new PaginatedList<Tour>(items, totalItems, page, PageSize);

        // Получаем избранные туры пользователя
        var userId = HttpContext.Session.GetInt32("UserId");
        var favoriteIds = new HashSet<int>();
        if (userId.HasValue)
        {
            favoriteIds = (await _context.FavoriteTours
                .Where(ft => ft.UserId == userId.Value)
                .Select(ft => ft.TourId)
                .ToListAsync())
                .ToHashSet();
        }
        ViewBag.FavoriteIds = favoriteIds;

        return View(paginatedList);
    }

    // GET: /Tours/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
            return NotFound();

        var tour = await _context.Tours
            .Include(t => t.Country)
            .Include(t => t.Agency)
            .Include(t => t.TourHotels!).ThenInclude(th => th.Hotel)
            .Include(t => t.TourSights!).ThenInclude(ts => ts.Sight)
            .Include(t => t.Reviews!).ThenInclude(r => r.User)
            .FirstOrDefaultAsync(t => t.Id == id);

        if (tour == null)
            return NotFound();

        // Увеличиваем счётчик просмотров
        tour.ViewCount++;
        await _context.SaveChangesAsync();

        // Проверяем, добавлен ли тур в избранное
        var userId = HttpContext.Session.GetInt32("UserId");
        ViewBag.IsFavorite = false;
        if (userId.HasValue)
        {
            ViewBag.IsFavorite = await _context.FavoriteTours
                .AnyAsync(ft => ft.UserId == userId.Value && ft.TourId == id.Value);
        }

        return View(tour);
    }

    // GET: /Tours/Create (перенаправление на админку)
    [HttpGet]
    public IActionResult Create()
    {
        var userRole = HttpContext.Session.GetString("UserRole");
        if (userRole != "Manager" && userRole != "Admin")
            return RedirectToAction("Login", "Account");

        return RedirectToAction("CreateTour", "Admin");
    }

    // POST: /Tours/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Tour tour)
    {
        var userRole = HttpContext.Session.GetString("UserRole");
        if (userRole != "Manager" && userRole != "Admin")
            return RedirectToAction("Login", "Account");

        if (ModelState.IsValid)
        {
            tour.CreatedDate = DateTime.Now;
            _context.Tours.Add(tour);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        ViewBag.Countries = _context.Countries.ToList();
        ViewBag.Agencies = _context.Agencies.ToList();
        return View(tour);
    }

    // POST: /Tours/AddToFavorites/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> AddToFavorites(int id)
    {
        var userId = HttpContext.Session.GetInt32("UserId");
        if (userId == null)
            return RedirectToAction("Login", "Account");

        var exists = await _context.FavoriteTours
            .AnyAsync(ft => ft.UserId == userId.Value && ft.TourId == id);

        if (!exists)
        {
            _context.FavoriteTours.Add(new FavoriteTour
            {
                UserId = userId.Value,
                TourId = id,
                AddedDate = DateTime.Now
            });
            await _context.SaveChangesAsync();
        }

        return RedirectToAction(nameof(Details), new { id });
    }

    // POST: /Tours/RemoveFromFavorites/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> RemoveFromFavorites(int id)
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
        }

        return RedirectToAction(nameof(Details), new { id });
    }

    // POST: /Tours/AddReview
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> AddReview(CreateReviewViewModel model)
    {
        var userId = HttpContext.Session.GetInt32("UserId");
        if (userId == null)
            return RedirectToAction("Login", "Account");

        if (!model.TourId.HasValue)
            return BadRequest();

        if (!ModelState.IsValid)
        {
            TempData["ReviewError"] = "Проверьте правильность заполнения формы";
            return RedirectToAction(nameof(Details), new { id = model.TourId.Value });
        }

        // Проверяем существование тура
        var tourExists = await _context.Tours.AnyAsync(t => t.Id == model.TourId.Value);
        if (!tourExists)
            return NotFound();

        // Проверяем, не оставлял ли пользователь уже отзыв
        var existingReview = await _context.Reviews
            .FirstOrDefaultAsync(r => r.UserId == userId.Value && r.TourId == model.TourId.Value);

        if (existingReview != null)
        {
            TempData["ReviewError"] = "Вы уже оставляли отзыв об этом туре";
            return RedirectToAction(nameof(Details), new { id = model.TourId.Value });
        }

        var review = new Review
        {
            UserId = userId.Value,
            TourId = model.TourId.Value,
            Rating = model.Rating,
            Text = model.Text,
            Date = DateTime.Now,
            Status = ReviewStatus.Approved // Автоматически одобряем
        };

        _context.Reviews.Add(review);
        await _context.SaveChangesAsync();

        TempData["ReviewSuccess"] = "Спасибо за ваш отзыв!";
        return RedirectToAction(nameof(Details), new { id = model.TourId.Value });
    }
}