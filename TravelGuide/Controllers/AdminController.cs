using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TravelGuide.Data;
using TravelGuide.Models.Entities;

namespace TravelGuide.Controllers;

/// <summary>
/// Административная панель
/// </summary>
public class AdminController : Controller
{
    private readonly TravelGuideContext _context;
    private readonly IWebHostEnvironment _webHostEnvironment;

    public AdminController(TravelGuideContext context, IWebHostEnvironment webHostEnvironment)
    {
        _context = context;
        _webHostEnvironment = webHostEnvironment;
    }

    // Проверка прав администратора
    private bool IsAdmin()
    {
        var role = HttpContext.Session.GetString("UserRole");
        return role == "Admin";
    }

    // GET: /Admin
    public async Task<IActionResult> Index()
    {
        if (!IsAdmin())
            return RedirectToAction("Login", "Account");

        ViewBag.UsersCount = await _context.Users.CountAsync();
        ViewBag.ToursCount = await _context.Tours.CountAsync();
        ViewBag.ReviewsPendingCount = await _context.Reviews.CountAsync(r => r.Status == ReviewStatus.Pending);
        ViewBag.ChatsActiveCount = await _context.Chats.CountAsync(c => c.Status == ChatStatus.Active);

        return View();
    }

    #region Модерация отзывов

    // GET: /Admin/Reviews
    public async Task<IActionResult> Reviews(ReviewStatus? status = null)
    {
        if (!IsAdmin())
            return RedirectToAction("Login", "Account");

        var query = _context.Reviews
            .Include(r => r.User)
            .Include(r => r.Tour)
            .Include(r => r.Hotel)
            .Include(r => r.Sight)
            .AsQueryable();

        if (status.HasValue)
            query = query.Where(r => r.Status == status.Value);

        var reviews = await query
            .OrderByDescending(r => r.Date)
            .ToListAsync();

        ViewBag.CurrentStatus = status;
        return View(reviews);
    }

    // POST: /Admin/ApproveReview/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ApproveReview(int id)
    {
        if (!IsAdmin())
            return RedirectToAction("Login", "Account");

        var review = await _context.Reviews.FindAsync(id);
        if (review == null)
            return NotFound();

        review.Status = ReviewStatus.Approved;
        await _context.SaveChangesAsync();

        TempData["SuccessMessage"] = "Отзыв опубликован";
        return RedirectToAction(nameof(Reviews), new { status = ReviewStatus.Pending });
    }

    // POST: /Admin/RejectReview/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> RejectReview(int id)
    {
        if (!IsAdmin())
            return RedirectToAction("Login", "Account");

        var review = await _context.Reviews.FindAsync(id);
        if (review == null)
            return NotFound();

        review.Status = ReviewStatus.Rejected;
        await _context.SaveChangesAsync();

        TempData["SuccessMessage"] = "Отзыв отклонён";
        return RedirectToAction(nameof(Reviews), new { status = ReviewStatus.Pending });
    }

    #endregion

    #region Управление пользователями

    // GET: /Admin/Users
    public async Task<IActionResult> Users()
    {
        if (!IsAdmin())
            return RedirectToAction("Login", "Account");

        var users = await _context.Users
            .Include(u => u.Agency)
            .OrderByDescending(u => u.RegistrationDate)
            .ToListAsync();

        return View(users);
    }

    // POST: /Admin/ToggleUserBlock/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ToggleUserBlock(int id)
    {
        if (!IsAdmin())
            return RedirectToAction("Login", "Account");

        var user = await _context.Users.FindAsync(id);
        if (user == null)
            return NotFound();

        if (user.Role == UserRole.Admin)
        {
            TempData["ErrorMessage"] = "Нельзя заблокировать администратора";
            return RedirectToAction(nameof(Users));
        }

        user.IsBlocked = !user.IsBlocked;
        await _context.SaveChangesAsync();

        TempData["SuccessMessage"] = user.IsBlocked ? "Пользователь заблокирован" : "Пользователь разблокирован";
        return RedirectToAction(nameof(Users));
    }

