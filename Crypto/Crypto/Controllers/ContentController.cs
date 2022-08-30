using Crypto.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Cryptography;
using System.Text;

namespace Crypto.Controllers;

[ApiController]
public class ContentController : Controller
{
    private readonly string _key = "b14ca5898a4e4142aace2ea2143a2410";
    private readonly byte[] _iv = new byte[16];
    
    [HttpPost]
    [Route("/[controller]/encrypt")]
    public IActionResult Encrypt(Content content)
    {
        using (Aes aes = Aes.Create())
        {
            aes.Key = Encoding.UTF8.GetBytes(_key);
            aes.IV = _iv;
            ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);
            using (var memoryStream = new MemoryStream())
            {
                using (var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                {
                    using (var streamWriter = new StreamWriter(cryptoStream))
                    {
                        streamWriter.Write(content.str);
                    }

                    var bytes = memoryStream.ToArray();
                    return Ok(Convert.ToBase64String(bytes));
                    
                    // return Ok(Convert.ToHexString(bytes));
                }
            }
        }
    }
    
    [HttpPost]
    [Route("/[controller]/decrypt")]
    public IActionResult Decrypt(Content content)
    {
        // var bytes = Convert.FromHexString(content.str);
        var bytes = Convert.FromBase64String(content.str);
        using (Aes aes = Aes.Create())
        {
            aes.Key = Encoding.UTF8.GetBytes(_key);
            aes.IV = _iv;
            ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
            using (var memoryStream = new MemoryStream(bytes))
            {
                using (var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read))
                { 
                    using (var streamReader = new StreamReader(cryptoStream))
                    {
                        return Ok(streamReader.ReadToEnd());
                    }
                }
            }
        }
    }
}