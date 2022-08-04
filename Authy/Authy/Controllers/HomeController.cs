﻿using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Authy.Models;
using Authy.Services;
using Microsoft.AspNetCore.Authorization;

namespace Authy.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly WeatherApiService _weatherApiService;

    public HomeController(ILogger<HomeController> logger, WeatherApiService weatherApiService)
    {
        _logger = logger;
        _weatherApiService = weatherApiService;
    }

    // default auth
    [Authorize]
    public async Task<IActionResult> Index()
    {
        var forecasts = await _weatherApiService.GetForecastsAsync();
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