using Microsoft.AspNetCore.Mvc;

namespace ClientSideDev.Controllers.api;

[ApiController]
[Route("api")]
public class HomeController: Controller
{
    [HttpGet]
    public ActionResult Get()
    {
        return Ok("Api Running...");
    }
}