    #endregion

    #region Справочники

    // GET: /Admin/Countries
    public async Task<IActionResult> Countries()
    {
        if (!IsAdmin())
            return RedirectToAction("Login", "Account");

        var countries = await _context.Countries
            .Include(c => c.Cities)
            .OrderBy(c => c.Name)
            .ToListAsync();

        return View(countries);
    }

    // GET: /Admin/CreateCountry
    public IActionResult CreateCountry()
    {
        if (!IsAdmin())
            return RedirectToAction("Login", "Account");

        return View();
    }

    // POST: /Admin/CreateCountry
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateCountry(Country country, IFormFile? photo)
    {
        if (!IsAdmin())
            return RedirectToAction("Login", "Account");

        if (ModelState.IsValid)
        {
            if (photo != null && photo.Length > 0)
            {
                country.PhotoUrl = await SavePhotoAsync(photo, "countries");
            }

            _context.Countries.Add(country);
            await _context.SaveChangesAsync();
            TempData["SuccessMessage"] = "Страна успешно создана";
            return RedirectToAction(nameof(Countries));
        }

        return View(country);
    }

    // GET: /Admin/EditCountry/5
    public async Task<IActionResult> EditCountry(int? id)
    {
        if (!IsAdmin())
            return RedirectToAction("Login", "Account");

        if (id == null)
            return NotFound();

        var country = await _context.Countries.FindAsync(id);
        if (country == null)
            return NotFound();

        return View(country);
    }

    // POST: /Admin/EditCountry/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> EditCountry(int id, Country country, IFormFile? photo)
    {
        if (!IsAdmin())
            return RedirectToAction("Login", "Account");

        if (id != country.Id)
            return NotFound();

        if (ModelState.IsValid)
        {
            var existingCountry = await _context.Countries.AsNoTracking().FirstOrDefaultAsync(c => c.Id == id);
            if (existingCountry == null)
                return NotFound();

            if (photo != null && photo.Length > 0)
            {
                country.PhotoUrl = await SavePhotoAsync(photo, "countries");
            }
            else
            {
                country.PhotoUrl = existingCountry.PhotoUrl;
            }

            _context.Update(country);
            await _context.SaveChangesAsync();
            TempData["SuccessMessage"] = "Страна успешно обновлена";
            return RedirectToAction(nameof(Countries));
        }

        return View(country);
    }

    // POST: /Admin/DeleteCountry/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteCountry(int id)
    {
        if (!IsAdmin())
            return RedirectToAction("Login", "Account");

        var country = await _context.Countries.FindAsync(id);
        if (country != null)
        {
            _context.Countries.Remove(country);
            await _context.SaveChangesAsync();
            TempData["SuccessMessage"] = "Страна удалена";
        }

        return RedirectToAction(nameof(Countries));
    }

    // GET: /Admin/Cities
    public async Task<IActionResult> Cities()
    {
        if (!IsAdmin())
            return RedirectToAction("Login", "Account");

        var cities = await _context.Cities
            .Include(c => c.Country)
            .OrderBy(c => c.Name)
            .ToListAsync();

        return View(cities);
    }

    // GET: /Admin/CreateCity
    public async Task<IActionResult> CreateCity()
    {
        if (!IsAdmin())
            return RedirectToAction("Login", "Account");

        ViewBag.Countries = await _context.Countries.OrderBy(c => c.Name).ToListAsync();
        return View();
    }

    // POST: /Admin/CreateCity
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateCity(City city)
    {
        if (!IsAdmin())
            return RedirectToAction("Login", "Account");

        if (ModelState.IsValid)
        {
            _context.Cities.Add(city);
            await _context.SaveChangesAsync();
            TempData["SuccessMessage"] = "Город успешно создан";
            return RedirectToAction(nameof(Cities));
        }

        ViewBag.Countries = await _context.Countries.OrderBy(c => c.Name).ToListAsync();
        return View(city);
    }

