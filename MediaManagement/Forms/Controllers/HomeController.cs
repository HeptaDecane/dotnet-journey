using System.Diagnostics;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Mvc;
using Forms.Models;
using Forms.Services;

namespace Forms.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly ApiService _apiService;

    public HomeController(ILogger<HomeController> logger, ApiService apiService)
    {
        _logger = logger;
        _apiService = apiService;
    }

    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Index(FormModel formModel)
    {
        Console.WriteLine(formModel);
        var httpClient = new HttpClient();
        using (var form = new MultipartFormDataContent())
        {
            form.Add(new StringContent("test"),"Title");
            form.Add(new StringContent("a test file"), "description");

            var streamContent = new StreamContent(formModel.Image.OpenReadStream());
            streamContent.Headers.ContentType = new MediaTypeHeaderValue(formModel.Image.ContentType);
            form.Add(streamContent,"Image",formModel.Image.FileName);
            var response = await httpClient.PostAsync("https://localhost:44384/", form);
            response.EnsureSuccessStatusCode();
            Console.WriteLine(response.StatusCode);
        }
        return View();
    }
    
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}