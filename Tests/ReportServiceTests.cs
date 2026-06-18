using Microsoft.EntityFrameworkCore;
using TravelGuide.Data;
using TravelGuide.Models.Entities;
using TravelGuide.Services;
using Xunit;

namespace TravelGuide.Tests;

/// <summary>
/// Unit-тесты для ReportService
/// </summary>
public class ReportServiceTests
{
    private TravelGuideContext GetInMemoryContext()
    {
        var options = new DbContextOptionsBuilder<TravelGuideContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
        return new TravelGuideContext(options);
    }

    private async Task SeedData(TravelGuideContext context)
    {
        // Страна
        var country = new Country { Name = "Египет" };
        context.Countries.Add(country);
        await context.SaveChangesAsync();

        // Агентство
        var agency = new Agency { Name = "TravelPro", ContactInfo = "info@travelpro.ru", Rating = 4.5 };
        context.Agencies.Add(agency);
        await context.SaveChangesAsync();

        // Пользователи
        var user1 = new User { Email = "user1@test.com", FullName = "Иван Иванов", Role = UserRole.Tourist, RegistrationDate = DateTime.Now.AddDays(-30) };
        var user2 = new User { Email = "user2@test.com", FullName = "Пётр Петров", Role = UserRole.Tourist, RegistrationDate = DateTime.Now.AddDays(-10) };
        var manager = new User { Email = "manager@test.com", FullName = "Менеджер Менеджеров", Role = UserRole.Manager, RegistrationDate = DateTime.Now.AddDays(-60) };
        context.Users.AddRange(user1, user2, manager);
        await context.SaveChangesAsync();

        // Туры
        var tour1 = new Tour
        {
            Name = "Отдых в Хургаде",
            TourType = TourType.Beach,
            Duration = 7,
            Price = 45000,
            CountryId = country.Id,
            AgencyId = agency.Id,
            CreatedDate = DateTime.Now.AddDays(-20)
        };
        var tour2 = new Tour
        {
            Name = "Экскурсия по Каиру",
            TourType = TourType.Excursion,
            Duration = 3,
            Price = 15000,
            CountryId = country.Id,
            AgencyId = agency.Id,
            CreatedDate = DateTime.Now.AddDays(-5)
        };
        context.Tours.AddRange(tour1, tour2);
        await context.SaveChangesAsync();

        // Отзывы
        var review1 = new Review { UserId = user1.Id, TourId = tour1.Id, Rating = 5, Text = "Отличный тур!", Status = ReviewStatus.Approved, Date = DateTime.Now.AddDays(-15) };
        var review2 = new Review { UserId = user2.Id, TourId = tour1.Id, Rating = 4, Text = "Хорошо, но дорого", Status = ReviewStatus.Approved, Date = DateTime.Now.AddDays(-10) };
        var review3 = new Review { UserId = user1.Id, TourId = tour2.Id, Rating = 3, Text = "Средне", Status = ReviewStatus.Pending, Date = DateTime.Now.AddDays(-3) };
        context.Reviews.AddRange(review1, review2, review3);
        await context.SaveChangesAsync();

        // Бронирования
        var booking1 = new Booking { TourId = tour1.Id, UserId = user1.Id, BookingDate = DateTime.Now.AddDays(14), GuestsCount = 2, TotalPrice = 90000, Status = BookingStatus.New, Phone = "+79001234567", CreatedAt = DateTime.Now.AddDays(-10) };
        var booking2 = new Booking { TourId = tour2.Id, UserId = user2.Id, BookingDate = DateTime.Now.AddDays(7), GuestsCount = 1, TotalPrice = 15000, Status = BookingStatus.Confirmed, Phone = "+79007654321", CreatedAt = DateTime.Now.AddDays(-5) };
        context.Bookings.AddRange(booking1, booking2);
        await context.SaveChangesAsync();

        // Чаты
        var chat1 = new Chat { UserId = user1.Id, ManagerId = manager.Id, StartTime = DateTime.Now.AddDays(-7), Status = ChatStatus.Active };
        var chat2 = new Chat { UserId = user2.Id, StartTime = DateTime.Now.AddDays(-3), Status = ChatStatus.Active };
        context.Chats.AddRange(chat1, chat2);
        await context.SaveChangesAsync();

        // Сообщения
        var msg1 = new Message { ChatId = chat1.Id, SenderId = user1.Id, Text = "Помогите с бронированием", Timestamp = DateTime.Now.AddDays(-7), IsRead = true };
        var msg2 = new Message { ChatId = chat1.Id, SenderId = manager.Id, Text = "Конечно, какой тур интересует?", Timestamp = DateTime.Now.AddDays(-7).AddMinutes(5), IsRead = true };
        context.Messages.AddRange(msg1, msg2);
        await context.SaveChangesAsync();
    }