    // GET: /Admin/EditCity/5
    public async Task<IActionResult> EditCity(int? id)
    {
        if (!IsAdmin())
            return RedirectToAction("Login", "Account");

        if (id == null)
            return NotFound();

        var city = await _context.Cities.FindAsync(id);
        if (city == null)
            return NotFound();

        ViewBag.Countries = await _context.Countries.OrderBy(c => c.Name).ToListAsync();
        return View(city);
    }

    // POST: /Admin/EditCity/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> EditCity(int id, City city)
    {
        if (!IsAdmin())
            return RedirectToAction("Login", "Account");

        if (id != city.Id)
            return NotFound();

        if (ModelState.IsValid)
        {
            _context.Update(city);
            await _context.SaveChangesAsync();
            TempData["SuccessMessage"] = "Город успешно обновлён";
            return RedirectToAction(nameof(Cities));
        }

        ViewBag.Countries = await _context.Countries.OrderBy(c => c.Name).ToListAsync();
        return View(city);
    }

    // POST: /Admin/DeleteCity/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteCity(int id)
    {
        if (!IsAdmin())
            return RedirectToAction("Login", "Account");

        var city = await _context.Cities.FindAsync(id);
        if (city != null)
        {
            _context.Cities.Remove(city);
            await _context.SaveChangesAsync();
            TempData["SuccessMessage"] = "Город удалён";
        }

        return RedirectToAction(nameof(Cities));
    }

    // GET: /Admin/Agencies
    public async Task<IActionResult> Agencies()
    {
        if (!IsAdmin())
            return RedirectToAction("Login", "Account");

        var agencies = await _context.Agencies
            .Include(a => a.Tours)
            .OrderBy(a => a.Name)
            .ToListAsync();

        return View(agencies);
    }

    // GET: /Admin/CreateAgency
    public IActionResult CreateAgency()
    {
        if (!IsAdmin())
            return RedirectToAction("Login", "Account");

        return View();
    }

    // POST: /Admin/CreateAgency
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateAgency(Agency agency, IFormFile? logo)
    {
        if (!IsAdmin())
            return RedirectToAction("Login", "Account");

        if (ModelState.IsValid)
        {
            if (logo != null && logo.Length > 0)
            {
                agency.LogoUrl = await SavePhotoAsync(logo, "agencies");
            }

            _context.Agencies.Add(agency);
            await _context.SaveChangesAsync();
            TempData["SuccessMessage"] = "Агентство успешно создано";
            return RedirectToAction(nameof(Agencies));
        }

        return View(agency);
    }

    // GET: /Admin/EditAgency/5
    public async Task<IActionResult> EditAgency(int? id)
    {
        if (!IsAdmin())
            return RedirectToAction("Login", "Account");

        if (id == null)
            return NotFound();

        var agency = await _context.Agencies.FindAsync(id);
        if (agency == null)
            return NotFound();

        return View(agency);
    }

    // POST: /Admin/EditAgency/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> EditAgency(int id, Agency agency, IFormFile? logo)
    {
        if (!IsAdmin())
            return RedirectToAction("Login", "Account");

        if (id != agency.Id)
            return NotFound();

        if (ModelState.IsValid)
        {
            var existingAgency = await _context.Agencies.AsNoTracking().FirstOrDefaultAsync(a => a.Id == id);
            if (existingAgency == null)
                return NotFound();

            if (logo != null && logo.Length > 0)
            {
                agency.LogoUrl = await SavePhotoAsync(logo, "agencies");
            }
            else
            {
                agency.LogoUrl = existingAgency.LogoUrl;
            }

            _context.Update(agency);
            await _context.SaveChangesAsync();
            TempData["SuccessMessage"] = "Агентство успешно обновлено";
            return RedirectToAction(nameof(Agencies));
        }

        return View(agency);
    }

