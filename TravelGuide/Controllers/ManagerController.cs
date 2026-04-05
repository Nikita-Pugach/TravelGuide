using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TravelGuide.Data;
using TravelGuide.Models.Entities;
using TravelGuide.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace TravelGuide.Controllers
{
    [Authorize(Roles = "Manager")]
    public class ManagerController : Controller
    {
        private readonly TravelGuideContext _context;
        private readonly IRepository<Tour> _tourRepository;
        private readonly IRepository<Hotel> _hotelRepository;
        private readonly IRepository<Sight> _sightRepository;
        private readonly IRepository<Chat> _chatRepository;
        private readonly IRepository<Review> _reviewRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ManagerController(
            TravelGuideContext context,
            IRepository<Tour> tourRepository,
            IRepository<Hotel> hotelRepository,
            IRepository<Sight> sightRepository,
            IRepository<Chat> chatRepository,
            IRepository<Review> reviewRepository,
            IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _tourRepository = tourRepository;
            _hotelRepository = hotelRepository;
            _sightRepository = sightRepository;
            _chatRepository = chatRepository;
            _reviewRepository = reviewRepository;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: Manager
        public async Task<IActionResult> Index()
        {
            var tours = await _tourRepository.GetAllAsync();
            var chats = await _chatRepository.GetAllAsync();
            var reviews = await _reviewRepository.GetAllAsync();

            ViewBag.ToursCount = tours.Count();
            ViewBag.ActiveChatsCount = chats.Count(c => c.Status == ChatStatus.Active);
            ViewBag.PendingReviewsCount = reviews.Count(r => r.Status == ReviewStatus.Pending);
            ViewBag.TotalReviewsCount = reviews.Count(r => r.Status == ReviewStatus.Approved);

            // Популярные туры (по отзывам)
            var popularTours = tours
                .Where(t => t.Reviews != null && t.Reviews.Any())
                .OrderByDescending(t => t.Reviews.Count)
                .Take(5)
                .ToList();
            ViewBag.PopularTours = popularTours;

            // Активные чаты
            var activeChats = chats
                .Where(c => c.Status == ChatStatus.Active)
                .OrderByDescending(c => c.StartTime)
                .Take(5)
                .ToList();
            ViewBag.ActiveChats = activeChats;

            return View();
        }

        // GET: Manager/Tours
        public async Task<IActionResult> Tours(string search, int page = 1)
        {
            var tours = await _tourRepository.GetAllAsync();
            var hotels = await _hotelRepository.GetAllAsync();
            var sights = await _sightRepository.GetAllAsync();

            // Подгружаем связанные данные
            var tourIds = tours.Select(t => t.Id).ToList();
            var tourHotels = await _context.TourHotels
                .Where(th => tourIds.Contains(th.TourId))
                .Include(th => th.Hotel)
                .ToListAsync();
            var tourSights = await _context.TourSights
                .Where(ts => tourIds.Contains(ts.TourId))
                .Include(ts => ts.Sight)
                .ToListAsync();

            foreach (var tour in tours)
            {
                tour.TourHotels = tourHotels.Where(th => th.TourId == tour.Id).ToList();
                tour.TourSights = tourSights.Where(ts => ts.TourId == tour.Id).ToList();
            }

            // Поиск
            if (!string.IsNullOrEmpty(search))
            {
                tours = tours.Where(t => 
                    t.Name.Contains(search, StringComparison.OrdinalIgnoreCase) ||
                    (t.Description != null && t.Description.Contains(search, StringComparison.OrdinalIgnoreCase)));
            }

            // Пагинация
            int pageSize = 10;
            int totalCount = tours.Count();
            int totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);

            var pagedTours = tours
                .OrderByDescending(t => t.Id)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = totalPages;
            ViewBag.Search = search;

            return View(pagedTours);
        }

        // GET: Manager/CreateTour
        public async Task<IActionResult> CreateTour()
        {
            ViewBag.Countries = await _context.Countries.OrderBy(c => c.Name).ToListAsync();
            ViewBag.Cities = await _context.Cities.OrderBy(c => c.Name).ToListAsync();
            ViewBag.Agencies = await _context.Agencies.OrderBy(a => a.Name).ToListAsync();
            ViewBag.Hotels = await _hotelRepository.GetAllAsync();
            ViewBag.Sights = await _sightRepository.GetAllAsync();
            ViewBag.TourTypes = Enum.GetValues(typeof(TourType)).Cast<TourType>().ToList();

            return View();
        }

        // POST: Manager/CreateTour
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateTour(Tour tour, int[] selectedHotels, int[] selectedSights, List<IFormFile> images)
        {
            if (ModelState.IsValid)
            {
                // Обработка изображений
                if (images != null && images.Count > 0)
                {
                    var image = images.FirstOrDefault(i => i.Length > 0);
                    if (image != null)
                    {
                        var uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "uploads/tours");
                        Directory.CreateDirectory(uploadsFolder);
                        var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(image.FileName);
                        var filePath = Path.Combine(uploadsFolder, uniqueFileName);
                        using (var fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            await image.CopyToAsync(fileStream);
                        }
                        tour.PhotoUrl = "/uploads/tours/" + uniqueFileName;
                    }
                }

                tour.CreatedDate = DateTime.Now;

                await _tourRepository.AddAsync(tour);

                // Добавление отелей
                if (selectedHotels != null)
                {
                    foreach (var hotelId in selectedHotels)
                    {
                        _context.TourHotels.Add(new TourHotel { TourId = tour.Id, HotelId = hotelId });
                    }
                }

                // Добавление достопримечательностей
                if (selectedSights != null)
                {
                    foreach (var sightId in selectedSights)
                    {
                        _context.TourSights.Add(new TourSight { TourId = tour.Id, SightId = sightId });
                    }
                }

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Tours));
            }

            ViewBag.Countries = await _context.Countries.OrderBy(c => c.Name).ToListAsync();
            ViewBag.Cities = await _context.Cities.OrderBy(c => c.Name).ToListAsync();
            ViewBag.Agencies = await _context.Agencies.OrderBy(a => a.Name).ToListAsync();
            ViewBag.Hotels = await _hotelRepository.GetAllAsync();
            ViewBag.Sights = await _sightRepository.GetAllAsync();
            ViewBag.TourTypes = Enum.GetValues(typeof(TourType)).Cast<TourType>().ToList();

            return View(tour);
        }

        // GET: Manager/EditTour/5
        public async Task<IActionResult> EditTour(int id)
        {
            var tour = await _tourRepository.GetByIdAsync(id);
            if (tour == null)
            {
                return NotFound();
            }

            // Подгружаем связанные данные
            tour.TourHotels = await _context.TourHotels
                .Where(th => th.TourId == id)
                .Include(th => th.Hotel)
                .ToListAsync();
            tour.TourSights = await _context.TourSights
                .Where(ts => ts.TourId == id)
                .Include(ts => ts.Sight)
                .ToListAsync();

            ViewBag.Countries = await _context.Countries.OrderBy(c => c.Name).ToListAsync();
            ViewBag.Cities = await _context.Cities.OrderBy(c => c.Name).ToListAsync();
            ViewBag.Agencies = await _context.Agencies.OrderBy(a => a.Name).ToListAsync();
            ViewBag.Hotels = await _hotelRepository.GetAllAsync();
            ViewBag.Sights = await _sightRepository.GetAllAsync();
            ViewBag.TourTypes = Enum.GetValues(typeof(TourType)).Cast<TourType>().ToList();
            ViewBag.SelectedHotels = tour.TourHotels.Select(th => th.HotelId).ToList();
            ViewBag.SelectedSights = tour.TourSights.Select(ts => ts.SightId).ToList();

            return View(tour);
        }

        // POST: Manager/EditTour/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditTour(int id, Tour tour, int[] selectedHotels, int[] selectedSights, List<IFormFile> images, bool removeImages = false)
        {
            if (id != tour.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var existingTour = await _tourRepository.GetByIdAsync(id);
                if (existingTour == null)
                {
                    return NotFound();
                }

                // Обработка изображений
                if (removeImages)
                {
                    existingTour.PhotoUrl = null;
                }
                
                if (images != null && images.Count > 0)
                {
                    var image = images.FirstOrDefault(i => i.Length > 0);
                    if (image != null)
                    {
                        var uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "uploads/tours");
                        Directory.CreateDirectory(uploadsFolder);
                        var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(image.FileName);
                        var filePath = Path.Combine(uploadsFolder, uniqueFileName);
                        using (var fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            await image.CopyToAsync(fileStream);
                        }
                        existingTour.PhotoUrl = "/uploads/tours/" + uniqueFileName;
                    }
                }

                existingTour.Name = tour.Name;
                existingTour.Description = tour.Description;
                existingTour.TourType = tour.TourType;
                existingTour.Duration = tour.Duration;
                existingTour.Price = tour.Price;
                existingTour.CountryId = tour.CountryId;
                existingTour.AgencyId = tour.AgencyId;
                existingTour.Route = tour.Route;
                existingTour.StartDate = tour.StartDate;
                existingTour.EndDate = tour.EndDate;
                existingTour.IsSeasonal = tour.IsSeasonal;

                await _tourRepository.UpdateAsync(existingTour);

                // Обновление отелей
                var existingHotels = await _context.TourHotels.Where(th => th.TourId == id).ToListAsync();
                _context.TourHotels.RemoveRange(existingHotels);
                if (selectedHotels != null)
                {
                    foreach (var hotelId in selectedHotels)
                    {
                        _context.TourHotels.Add(new TourHotel { TourId = id, HotelId = hotelId });
                    }
                }

                // Обновление достопримечательностей
                var existingSights = await _context.TourSights.Where(ts => ts.TourId == id).ToListAsync();
                _context.TourSights.RemoveRange(existingSights);
                if (selectedSights != null)
                {
                    foreach (var sightId in selectedSights)
                    {
                        _context.TourSights.Add(new TourSight { TourId = id, SightId = sightId });
                    }
                }

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Tours));
            }

            ViewBag.Countries = await _context.Countries.OrderBy(c => c.Name).ToListAsync();
            ViewBag.Cities = await _context.Cities.OrderBy(c => c.Name).ToListAsync();
            ViewBag.Agencies = await _context.Agencies.OrderBy(a => a.Name).ToListAsync();
            ViewBag.Hotels = await _hotelRepository.GetAllAsync();
            ViewBag.Sights = await _sightRepository.GetAllAsync();
            ViewBag.TourTypes = Enum.GetValues(typeof(TourType)).Cast<TourType>().ToList();

            return View(tour);
        }

        // POST: Manager/DeleteTour/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteTour(int id)
        {
            var tour = await _tourRepository.GetByIdAsync(id);
            if (tour != null)
            {
                await _tourRepository.DeleteAsync(id);
            }
            return RedirectToAction(nameof(Tours));
        }

        // GET: Manager/Chats
        public async Task<IActionResult> Chats(ChatStatus? status)
        {
            var chats = await _chatRepository.GetAllAsync();

            // Подгружаем пользователей
            var chatIds = chats.Select(c => c.Id).ToList();
            var messages = await _context.Messages
                .Where(m => chatIds.Contains(m.ChatId))
                .OrderByDescending(m => m.Timestamp)
                .ToListAsync();

            foreach (var chat in chats)
            {
                chat.Messages = messages.Where(m => m.ChatId == chat.Id).ToList();
            }

            if (status.HasValue)
            {
                chats = chats.Where(c => c.Status == status.Value);
            }

            chats = chats.OrderByDescending(c => c.StartTime).ToList();

            ViewBag.Status = status;
            return View(chats);
        }

        // GET: Manager/Chat/5
        public async Task<IActionResult> Chat(int id)
        {
            var chat = await _chatRepository.GetByIdAsync(id);
            if (chat == null)
            {
                return NotFound();
            }

            chat.Messages = await _context.Messages
                .Where(m => m.ChatId == id)
                .OrderBy(m => m.Timestamp)
                .ToListAsync();

            return View(chat);
        }

        // POST: Manager/CloseChat/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CloseChat(int id)
        {
            var chat = await _chatRepository.GetByIdAsync(id);
            if (chat != null)
            {
                chat.Status = ChatStatus.Closed;
                chat.EndTime = DateTime.Now;
                await _chatRepository.UpdateAsync(chat);
            }
            return RedirectToAction(nameof(Chats));
        }

        // GET: Manager/Statistics
        public async Task<IActionResult> Statistics()
        {
            var tours = await _tourRepository.GetAllAsync();
            var reviews = await _reviewRepository.GetAllAsync();

            var stats = tours.Select(t => new TourStatistics
            {
                TourId = t.Id,
                TourName = t.Name,
                ViewsCount = t.ViewCount,
                ReviewsCount = reviews.Count(r => r.TourId == t.Id && r.Status == ReviewStatus.Approved),
                AverageRating = reviews.Where(r => r.TourId == t.Id && r.Status == ReviewStatus.Approved)
                    .Select(r => r.Rating)
                    .DefaultIfEmpty(0)
                    .Average()
            }).OrderByDescending(s => s.ViewsCount).ToList();

            return View(stats);
        }
    }

    public class TourStatistics
    {
        public int TourId { get; set; }
        public string TourName { get; set; } = string.Empty;
        public int ViewsCount { get; set; }
        public int ReviewsCount { get; set; }
        public double AverageRating { get; set; }
    }
}