    [Fact]
    public async Task GetPopularToursAsync_ReturnsToursOrderedByReviews()
    {
        await using var context = GetInMemoryContext();
        await SeedData(context);
        var service = new ReportService(context);

        var result = await service.GetPopularToursAsync(null, null);

        Assert.Equal(2, result.Count);
        // Тур с 2 отзывами должен быть первым
        Assert.Equal("Отдых в Хургаде", result[0].TourName);
        Assert.Equal(2, result[0].ReviewCount);
        Assert.Equal(4.5, result[0].AverageRating, 1); // (5+4)/2
    }

    [Fact]
    public async Task GetPopularToursAsync_WithDateFilter_FiltersCorrectly()
    {
        await using var context = GetInMemoryContext();
        await SeedData(context);
        var service = new ReportService(context);

        // Только туры за последние 10 дней
        var result = await service.GetPopularToursAsync(DateTime.Now.AddDays(-10), null);

        Assert.Single(result);
        Assert.Equal("Экскурсия по Каиру", result[0].TourName);
    }

    [Fact]
    public async Task GetPopularToursAsync_EmptyDatabase_ReturnsEmpty()
    {
        await using var context = GetInMemoryContext();
        var service = new ReportService(context);

        var result = await service.GetPopularToursAsync(null, null);

        Assert.Empty(result);
    }

    [Fact]
    public async Task GetUserActivityAsync_ReturnsCorrectStats()
    {
        await using var context = GetInMemoryContext();
        await SeedData(context);
        var service = new ReportService(context);

        var result = await service.GetUserActivityAsync(null, null);

        Assert.Equal(3, result.TotalUsers); // user1, user2, manager
        Assert.Equal(3, result.TotalReviews);
        Assert.Equal(2, result.ApprovedReviews); // 2 approved, 1 pending
        Assert.Equal(2, result.TotalChats);
        Assert.Equal(2, result.ActiveChats); // оба чата Active
        Assert.Equal(2, result.TotalBookings);
    }

    [Fact]
    public async Task GetUserActivityAsync_WithDateFilter_OnlyCountsInRange()
    {
        await using var context = GetInMemoryContext();
        await SeedData(context);
        var service = new ReportService(context);

        // Только за последние 15 дней
        var result = await service.GetUserActivityAsync(DateTime.Now.AddDays(-15), null);

        Assert.Equal(3, result.TotalUsers); // все 3 зарегистрировались > 15 дней назад
        Assert.True(result.TotalReviews <= 3);
    }

    [Fact]
    public async Task GetReviewStatsAsync_ReturnsCorrectAverages()
    {
        await using var context = GetInMemoryContext();
        await SeedData(context);
        var service = new ReportService(context);

        var result = await service.GetReviewStatsAsync(null, null);

        Assert.Equal(3, result.TotalReviews);
        // (5 + 4 + 3) / 3 = 4.0
        Assert.Equal(4.0, result.AverageRating, 1);
        Assert.NotEmpty(result.TourStats);
    }

    [Fact]
    public async Task GetReviewStatsAsync_TourStatsOrderedByRating()
    {
        await using var context = GetInMemoryContext();
        await SeedData(context);
        var service = new ReportService(context);

        var result = await service.GetReviewStatsAsync(null, null);

        // Тур с рейтингом 4.5 (5+4)/2 должен быть выше, чем тур с рейтингом 3
        Assert.Equal("Отдых в Хургаде", result.TourStats[0].ObjectName);
        Assert.Equal(2, result.TourStats[0].ReviewCount);
    }

    [Fact]
    public async Task GetManagerPerformanceAsync_EmptyDatabase_ReturnsEmpty()
    {
        await using var context = GetInMemoryContext();
        var service = new ReportService(context);

        var result = await service.GetManagerPerformanceAsync(null, null);

        Assert.Empty(result);
    }

    [Fact]
    public async Task GetAgencyReportAsync_ReturnsAgenciesWithStats()
    {
        await using var context = GetInMemoryContext();
        await SeedData(context);
        var service = new ReportService(context);

        var result = await service.GetAgencyReportAsync();

        Assert.Single(result);
        Assert.Equal("TravelPro", result[0].AgencyName);
        Assert.Equal(2, result[0].TourCount);
        Assert.Equal(3, result[0].TotalReviews); // все 3 отзыва у туров этого агентства
    }

    [Fact]
    public async Task GetAgencyReportAsync_EmptyDatabase_ReturnsEmpty()
    {
        await using var context = GetInMemoryContext();
        var service = new ReportService(context);

        var result = await service.GetAgencyReportAsync();

        Assert.Empty(result);
    }
}