    // POST: /Admin/DeleteAgency/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteAgency(int id)
    {
        if (!IsAdmin())
            return RedirectToAction("Login", "Account");

        var agency = await _context.Agencies.FindAsync(id);
        if (agency != null)
        {
            _context.Agencies.Remove(agency);
            await _context.SaveChangesAsync();
            TempData["SuccessMessage"] = "Агентство удалено";
        }

        return RedirectToAction(nameof(Agencies));
    }

    #endregion

    #region Управление турами

    // GET: /Admin/Tours
    public async Task<IActionResult> Tours()
    {
        if (!IsAdmin())
            return RedirectToAction("Login", "Account");

        var tours = await _context.Tours
            .Include(t => t.Country)
            .Include(t => t.Agency)
            .OrderByDescending(t => t.CreatedDate)
            .ToListAsync();

        return View(tours);
    }

    // GET: /Admin/CreateTour
    public async Task<IActionResult> CreateTour()
    {
        if (!IsAdmin())
            return RedirectToAction("Login", "Account");

        ViewBag.Countries = await _context.Countries.OrderBy(c => c.Name).ToListAsync();
        ViewBag.Agencies = await _context.Agencies.OrderBy(a => a.Name).ToListAsync();
        ViewBag.TourTypes = Enum.GetValues(typeof(TourType)).Cast<TourType>().ToList();
        ViewBag.Hotels = await _context.Hotels.Include(h => h.City).OrderBy(h => h.Name).ToListAsync();
        ViewBag.Sights = await _context.Sights.Include(s => s.City).OrderBy(s => s.Name).ToListAsync();
        return View();
    }

    // POST: /Admin/CreateTour
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateTour(Tour tour, IFormFile? photo, int[] selectedHotels, int[] selectedSights)
    {
        if (!IsAdmin())
            return RedirectToAction("Login", "Account");

        if (ModelState.IsValid)
        {
            if (photo != null && photo.Length > 0)
            {
                tour.PhotoUrl = await SavePhotoAsync(photo, "tours");
            }

            tour.CreatedDate = DateTime.Now;
            _context.Tours.Add(tour);
            await _context.SaveChangesAsync();

            // Добавляем отели
            foreach (var hotelId in selectedHotels)
            {
                _context.TourHotels.Add(new TourHotel { TourId = tour.Id, HotelId = hotelId });
            }

            // Добавляем достопримечательности
            foreach (var sightId in selectedSights)
            {
                _context.TourSights.Add(new TourSight { TourId = tour.Id, SightId = sightId });
            }

            await _context.SaveChangesAsync();
            TempData["SuccessMessage"] = "Тур успешно создан";
            return RedirectToAction(nameof(Tours));
        }

        ViewBag.Countries = await _context.Countries.OrderBy(c => c.Name).ToListAsync();
        ViewBag.Agencies = await _context.Agencies.OrderBy(a => a.Name).ToListAsync();
        ViewBag.TourTypes = Enum.GetValues(typeof(TourType)).Cast<TourType>().ToList();
        ViewBag.Hotels = await _context.Hotels.Include(h => h.City).OrderBy(h => h.Name).ToListAsync();
        ViewBag.Sights = await _context.Sights.Include(s => s.City).OrderBy(s => s.Name).ToListAsync();
        return View(tour);
    }

