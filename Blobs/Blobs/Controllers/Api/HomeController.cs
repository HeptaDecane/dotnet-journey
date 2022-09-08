using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Blobs.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Blobs.Controllers.Api
{
    [ApiController]
    [Route("api")]
    public class HomeController : Controller
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok("API Running...");
        }

        [Route("toBase64")]
        [HttpPost]
        [Consumes("image/png","image/jpg","application/pdf","application/octet-stream")]
        public async Task<IActionResult> ToBase64()
        {
            /*
            using (var fileStream = new FileStream("red.pdf", FileMode.Create)) {
                await Request.BodyReader.CopyToAsync(fileStream);
            }
            */
            
            /*
            using (var outStream = new FileStream("green.pdf", FileMode.Create))
            {
                var inStream = Request.BodyReader.AsStream();
                int data;
                while ((data = inStream.ReadByte()) != -1)
                {
                    outStream.WriteByte(Convert.ToByte(data));
                }
            }
            */
            
            using (MemoryStream memoryStream = new MemoryStream())
            {
                await Request.BodyReader.CopyToAsync(memoryStream);
                var bytes = memoryStream.ToArray();
                string base64String = Convert.ToBase64String(bytes);
                return Ok($"data:{Request.ContentType};base64,{base64String}");
            }
        }

        [Route("toFile")]
        [HttpPost]
        [Consumes("application/x-www-form-urlencoded","multipart/form-data")]
        public IActionResult ToFile([FromForm]FileData fileData)
        {
            try {
                string metadata = fileData.EncodedString.Split(",").First();
                string base64String = fileData.EncodedString.Split(",").Last();
                var bytes = Convert.FromBase64String(base64String);
                var contentType = metadata.Split(';', ':')[1];
                return File(bytes, contentType);
            }
            catch (Exception e) {
                if (e is IndexOutOfRangeException or FormatException)
                    return BadRequest("encoded string format does not match `data:[<mediatype>][;base64],<data>`");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}