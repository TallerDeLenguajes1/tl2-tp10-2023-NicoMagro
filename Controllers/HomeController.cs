using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using tl2_tp10_2023_NicoMagro.Models;

namespace tl2_tp10_2023_NicoMagro.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        if (!Logueado())
        {
            return RedirectToRoute(new { controller = "Login", action = "Index" });
        }
        return View();
    }

    public bool Logueado()
    {
        return HttpContext.Session.Keys.Any();
    }

    private bool esAdmin()
    {
        return HttpContext.Session.Keys.Any() && ((int)HttpContext.Session.GetInt32("rol") == 1);
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