    // GET: /Admin/EditTour/5
    public async Task<IActionResult> EditTour(int? id)
    {
        if (!IsAdmin())
            return RedirectToAction("Login", "Account");

        if (id == null)
            return NotFound();

        var tour = await _context.Tours
            .Include(t => t.TourHotels)
            .Include(t => t.TourSights)
            .FirstOrDefaultAsync(t => t.Id == id);

        if (tour == null)
            return NotFound();

        ViewBag.Countries = await _context.Countries.OrderBy(c => c.Name).ToListAsync();
        ViewBag.Agencies = await _context.Agencies.OrderBy(a => a.Name).ToListAsync();
        ViewBag.TourTypes = Enum.GetValues(typeof(TourType)).Cast<TourType>().ToList();
        ViewBag.Hotels = await _context.Hotels.Include(h => h.City).OrderBy(h => h.Name).ToListAsync();
        ViewBag.Sights = await _context.Sights.Include(s => s.City).OrderBy(s => s.Name).ToListAsync();
        ViewBag.SelectedHotels = tour.TourHotels.Select(th => th.HotelId).ToList();
        ViewBag.SelectedSights = tour.TourSights.Select(ts => ts.SightId).ToList();

        return View(tour);
    }

    // POST: /Admin/EditTour/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> EditTour(int id, Tour tour, IFormFile? photo, int[] selectedHotels, int[] selectedSights)
    {
        if (!IsAdmin())
            return RedirectToAction("Login", "Account");

        if (id != tour.Id)
            return NotFound();

        if (ModelState.IsValid)
        {
            var existingTour = await _context.Tours
                .AsNoTracking()
                .FirstOrDefaultAsync(t => t.Id == id);

            if (existingTour == null)
                return NotFound();

            if (photo != null && photo.Length > 0)
            {
                tour.PhotoUrl = await SavePhotoAsync(photo, "tours");
            }
            else
            {
                tour.PhotoUrl = existingTour.PhotoUrl;
            }

            tour.CreatedDate = existingTour.CreatedDate;
            _context.Update(tour);

            // Обновляем отели
            var existingHotels = await _context.TourHotels.Where(th => th.TourId == id).ToListAsync();
            _context.TourHotels.RemoveRange(existingHotels);
            foreach (var hotelId in selectedHotels)
            {
                _context.TourHotels.Add(new TourHotel { TourId = tour.Id, HotelId = hotelId });
            }

            // Обновляем достопримечательности
            var existingSights = await _context.TourSights.Where(ts => ts.TourId == id).ToListAsync();
            _context.TourSights.RemoveRange(existingSights);
            foreach (var sightId in selectedSights)
            {
                _context.TourSights.Add(new TourSight { TourId = tour.Id, SightId = sightId });
            }

            await _context.SaveChangesAsync();
            TempData["SuccessMessage"] = "Тур успешно обновлён";
            return RedirectToAction(nameof(Tours));
        }

        ViewBag.Countries = await _context.Countries.OrderBy(c => c.Name).ToListAsync();
        ViewBag.Agencies = await _context.Agencies.OrderBy(a => a.Name).ToListAsync();
        ViewBag.TourTypes = Enum.GetValues(typeof(TourType)).Cast<TourType>().ToList();
        ViewBag.Hotels = await _context.Hotels.Include(h => h.City).OrderBy(h => h.Name).ToListAsync();
        ViewBag.Sights = await _context.Sights.Include(s => s.City).OrderBy(s => s.Name).ToListAsync();
        return View(tour);
    }

    // POST: /Admin/DeleteTour/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteTour(int id)
    {
        if (!IsAdmin())
            return RedirectToAction("Login", "Account");

        var tour = await _context.Tours.FindAsync(id);
        if (tour != null)
        {
            _context.Tours.Remove(tour);
            await _context.SaveChangesAsync();
            TempData["SuccessMessage"] = "Тур удалён";
        }

        return RedirectToAction(nameof(Tours));
    }

    #endregion

    #region Управление отелями

    // GET: /Admin/Hotels
    public async Task<IActionResult> Hotels()
    {
        if (!IsAdmin())
            return RedirectToAction("Login", "Account");

        var hotels = await _context.Hotels
            .Include(h => h.City)
                .ThenInclude(c => c!.Country)
            .OrderBy(h => h.Name)
            .ToListAsync();

        return View(hotels);
    }

