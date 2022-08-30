using Microsoft.AspNetCore.Mvc;

namespace Crypto.Controllers;

[ApiController]
[Route("/")]
public class HomeController : Controller
{
    [HttpGet]
    public IActionResult Get()
    {
        return Ok("API Running...");
    }
}