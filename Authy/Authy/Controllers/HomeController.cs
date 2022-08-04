using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Authy.Models;
using Microsoft.AspNetCore.Authorization;

namespace Authy.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    // default auth
    [Authorize]
    public IActionResult Index()
    {
        return View();
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