    // GET: /Admin/CreateHotel
    public async Task<IActionResult> CreateHotel()
    {
        if (!IsAdmin())
            return RedirectToAction("Login", "Account");

        ViewBag.Cities = await _context.Cities.Include(c => c.Country).OrderBy(c => c.Name).ToListAsync();
        ViewBag.MealTypes = Enum.GetValues(typeof(MealType)).Cast<MealType>().ToList();
        return View();
    }

    // POST: /Admin/CreateHotel
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateHotel(Hotel hotel, IFormFile? photo)
    {
        if (!IsAdmin())
            return RedirectToAction("Login", "Account");

        if (ModelState.IsValid)
        {
            if (photo != null && photo.Length > 0)
            {
                hotel.PhotoUrl = await SavePhotoAsync(photo, "hotels");
            }

            _context.Hotels.Add(hotel);
            await _context.SaveChangesAsync();
            TempData["SuccessMessage"] = "Отель успешно создан";
            return RedirectToAction(nameof(Hotels));
        }

        ViewBag.Cities = await _context.Cities.Include(c => c.Country).OrderBy(c => c.Name).ToListAsync();
        ViewBag.MealTypes = Enum.GetValues(typeof(MealType)).Cast<MealType>().ToList();
        return View(hotel);
    }

    // GET: /Admin/EditHotel/5
    public async Task<IActionResult> EditHotel(int? id)
    {
        if (!IsAdmin())
            return RedirectToAction("Login", "Account");

        if (id == null)
            return NotFound();

        var hotel = await _context.Hotels.FindAsync(id);
        if (hotel == null)
            return NotFound();

        ViewBag.Cities = await _context.Cities.Include(c => c.Country).OrderBy(c => c.Name).ToListAsync();
        ViewBag.MealTypes = Enum.GetValues(typeof(MealType)).Cast<MealType>().ToList();
        return View(hotel);
    }

    // POST: /Admin/EditHotel/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> EditHotel(int id, Hotel hotel, IFormFile? photo)
    {
        if (!IsAdmin())
            return RedirectToAction("Login", "Account");

        if (id != hotel.Id)
            return NotFound();

        if (ModelState.IsValid)
        {
            var existingHotel = await _context.Hotels.AsNoTracking().FirstOrDefaultAsync(h => h.Id == id);
            if (existingHotel == null)
                return NotFound();

            if (photo != null && photo.Length > 0)
            {
                hotel.PhotoUrl = await SavePhotoAsync(photo, "hotels");
            }
            else
            {
                hotel.PhotoUrl = existingHotel.PhotoUrl;
            }

            _context.Update(hotel);
            await _context.SaveChangesAsync();
            TempData["SuccessMessage"] = "Отель успешно обновлён";
            return RedirectToAction(nameof(Hotels));
        }

        ViewBag.Cities = await _context.Cities.Include(c => c.Country).OrderBy(c => c.Name).ToListAsync();
        ViewBag.MealTypes = Enum.GetValues(typeof(MealType)).Cast<MealType>().ToList();
        return View(hotel);
    }

    // POST: /Admin/DeleteHotel/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteHotel(int id)
    {
        if (!IsAdmin())
            return RedirectToAction("Login", "Account");

        var hotel = await _context.Hotels.FindAsync(id);
        if (hotel != null)
        {
            _context.Hotels.Remove(hotel);
            await _context.SaveChangesAsync();
            TempData["SuccessMessage"] = "Отель удалён";
        }

        return RedirectToAction(nameof(Hotels));
    }

    #endregion

    #region Управление достопримечательностями

    // GET: /Admin/Sights
    public async Task<IActionResult> Sights()
    {
        if (!IsAdmin())
            return RedirectToAction("Login", "Account");

        var sights = await _context.Sights
            .Include(s => s.City)
                .ThenInclude(c => c!.Country)
            .OrderBy(s => s.Name)
            .ToListAsync();

        return View(sights);
    }

