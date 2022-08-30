using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using PostBin.Models;
using PostBin.Services;
using PostBin.Shared;

namespace PostBin.Controllers
{
    [ApiController]
    [Route("/users")]
    public class UsersController : Controller
    {
        private readonly IUserService _userService;
        private readonly HttpClient _http;
        public UsersController(IUserService userService, IOptions<AppSettings> options)
        {
            _userService = userService;
            _http = new HttpClient();
        }
        
        [HttpPost]
        public async Task<ActionResult> Post(User user)
        {
            User existingUser = await _userService.GetByUsernameAsync(user.Username);
            if (existingUser is not null)
                return BadRequest($"username exists: {user.Username}");
               
        
            user.Salt = Crypto.GenerateSalt();
            user.Password = Crypto.Hash(user.Password, user.Salt);
            await _userService.CreateAsync(user);

            return RedirectToAction("Post", "Auth");
        }
    }
}