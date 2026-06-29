using Microsoft.EntityFrameworkCore;
using TravelGuide.Data;
using TravelGuide.Models.Entities;

namespace TravelGuide.Services;

/// <summary>
/// Сервис для формирования отчётов
/// </summary>
public class ReportService
{
    private readonly TravelGuideContext _context;

    public ReportService(TravelGuideContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Отчёт по популярным турам (по просмотрам и отзывам)
    /// </summary>
    public async Task<List<PopularTourReport>> GetPopularToursAsync(DateTime? dateFrom, DateTime? dateTo)
    {
        var query = _context.Tours
            .Include(t => t.Country)
            .Include(t => t.Agency)
            .Include(t => t.Reviews)
            .AsQueryable();

        if (dateFrom.HasValue)
            query = query.Where(t => t.CreatedDate >= dateFrom.Value);

        if (dateTo.HasValue)
            query = query.Where(t => t.CreatedDate <= dateTo.Value);

        return await query.Select(t => new PopularTourReport
        {
            TourId = t.Id,
            TourName = t.Name,
            CountryName = t.Country != null ? t.Country.Name : "—",
            AgencyName = t.Agency != null ? t.Agency.Name : "—",
            Price = t.Price,
            Duration = t.Duration,
            ReviewCount = t.Reviews.Count,
            AverageRating = t.Reviews.Any() ? t.Reviews.Average(r => r.Rating) : 0,
            CreatedDate = t.CreatedDate
        })
        .OrderByDescending(r => r.ReviewCount)
        .ThenByDescending(r => r.AverageRating)
        .ToListAsync();
    }

    /// <summary>
    /// Отчёт по активности пользователей
    /// </summary>
    public async Task<UserActivityReport> GetUserActivityAsync(DateTime? dateFrom, DateTime? dateTo)
    {
        var usersQuery = _context.Users.AsQueryable();
        var reviewsQuery = _context.Reviews.AsQueryable();
        var chatsQuery = _context.Chats.AsQueryable();
        var bookingsQuery = _context.Bookings.AsQueryable();

        if (dateFrom.HasValue)
        {
            usersQuery = usersQuery.Where(u => u.RegistrationDate >= dateFrom.Value);
            reviewsQuery = reviewsQuery.Where(r => r.Date >= dateFrom.Value);
            chatsQuery = chatsQuery.Where(c => c.StartTime >= dateFrom.Value);
            bookingsQuery = bookingsQuery.Where(b => b.CreatedAt >= dateFrom.Value);
        }

        if (dateTo.HasValue)
        {
            usersQuery = usersQuery.Where(u => u.RegistrationDate <= dateTo.Value);
            reviewsQuery = reviewsQuery.Where(r => r.Date <= dateTo.Value);
            chatsQuery = chatsQuery.Where(c => c.StartTime <= dateTo.Value);
            bookingsQuery = bookingsQuery.Where(b => b.CreatedAt <= dateTo.Value);
        }

        var totalUsers = await _context.Users.CountAsync();
        var newUsers = await usersQuery.CountAsync();
        var totalReviews = await reviewsQuery.CountAsync();
        var approvedReviews = await reviewsQuery.CountAsync(r => r.Status == ReviewStatus.Approved);
        var totalChats = await chatsQuery.CountAsync();
        var activeChats = await chatsQuery.CountAsync(c => c.Status == ChatStatus.Active);
        var totalBookings = await bookingsQuery.CountAsync();

        // Регистрации по месяцам
        var registrationsByMonth = await _context.Users
            .Where(u => (!dateFrom.HasValue || u.RegistrationDate >= dateFrom.Value) &&
                        (!dateTo.HasValue || u.RegistrationDate <= dateTo.Value))
            .GroupBy(u => new { u.RegistrationDate.Year, u.RegistrationDate.Month })
            .Select(g => new MonthlyStat
            {
                Year = g.Key.Year,
                Month = g.Key.Month,
                Count = g.Count()
            })
            .OrderBy(s => s.Year).ThenBy(s => s.Month)
            .ToListAsync();

        return new UserActivityReport
        {
            TotalUsers = totalUsers,
            NewUsers = newUsers,
            TotalReviews = totalReviews,
            ApprovedReviews = approvedReviews,
            TotalChats = totalChats,
            ActiveChats = activeChats,
            TotalBookings = totalBookings,
            RegistrationsByMonth = registrationsByMonth,
            DateFrom = dateFrom,
            DateTo = dateTo
        };
    }

    /// <summary>
    /// Отчёт по статистике отзывов
    /// </summary>
    public async Task<ReviewStatsReport> GetReviewStatsAsync(DateTime? dateFrom, DateTime? dateTo)
    {
        var reviewsQuery = _context.Reviews
            .Include(r => r.Tour)
            .Include(r => r.Hotel)
            .Include(r => r.Sight)
            .AsQueryable();

        if (dateFrom.HasValue)
            reviewsQuery = reviewsQuery.Where(r => r.Date >= dateFrom.Value);
        if (dateTo.HasValue)
            reviewsQuery = reviewsQuery.Where(r => r.Date <= dateTo.Value);

        var allReviews = await reviewsQuery.ToListAsync();

        // Средняя оценка по турам
        var tourStats = allReviews
            .Where(r => r.Tour != null)
            .GroupBy(r => new { r.TourId, TourName = r.Tour!.Name })
            .Select(g => new ObjectRatingStat
            {
                ObjectId = g.Key.TourId ?? 0,
                ObjectName = g.Key.TourName,
                ReviewCount = g.Count(),
                AverageRating = g.Average(r => r.Rating)
            })
            .OrderByDescending(s => s.AverageRating)
            .ToList();

        // Средняя оценка по отелям
        var hotelStats = allReviews
            .Where(r => r.Hotel != null)
            .GroupBy(r => new { r.HotelId, HotelName = r.Hotel!.Name })
            .Select(g => new ObjectRatingStat
            {
                ObjectId = g.Key.HotelId ?? 0,
                ObjectName = g.Key.HotelName,
                ReviewCount = g.Count(),
                AverageRating = g.Average(r => r.Rating)
            })
            .OrderByDescending(s => s.AverageRating)
            .ToList();

        // Средняя оценка по достопримечательностям
        var sightStats = allReviews
            .Where(r => r.Sight != null)
            .GroupBy(r => new { r.SightId, SightName = r.Sight!.Name })
            .Select(g => new ObjectRatingStat
            {
                ObjectId = g.Key.SightId ?? 0,
                ObjectName = g.Key.SightName,
                ReviewCount = g.Count(),
                AverageRating = g.Average(r => r.Rating)
            })
            .OrderByDescending(s => s.AverageRating)
            .ToList();

        return new ReviewStatsReport
        {
            TotalReviews = allReviews.Count,
            AverageRating = allReviews.Any() ? allReviews.Average(r => r.Rating) : 0,
            TourStats = tourStats,
            HotelStats = hotelStats,
            SightStats = sightStats,
            DateFrom = dateFrom,
            DateTo = dateTo
        };
    }

    /// <summary>
    /// Отчёт по работе менеджеров
    /// </summary>
    public async Task<List<ManagerPerformanceReport>> GetManagerPerformanceAsync(DateTime? dateFrom, DateTime? dateTo)
    {
        var managers = await _context.Users
            .Where(u => u.Role == UserRole.Manager)
            .ToListAsync();

        var result = new List<ManagerPerformanceReport>();

        foreach (var manager in managers)
        {
            // Запрашиваем чаты напрямую из БД, а не из in-memory коллекции
            var chatsQuery = _context.Chats
                .Where(c => c.ManagerId == manager.Id);

            if (dateFrom.HasValue)
                chatsQuery = chatsQuery.Where(c => c.StartTime >= dateFrom.Value);
            if (dateTo.HasValue)
                chatsQuery = chatsQuery.Where(c => c.StartTime <= dateTo.Value);

            var chats = await chatsQuery.ToListAsync();
            var totalChats = chats.Count;
            var closedChats = chats.Count(c => c.Status == ChatStatus.Closed);

            // Среднее время ответа (в минутах)
            var chatIds = chats.Select(c => c.Id).ToList();
            var messages = await _context.Messages
                .Where(m => chatIds.Contains(m.ChatId) && m.SenderId == manager.Id)
                .GroupBy(m => m.ChatId)
                .Select(g => new
                {
                    ChatId = g.Key,
                    FirstReply = g.Min(m => m.Timestamp)
                })
                .ToListAsync();

            var chatStarts = chats.ToDictionary(c => c.Id, c => c.StartTime);
            var avgResponseMinutes = messages.Any()
                ? messages.Average(m => (m.FirstReply - chatStarts[m.ChatId]).TotalMinutes)
                : 0;

            result.Add(new ManagerPerformanceReport
            {
                ManagerId = manager.Id,
                ManagerName = manager.FullName,
                Email = manager.Email,
                TotalChats = totalChats,
                ClosedChats = closedChats,
                AverageResponseMinutes = Math.Round(avgResponseMinutes, 1)
            });
        }

        return result.OrderByDescending(r => r.TotalChats).ToList();
    }

    /// <summary>
    /// Отчёт по агентствам
    /// </summary>
    public async Task<List<AgencyReport>> GetAgencyReportAsync()
    {
        return await _context.Agencies
            .Include(a => a.Tours)
            .ThenInclude(t => t.Reviews)
            .Select(a => new AgencyReport
            {
                AgencyId = a.Id,
                AgencyName = a.Name,
                ContactInfo = a.ContactInfo,
                Rating = a.Rating ?? 0,
                TourCount = a.Tours.Count,
                TotalReviews = a.Tours.SelectMany(t => t.Reviews).Count(),
                AverageTourRating = a.Tours.SelectMany(t => t.Reviews).Any()
                    ? a.Tours.SelectMany(t => t.Reviews).Average(r => r.Rating)
                    : 0
            })
            .OrderByDescending(r => r.Rating)
            .ToListAsync();
    }
}

// ========================
// Модели отчётов
// ========================

public class PopularTourReport
{
    public int TourId { get; set; }
    public string TourName { get; set; } = "";
    public string CountryName { get; set; } = "";
    public string AgencyName { get; set; } = "";
    public decimal Price { get; set; }
    public int Duration { get; set; }
    public int ReviewCount { get; set; }
    public double AverageRating { get; set; }
    public DateTime CreatedDate { get; set; }
}

public class UserActivityReport
{
    public int TotalUsers { get; set; }
    public int NewUsers { get; set; }
    public int TotalReviews { get; set; }
    public int ApprovedReviews { get; set; }
    public int TotalChats { get; set; }
    public int ActiveChats { get; set; }
    public int TotalBookings { get; set; }
    public List<MonthlyStat> RegistrationsByMonth { get; set; } = new();
    public DateTime? DateFrom { get; set; }
    public DateTime? DateTo { get; set; }
}

public class MonthlyStat
{
    public int Year { get; set; }
    public int Month { get; set; }
    public int Count { get; set; }
    public string MonthName => Month switch
    {
        1 => "Янв", 2 => "Фев", 3 => "Мар", 4 => "Апр",
        5 => "Май", 6 => "Июн", 7 => "Июл", 8 => "Авг",
        9 => "Сен", 10 => "Окт", 11 => "Ноя", 12 => "Дек",
        _ => ""
    };
}

public class ReviewStatsReport
{
    public int TotalReviews { get; set; }
    public double AverageRating { get; set; }
    public List<ObjectRatingStat> TourStats { get; set; } = new();
    public List<ObjectRatingStat> HotelStats { get; set; } = new();
    public List<ObjectRatingStat> SightStats { get; set; } = new();
    public DateTime? DateFrom { get; set; }
    public DateTime? DateTo { get; set; }
}

public class ObjectRatingStat
{
    public int ObjectId { get; set; }
    public string ObjectName { get; set; } = "";
    public int ReviewCount { get; set; }
    public double AverageRating { get; set; }
}

public class ManagerPerformanceReport
{
    public int ManagerId { get; set; }
    public string ManagerName { get; set; } = "";
    public string Email { get; set; } = "";
    public int TotalChats { get; set; }
    public int ClosedChats { get; set; }
    public double AverageResponseMinutes { get; set; }
}

public class AgencyReport
{
    public int AgencyId { get; set; }
    public string AgencyName { get; set; } = "";
    public string ContactInfo { get; set; } = "";
    public double Rating { get; set; }
    public int TourCount { get; set; }
    public int TotalReviews { get; set; }
    public double AverageTourRating { get; set; }
}
