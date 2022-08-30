using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text;
using Crypto.Models;

namespace Crypto.Controllers;

[ApiController]
public class MediaController : Controller
{
    private readonly string _key = "b14ca5898a4e4142aace2ea2143a2410";
    private readonly byte[] _iv = new byte[16];
    
    [HttpPost]
    [Route("/[controller]/encrypt")]
    [Consumes("multipart/form-data")]
    public IActionResult Encrypt([FromForm]Media media)
    {
        using (Aes aes = Aes.Create())
        {
            aes.Key = Encoding.UTF8.GetBytes(_key);
            aes.IV = _iv;
            ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, _iv);
            using (var outStream = new FileStream("test.aes", FileMode.Create))
            {
                using (var cryptoStream = new CryptoStream(outStream, encryptor, CryptoStreamMode.Write))
                {
                    var inStream = media.file.OpenReadStream();
                    int data;
                    while ((data = inStream.ReadByte()) != -1)
                    {
                        cryptoStream.WriteByte((byte)data);
                    }
                }
            }
            return Ok();
        }
    }
    
    [HttpPost]
    [Route("/[controller]/decrypt")]
    [Consumes("multipart/form-data")]
    public IActionResult Decrypt([FromForm]Media media)
    {
        using (Aes aes = Aes.Create())
        {
            aes.Key = Encoding.UTF8.GetBytes(_key);
            aes.IV = _iv;
            ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, _iv);
            var inStream = media.file.OpenReadStream();
            using (var cryptoStream = new CryptoStream(inStream, decryptor, CryptoStreamMode.Read))
            {
                using (var outStream = new FileStream("test.pdf", FileMode.Create))
                {
                    int data;
                    while ((data = cryptoStream.ReadByte()) != -1)
                    {
                        outStream.WriteByte((byte)data);
                    }
                }
            }
            return Ok();
        }
    }
}