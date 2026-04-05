using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TravelGuide.Models;
using TravelGuide.Models.Entities;
using TravelGuide.Data;

namespace TravelGuide.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly TravelGuideContext _context;

    public HomeController(ILogger<HomeController> logger, TravelGuideContext context)
    {
        _logger = logger;
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        // Получаем 4 популярных тура с связанными данными
        var tours = await _context.Tours
            .Include(t => t.Country)
            .Include(t => t.Agency)
            .Include(t => t.Reviews)
            .ToListAsync();
        
        var popularTours = tours
            .Where(t => t.Reviews != null && t.Reviews.Any())
            .OrderByDescending(t => t.Reviews.Average(r => r.Rating))
            .Take(4)
            .ToList();
        
        // Если нет туров с отзывами, берём любые 4
        if (!popularTours.Any())
        {
            popularTours = tours.Take(4).ToList();
        }
        
        ViewBag.PopularTours = popularTours;
        
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}