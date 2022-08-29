using Api.Models;
using Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("/")]
public class HomeController : ControllerBase
{
    private readonly ILogger<HomeController> _logger;
    private readonly BufferUploadService _bufferUploadService;

    public HomeController(ILogger<HomeController> logger, BufferUploadService bufferUploadService)
    {
        _logger = logger;
        _bufferUploadService = bufferUploadService;
    }

    [HttpPost]
    [Consumes("multipart/form-data")]
    public async Task<IActionResult> Post([FromForm]FormModel formModel)
    {
        Console.WriteLine(formModel);
        if (formModel.Image is not null)
        {
            Console.WriteLine(formModel.Image.ContentType);
            Console.WriteLine(formModel.Image.Length);
            Console.WriteLine(formModel.Image.FileName);

            if (await _bufferUploadService.Upload(formModel.Image))
                return Accepted(new {File = formModel.Image.FileName});
        }
        return BadRequest();
    }
}