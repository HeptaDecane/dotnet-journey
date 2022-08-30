using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PostBin.Models;
using PostBin.Services;

namespace PostBin.Controllers
{
    [ApiController]
    [Route("/posts")]
    public class PostsController : Controller
    {
        private readonly IPostService _postService;
        private readonly IUserService _userService;

        public PostsController(IPostService postService, IUserService userService)
        {
            _postService = postService;
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var posts = await _postService.GetAllAsync();
            return Ok(posts);
        }
        
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var post = await _postService.GetByIdAsync(id);
            return post is null ? NotFound() : Ok(post);
        }
        
        [HttpPost]
        public async Task<ActionResult> Post(Post post)
        {
            if (base.User.Identity.IsAuthenticated)
            {
                var user = await _userService.GetByUsernameAsync(base.User.Identity.Name);
                post.UserId = user.Id;
            }
            
            post.Id = await _postService.CreateAsync(post);
            
            // item has been assigned Id at this point
            return Created(new Uri($"posts/{post.Id}", UriKind.Relative), post);
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Post post)
        {
            var postInDb = await _postService.GetByIdAsync(id);
            if(postInDb is null)
                return NotFound();
            
            var userIdClaim = HttpContext.User.Claims.FirstOrDefault(claim => claim.Type==ClaimTypes.NameIdentifier);
            if (userIdClaim is null || userIdClaim.Value != postInDb.UserId.ToString())
                return Forbid();
            
            post = await _postService.UpdateAsync(id, post);
            return Ok(post);
        }
        
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var post = await _postService.GetByIdAsync(id);
            if(post is null)
                return NotFound();
            
            var userIdClaim = HttpContext.User.Claims.FirstOrDefault(claim => claim.Type==ClaimTypes.NameIdentifier);
            if (userIdClaim is null || userIdClaim.Value != post.UserId.ToString())
                return Forbid();

            await _postService.DeleteAsync(id);
            return Ok("OK");
        }
    }
}