using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using WeatherApi.Models;
using WeatherApi.Services;

namespace WeatherApi.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController : Controller
{
    private readonly UsersServices _usersServices;
    private readonly IConfiguration _configuration;

    public AuthController(UsersServices usersServices, IConfiguration configuration)
    {
        _usersServices = usersServices;
        _configuration = configuration;
    }
    
    private string _generateSalt() {
        var random = new Random();
        string set = "abcdefghijklmnopqrstuvwxyz";
        string salt = "";

        
        for(int i=0; i<8; i++) {
            int idx = random.Next(set.Length);
            salt = salt + set[idx];
        }

        return salt;
    }
    private string _hash(string password, string salt) {
        
        // Create a SHA256   
        var sha256 = SHA256.Create();
        
        // ComputeHash - returns byte array  
        byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password+salt));  
  
        // Convert byte array to a string   
        StringBuilder builder = new StringBuilder();  
        foreach (byte b in bytes) builder.Append(b.ToString("x2"));

        return builder.ToString();
    }

    private string _generateToken(IEnumerable<Claim> claims, DateTime expireAt)
    {
        string jwtSecret = _configuration.GetValue<string>("JwtSecret");
        var securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtSecret));
        var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        
        var jwt = new JwtSecurityToken(
            claims: claims,
            expires: expireAt,
            signingCredentials: signingCredentials
        );

        string token = new JwtSecurityTokenHandler().WriteToken(jwt);
        Console.WriteLine(token);
        return token;
    }

    [HttpPost]
    public async Task<ActionResult> Post(Auth auth)
    {
        User user = await _usersServices.GetAsync(auth.Username);
        if (user.Id == 0)
            return Unauthorized("invalid username or password");

        string password = _hash(auth.Password, user.Salt);
        if (password == user.Password)
        {
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, user.Role),
                new Claim(ClaimTypes.GivenName, user.Name),
                new Claim("test_claim","this is a custom claim")
            };
            var expiresAt = DateTime.UtcNow.AddDays(30);
            
            return Ok(new {
                accessToken = _generateToken(claims, expiresAt),
                expiresAt = expiresAt
            });
        }
        return Unauthorized("invalid username or password");
    }
}