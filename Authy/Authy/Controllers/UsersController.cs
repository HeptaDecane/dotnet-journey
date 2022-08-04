using System.Security.Claims;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using Authy.Services;
using Authy.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;

namespace Authy.Controllers;

public class UsersController : Controller
{

    private readonly UsersServices _usersServices;
    private readonly WeatherApiServices _weatherApiServices;

    public UsersController(UsersServices usersServices, WeatherApiServices weatherApiServices) {
        _usersServices = usersServices;
        _weatherApiServices = weatherApiServices;
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

    public ActionResult Register()
    {
        return View();
    }
    
    [HttpPost]
    public async Task<ActionResult> Register(User user)
    {
        User existingUser = await _usersServices.GetAsync(user.Username);
        if (existingUser.Id != 0) {
            Console.WriteLine("username exists: "+user.Username);
            return View();
        }
        
        user.Salt = _generateSalt();
        user.Password = _hash(user.Password, user.Salt);
        int rowsAffected = await _usersServices.CreateAsync(user);
        Console.WriteLine("rows affected: " + rowsAffected);
        return RedirectToAction("Login");
    }

    public ActionResult Login()
    {
        return View();
    }
    
    [HttpPost]
    public async Task<ActionResult> Login(Auth auth)
    {
        User user = await _usersServices.GetAsync(auth.Username);
        if (user.Id == 0) {
            Console.WriteLine("username not found: "+auth.Username);
            return View();
        }

        string password = _hash(auth.Password, user.Salt);
        if (password == user.Password)
        {
            var claims = new List<Claim> {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, user.Role),
                new Claim(ClaimTypes.GivenName, user.Name)
            };
            var identity = new ClaimsIdentity(claims, Models.User.AuthName);
            var principal = new ClaimsPrincipal(identity);

            var properties = new AuthenticationProperties() {
                IsPersistent = true
            };
            await HttpContext.SignInAsync(Models.User.AuthName, principal, properties);
            
            // login to weather apis
            var jwtResponse = await _weatherApiServices.Authenticate(auth);
            Response.Cookies.Append(JwtResponse.AuthName,jwtResponse.AccessToken);
            
            return RedirectToAction("Index","Home");
        }
        return View();
    }

    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(Models.User.AuthName);
        Response.Cookies.Delete(JwtResponse.AuthName);
        return RedirectToAction("Index", "Home");
    }

    public IActionResult AccessDenied()
    {
        return View();
    }
}