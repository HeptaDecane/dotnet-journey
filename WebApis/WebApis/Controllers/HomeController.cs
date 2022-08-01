using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace WebApis.Controllers;

[ApiController]
[Route("")]
public class HomeController : Controller
{
    [HttpGet]
    public ActionResult Get()
    {
        return Ok("Api Running...");
    }
}