    // GET: /Admin/CreateSight
    public async Task<IActionResult> CreateSight()
    {
        if (!IsAdmin())
            return RedirectToAction("Login", "Account");

        ViewBag.Cities = await _context.Cities.Include(c => c.Country).OrderBy(c => c.Name).ToListAsync();
        ViewBag.SightTypes = Enum.GetValues(typeof(SightType)).Cast<SightType>().ToList();
        return View();
    }

    // POST: /Admin/CreateSight
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateSight(Sight sight, IFormFile? photo)
    {
        if (!IsAdmin())
            return RedirectToAction("Login", "Account");

        if (ModelState.IsValid)
        {
            if (photo != null && photo.Length > 0)
            {
                sight.PhotoUrl = await SavePhotoAsync(photo, "sights");
            }

            _context.Sights.Add(sight);
            await _context.SaveChangesAsync();
            TempData["SuccessMessage"] = "Достопримечательность успешно создана";
            return RedirectToAction(nameof(Sights));
        }

        ViewBag.Cities = await _context.Cities.Include(c => c.Country).OrderBy(c => c.Name).ToListAsync();
        ViewBag.SightTypes = Enum.GetValues(typeof(SightType)).Cast<SightType>().ToList();
        return View(sight);
    }

    // GET: /Admin/EditSight/5
    public async Task<IActionResult> EditSight(int? id)
    {
        if (!IsAdmin())
            return RedirectToAction("Login", "Account");

        if (id == null)
            return NotFound();

        var sight = await _context.Sights.FindAsync(id);
        if (sight == null)
            return NotFound();

        ViewBag.Cities = await _context.Cities.Include(c => c.Country).OrderBy(c => c.Name).ToListAsync();
        ViewBag.SightTypes = Enum.GetValues(typeof(SightType)).Cast<SightType>().ToList();
        return View(sight);
    }

    // POST: /Admin/EditSight/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> EditSight(int id, Sight sight, IFormFile? photo)
    {
        if (!IsAdmin())
            return RedirectToAction("Login", "Account");

        if (id != sight.Id)
            return NotFound();

        if (ModelState.IsValid)
        {
            var existingSight = await _context.Sights.AsNoTracking().FirstOrDefaultAsync(s => s.Id == id);
            if (existingSight == null)
                return NotFound();

            if (photo != null && photo.Length > 0)
            {
                sight.PhotoUrl = await SavePhotoAsync(photo, "sights");
            }
            else
            {
                sight.PhotoUrl = existingSight.PhotoUrl;
            }

            _context.Update(sight);
            await _context.SaveChangesAsync();
            TempData["SuccessMessage"] = "Достопримечательность успешно обновлена";
            return RedirectToAction(nameof(Sights));
        }

        ViewBag.Cities = await _context.Cities.Include(c => c.Country).OrderBy(c => c.Name).ToListAsync();
        ViewBag.SightTypes = Enum.GetValues(typeof(SightType)).Cast<SightType>().ToList();
        return View(sight);
    }

    // POST: /Admin/DeleteSight/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteSight(int id)
    {
        if (!IsAdmin())
            return RedirectToAction("Login", "Account");

        var sight = await _context.Sights.FindAsync(id);
        if (sight != null)
        {
            _context.Sights.Remove(sight);
            await _context.SaveChangesAsync();
            TempData["SuccessMessage"] = "Достопримечательность удалена";
        }

        return RedirectToAction(nameof(Sights));
    }

    #endregion

    #region Вспомогательные методы

    private async Task<string> SavePhotoAsync(IFormFile photo, string folder)
    {
        var uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "uploads", folder);
        Directory.CreateDirectory(uploadsFolder);

        var uniqueFileName = $"{Guid.NewGuid()}_{photo.FileName}";
        var filePath = Path.Combine(uploadsFolder, uniqueFileName);

        using (var fileStream = new FileStream(filePath, FileMode.Create))
        {
            await photo.CopyToAsync(fileStream);
        }

        return $"/uploads/{folder}/{uniqueFileName}";
    }

    #endregion
}
