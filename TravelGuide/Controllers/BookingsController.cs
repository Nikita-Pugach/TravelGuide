using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TravelGuide.Data;
using TravelGuide.Models.Entities;
using TravelGuide.ViewModels;

namespace TravelGuide.Controllers;

public class BookingsController : Controller
{
    private readonly TravelGuideContext _context;

    public BookingsController(TravelGuideContext context)
    {
        _context = context;
    }

    // GET: /Bookings - Мои бронирования
    public async Task<IActionResult> Index()
    {
        var userId = HttpContext.Session.GetInt32("UserId");
        if (userId == null)
            return RedirectToAction("Login", "Account");

        var bookings = await _context.Bookings
            .Include(b => b.Tour)
                .ThenInclude(t => t!.Country)
            .Where(b => b.UserId == userId)
            .OrderByDescending(b => b.CreatedAt)
            .Select(b => new BookingListViewModel
            {
                Id = b.Id,
                TourId = b.TourId,
                TourName = b.Tour!.Name,
                TourPhotoUrl = b.Tour.PhotoUrl,
                CountryName = b.Tour.Country!.Name,
                BookingDate = b.BookingDate,
                GuestsCount = b.GuestsCount,
                TotalPrice = b.TotalPrice,
                Status = b.Status,
                CreatedAt = b.CreatedAt
            })
            .ToListAsync();

        return View(bookings);
    }

    // GET: /Bookings/Create/5
    public async Task<IActionResult> Create(int id)
    {
        var userId = HttpContext.Session.GetInt32("UserId");
        if (userId == null)
            return RedirectToAction("Login", "Account");

        var tour = await _context.Tours
            .Include(t => t.Country)
            .FirstOrDefaultAsync(t => t.Id == id);

        if (tour == null)
            return NotFound();

        var user = await _context.Users.FindAsync(userId);

        var vm = new BookingCreateViewModel
        {
            TourId = tour.Id,
            TourName = tour.Name,
            TourPrice = tour.Price,
            Duration = tour.Duration,
            TourPhotoUrl = tour.PhotoUrl,
            CountryName = tour.Country?.Name ?? "",
            Email = user?.Email
        };

        return View(vm);
    }

    // POST: /Bookings/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(BookingCreateViewModel vm)
    {
        var userId = HttpContext.Session.GetInt32("UserId");
        if (userId == null)
            return RedirectToAction("Login", "Account");

        var tour = await _context.Tours.FindAsync(vm.TourId);
        if (tour == null)
        {
            ModelState.AddModelError("", "Тур не найден");
            return View(vm);
        }

        if (!ModelState.IsValid)
        {
            vm.TourName = tour.Name;
            vm.TourPrice = tour.Price;
            vm.Duration = tour.Duration;
            vm.TourPhotoUrl = tour.PhotoUrl;
            vm.CountryName = tour.Country?.Name ?? "";
            return View(vm);
        }

        var booking = new Booking
        {
            TourId = vm.TourId,
            UserId = userId.Value,
            BookingDate = vm.BookingDate,
            GuestsCount = vm.GuestsCount,
            TotalPrice = tour.Price * vm.GuestsCount,
            Phone = vm.Phone,
            Email = vm.Email,
            Notes = vm.Notes,
            Status = BookingStatus.New,
            CreatedAt = DateTime.Now
        };

        _context.Bookings.Add(booking);
        await _context.SaveChangesAsync();

        TempData["Success"] = "Заявка на бронирование успешно создана!";
        return RedirectToAction(nameof(Index));
    }

    // POST: /Bookings/Cancel/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Cancel(int id)
    {
        var userId = HttpContext.Session.GetInt32("UserId");
        if (userId == null)
            return RedirectToAction("Login", "Account");

        var booking = await _context.Bookings.FindAsync(id);
        if (booking == null || booking.UserId != userId)
            return NotFound();

        if (booking.Status == BookingStatus.New)
        {
            booking.Status = BookingStatus.Cancelled;
            await _context.SaveChangesAsync();
            TempData["Success"] = "Заявка отменена";
        }

        return RedirectToAction(nameof(Index));
    }
}