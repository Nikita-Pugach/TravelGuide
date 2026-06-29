using Microsoft.AspNetCore.Mvc;
using TravelGuide.Services;

namespace TravelGuide.Controllers;

/// <summary>
/// Контроллер для формирования и экспорта отчётов
/// </summary>
public class ReportsController : Controller
{
    private readonly ReportService _reportService;

    public ReportsController(ReportService reportService)
    {
        _reportService = reportService;
    }

    private bool IsLoggedIn() => HttpContext.Session.GetInt32("UserId") != null;

    /// <summary>
    /// Главная страница отчётов — выбор типа
    /// </summary>
    public IActionResult Index()
    {
        if (!IsLoggedIn()) return RedirectToAction("Login", "Account");
        return View();
    }

    // =============================================
    // 1. Популярные туры
    // =============================================
    public async Task<IActionResult> PopularTours(DateTime? dateFrom, DateTime? dateTo)
    {
        if (!IsLoggedIn()) return RedirectToAction("Login", "Account");
        var data = await _reportService.GetPopularToursAsync(dateFrom, dateTo);
        ViewBag.DateFrom = dateFrom;
        ViewBag.DateTo = dateTo;
        return View(data);
    }

    [HttpPost]
    public async Task<IActionResult> ExportPopularToursExcel(DateTime? dateFrom, DateTime? dateTo)
    {
        var data = await _reportService.GetPopularToursAsync(dateFrom, dateTo);
        using var workbook = new ClosedXML.Excel.XLWorkbook();
        var ws = workbook.Worksheets.Add("Популярные туры");

        ws.Cell(1, 1).Value = "№";
        ws.Cell(1, 2).Value = "Название тура";
        ws.Cell(1, 3).Value = "Страна";
        ws.Cell(1, 4).Value = "Агентство";
        ws.Cell(1, 5).Value = "Цена (₽)";
        ws.Cell(1, 6).Value = "Длительность";
        ws.Cell(1, 7).Value = "Кол-во отзывов";
        ws.Cell(1, 8).Value = "Средняя оценка";

        var headerRange = ws.Range(1, 1, 1, 8);
        headerRange.Style.Font.Bold = true;
        headerRange.Style.Fill.BackgroundColor = ClosedXML.Excel.XLColor.FromHtml("#4a90d9");
        headerRange.Style.Font.FontColor = ClosedXML.Excel.XLColor.White;

        for (int i = 0; i < data.Count; i++)
        {
            ws.Cell(i + 2, 1).Value = i + 1;
            ws.Cell(i + 2, 2).Value = data[i].TourName;
            ws.Cell(i + 2, 3).Value = data[i].CountryName;
            ws.Cell(i + 2, 4).Value = data[i].AgencyName;
            ws.Cell(i + 2, 5).Value = (double)data[i].Price;
            ws.Cell(i + 2, 6).Value = data[i].Duration;
            ws.Cell(i + 2, 7).Value = data[i].ReviewCount;
            ws.Cell(i + 2, 8).Value = Math.Round(data[i].AverageRating, 2);
        }

        ws.Columns().AdjustToContents();

        using var stream = new MemoryStream();
        workbook.SaveAs(stream);
        stream.Position = 0;
        return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
            $"Популярные_туры_{DateTime.Now:yyyyMMdd}.xlsx");
    }

    // =============================================
    // 2. Активность пользователей
    // =============================================
    public async Task<IActionResult> UserActivity(DateTime? dateFrom, DateTime? dateTo)
    {
        if (!IsLoggedIn()) return RedirectToAction("Login", "Account");
        var data = await _reportService.GetUserActivityAsync(dateFrom, dateTo);
        return View(data);
    }

    [HttpPost]
    public async Task<IActionResult> ExportUserActivityExcel(DateTime? dateFrom, DateTime? dateTo)
    {
        var data = await _reportService.GetUserActivityAsync(dateFrom, dateTo);
        using var workbook = new ClosedXML.Excel.XLWorkbook();
        var ws = workbook.Worksheets.Add("Активность пользователей");

        ws.Cell(1, 1).Value = "Метрика";
        ws.Cell(1, 2).Value = "Значение";
        ws.Range(1, 1, 1, 2).Style.Font.Bold = true;

        ws.Cell(2, 1).Value = "Всего пользователей";
        ws.Cell(2, 2).Value = data.TotalUsers;
        ws.Cell(3, 1).Value = "Новых за период";
        ws.Cell(3, 2).Value = data.NewUsers;
        ws.Cell(4, 1).Value = "Всего отзывов";
        ws.Cell(4, 2).Value = data.TotalReviews;
        ws.Cell(5, 1).Value = "Опубликованных отзывов";
        ws.Cell(5, 2).Value = data.ApprovedReviews;
        ws.Cell(6, 1).Value = "Всего чатов";
        ws.Cell(6, 2).Value = data.TotalChats;
        ws.Cell(7, 1).Value = "Активных чатов";
        ws.Cell(7, 2).Value = data.ActiveChats;
        ws.Cell(8, 1).Value = "Бронирований";
        ws.Cell(8, 2).Value = data.TotalBookings;

        ws.Cell(10, 1).Value = "Регистрации по месяцам";
        ws.Range(10, 1, 10, 2).Style.Font.Bold = true;
        int row = 11;
        foreach (var m in data.RegistrationsByMonth)
        {
            ws.Cell(row, 1).Value = $"{m.MonthName} {m.Year}";
            ws.Cell(row, 2).Value = m.Count;
            row++;
        }

        ws.Columns().AdjustToContents();

        using var stream = new MemoryStream();
        workbook.SaveAs(stream);
        stream.Position = 0;
        return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
            $"Активность_пользователей_{DateTime.Now:yyyyMMdd}.xlsx");
    }

    // =============================================
    // 3. Статистика отзывов
    // =============================================
    public async Task<IActionResult> ReviewStats(DateTime? dateFrom, DateTime? dateTo)
    {
        if (!IsLoggedIn()) return RedirectToAction("Login", "Account");
        var data = await _reportService.GetReviewStatsAsync(dateFrom, dateTo);
        return View(data);
    }

    [HttpPost]
    public async Task<IActionResult> ExportReviewStatsExcel(DateTime? dateFrom, DateTime? dateTo)
    {
        var data = await _reportService.GetReviewStatsAsync(dateFrom, dateTo);
        using var workbook = new ClosedXML.Excel.XLWorkbook();

        var wsTours = workbook.Worksheets.Add("По турам");
        wsTours.Cell(1, 1).Value = "Тур";
        wsTours.Cell(1, 2).Value = "Отзывов";
        wsTours.Cell(1, 3).Value = "Средняя оценка";
        wsTours.Range(1, 1, 1, 3).Style.Font.Bold = true;
        for (int i = 0; i < data.TourStats.Count; i++)
        {
            wsTours.Cell(i + 2, 1).Value = data.TourStats[i].ObjectName;
            wsTours.Cell(i + 2, 2).Value = data.TourStats[i].ReviewCount;
            wsTours.Cell(i + 2, 3).Value = Math.Round(data.TourStats[i].AverageRating, 2);
        }
        wsTours.Columns().AdjustToContents();

        var wsHotels = workbook.Worksheets.Add("По отелям");
        wsHotels.Cell(1, 1).Value = "Отель";
        wsHotels.Cell(1, 2).Value = "Отзывов";
        wsHotels.Cell(1, 3).Value = "Средняя оценка";
        wsHotels.Range(1, 1, 1, 3).Style.Font.Bold = true;
        for (int i = 0; i < data.HotelStats.Count; i++)
        {
            wsHotels.Cell(i + 2, 1).Value = data.HotelStats[i].ObjectName;
            wsHotels.Cell(i + 2, 2).Value = data.HotelStats[i].ReviewCount;
            wsHotels.Cell(i + 2, 3).Value = Math.Round(data.HotelStats[i].AverageRating, 2);
        }
        wsHotels.Columns().AdjustToContents();

        var wsSights = workbook.Worksheets.Add("По достопримечательностям");
        wsSights.Cell(1, 1).Value = "Достопримечательность";
        wsSights.Cell(1, 2).Value = "Отзывов";
        wsSights.Cell(1, 3).Value = "Средняя оценка";
        wsSights.Range(1, 1, 1, 3).Style.Font.Bold = true;
        for (int i = 0; i < data.SightStats.Count; i++)
        {
            wsSights.Cell(i + 2, 1).Value = data.SightStats[i].ObjectName;
            wsSights.Cell(i + 2, 2).Value = data.SightStats[i].ReviewCount;
            wsSights.Cell(i + 2, 3).Value = Math.Round(data.SightStats[i].AverageRating, 2);
        }
        wsSights.Columns().AdjustToContents();

        using var stream = new MemoryStream();
        workbook.SaveAs(stream);
        stream.Position = 0;
        return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
            $"Статистика_отзывов_{DateTime.Now:yyyyMMdd}.xlsx");
    }

    // =============================================
    // 4. Работа менеджеров
    // =============================================
    public async Task<IActionResult> ManagerPerformance(DateTime? dateFrom, DateTime? dateTo)
    {
        if (!IsLoggedIn()) return RedirectToAction("Login", "Account");
        var data = await _reportService.GetManagerPerformanceAsync(dateFrom, dateTo);
        return View(data);
    }

    [HttpPost]
    public async Task<IActionResult> ExportManagerPerformanceExcel(DateTime? dateFrom, DateTime? dateTo)
    {
        var data = await _reportService.GetManagerPerformanceAsync(dateFrom, dateTo);
        using var workbook = new ClosedXML.Excel.XLWorkbook();
        var ws = workbook.Worksheets.Add("Работа менеджеров");

        ws.Cell(1, 1).Value = "Менеджер";
        ws.Cell(1, 2).Value = "Email";
        ws.Cell(1, 3).Value = "Всего чатов";
        ws.Cell(1, 4).Value = "Завершено";
        ws.Cell(1, 5).Value = "Среднее время ответа (мин)";
        ws.Range(1, 1, 1, 5).Style.Font.Bold = true;

        for (int i = 0; i < data.Count; i++)
        {
            ws.Cell(i + 2, 1).Value = data[i].ManagerName;
            ws.Cell(i + 2, 2).Value = data[i].Email;
            ws.Cell(i + 2, 3).Value = data[i].TotalChats;
            ws.Cell(i + 2, 4).Value = data[i].ClosedChats;
            ws.Cell(i + 2, 5).Value = data[i].AverageResponseMinutes;
        }

        ws.Columns().AdjustToContents();

        using var stream = new MemoryStream();
        workbook.SaveAs(stream);
        stream.Position = 0;
        return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
            $"Работа_менеджеров_{DateTime.Now:yyyyMMdd}.xlsx");
    }

    // =============================================
    // 5. Агентства
    // =============================================
    public async Task<IActionResult> AgencyReport()
    {
        if (!IsLoggedIn()) return RedirectToAction("Login", "Account");
        var data = await _reportService.GetAgencyReportAsync();
        return View(data);
    }

    [HttpPost]
    public async Task<IActionResult> ExportAgencyReportExcel()
    {
        var data = await _reportService.GetAgencyReportAsync();
        using var workbook = new ClosedXML.Excel.XLWorkbook();
        var ws = workbook.Worksheets.Add("Агентства");

        ws.Cell(1, 1).Value = "Агентство";
        ws.Cell(1, 2).Value = "Контакты";
        ws.Cell(1, 3).Value = "Рейтинг";
        ws.Cell(1, 4).Value = "Туров";
        ws.Cell(1, 5).Value = "Отзывов";
        ws.Cell(1, 6).Value = "Средний рейтинг туров";
        ws.Range(1, 1, 1, 6).Style.Font.Bold = true;

        for (int i = 0; i < data.Count; i++)
        {
            ws.Cell(i + 2, 1).Value = data[i].AgencyName;
            ws.Cell(i + 2, 2).Value = data[i].ContactInfo;
            ws.Cell(i + 2, 3).Value = data[i].Rating;
            ws.Cell(i + 2, 4).Value = data[i].TourCount;
            ws.Cell(i + 2, 5).Value = data[i].TotalReviews;
            ws.Cell(i + 2, 6).Value = Math.Round(data[i].AverageTourRating, 2);
        }

        ws.Columns().AdjustToContents();

        using var stream = new MemoryStream();
        workbook.SaveAs(stream);
        stream.Position = 0;
        return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
            $"Агентства_{DateTime.Now:yyyyMMdd}.xlsx");
    }
}
