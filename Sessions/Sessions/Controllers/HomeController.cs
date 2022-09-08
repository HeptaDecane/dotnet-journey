using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Sessions.Models;

namespace Sessions.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        
        [TempData]
        public int Visits { get; set; }
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var sessionId = HttpContext.Session.Id;
            var user = HttpContext.Session.GetString("user");
            Visits = Visits + 1;
            
            if(string.IsNullOrEmpty(user))
                return RedirectToAction("Login", "Home");
            
            ViewData["sessionId"] = sessionId;
            ViewData["user"] = user;
            
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }
        
        public IActionResult About()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(Auth auth)
        {
            if(!ModelState.IsValid)
                return View();

            if (auth.Username == "testuser" && auth.Password == "testpass")
            {
                HttpContext.Session.SetString("user","testuser");
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        [HttpGet]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login", "Home");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}