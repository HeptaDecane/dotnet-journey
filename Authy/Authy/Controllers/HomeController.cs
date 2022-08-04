using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Authy.Models;
using Authy.Services;
using Microsoft.AspNetCore.Authorization;

namespace Authy.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly WeatherApiServices _weatherApiServices;

    public HomeController(ILogger<HomeController> logger, WeatherApiServices weatherApiServices)
    {
        _logger = logger;
        _weatherApiServices = weatherApiServices;
    }

    // default auth
    [Authorize]
    public async Task<IActionResult> Index()
    {
        string accessToken = Request.Cookies[JwtResponse.AuthName] ?? "";
        var forecasts = await _weatherApiServices.GetForecastsAsync(accessToken);
        return View(forecasts);
    }

    // policy based auth
    [Authorize("Admin")]
    public IActionResult Settings()
    {
        return View();
    }
    
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}