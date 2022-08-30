using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using PostBin.Models;
using PostBin.Services;
using PostBin.Shared;

namespace PostBin.Controllers
{
    [ApiController]
    [Route("/auth")]
    public class AuthController : Controller
    {
        private readonly IUserService _userService;
        private readonly AppSettings _appSettings;
        public AuthController(IUserService userService, IOptions<AppSettings> options)
        {
            _userService = userService;
            _appSettings = options.Value;
        }

        private string _generateToken(IEnumerable<Claim> claims, DateTime expireAt)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_appSettings.Secret));
            var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        
            var jwt = new JwtSecurityToken(
                claims: claims,
                issuer: _appSettings.Issuer,
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
            User user = await _userService.GetByUsernameAsync(auth.Username);
            if (user is null)
                return Unauthorized("invalid username or password");

            string password = Crypto.Hash(auth.Password, user.Salt);
            if (password == user.Password)
            {
                var claims = new List<Claim>{
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Name, user.Username),
                    new Claim(ClaimTypes.Role, user.Role),
                    new Claim(ClaimTypes.GivenName, user.Name),
                };
                var expiresAt = DateTime.UtcNow.AddDays(_appSettings.ExpireTime);
            
                return Ok(new {
                    accessToken = _generateToken(claims, expiresAt),
                    expiresAt = expiresAt
                });
            }
            
            return Unauthorized("invalid username or password");
        }
    }
}