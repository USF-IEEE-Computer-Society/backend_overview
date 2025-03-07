using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Workshop_Basics.Database;
using Workshop_Basics.Database.Entities;

namespace Workshop_Basics.Controllers.ImprovedDummyControllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ImprovedDummyAdminController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;
        
        public ImprovedDummyAdminController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
    
        [HttpGet("users")]
        public async Task<ActionResult<List<User>>> GetAllUsers()
        {
            var users = await _appDbContext.Users.ToListAsync();
            return Ok(users);
        }
        
        [HttpGet("posts")]
        public async Task<ActionResult<List<Post>>> GetAllPosts()
        {
            var posts = await _appDbContext.Posts.ToListAsync();
            return Ok(posts);
        }
    
        [HttpGet("user/{userId}")]
        public async Task<ActionResult<User>> GetUserAndPostByUserId(int userId)
        {
            var userWithPosts = await _appDbContext.Users
                .Include(u => u.Posts)
                .FirstOrDefaultAsync(u => u.UserId == userId);
            if (userWithPosts == null)
            {
                return NotFound();
            }
            return Ok(userWithPosts);
        }
    
        [HttpDelete("post/{postId}")]
        public async Task<ActionResult<Post>> DeletePostByPostId(int postId)
        {
            var postToDelete = await _appDbContext.Posts.FirstOrDefaultAsync(p => p.PostId == postId);
            if (postToDelete == null)
            {
                return NotFound();
            }
            
            _appDbContext.Posts.Remove(postToDelete);
            await _appDbContext.SaveChangesAsync();
            return Ok(postToDelete);
        }
    
        // GET user by nickname
        [HttpGet("{nickname}")]
        public async Task<ActionResult<User>> GetUserByNickname(string nickname)
        {
            if (string.IsNullOrWhiteSpace(nickname))
            {
                return BadRequest("Nickname must be provided.");
            }
    
            var user = await _appDbContext.Users
                .Include(u => u.Posts)
                .FirstOrDefaultAsync(u => u.Nickname == nickname);
    
            if (user == null)
            {
                return NotFound($"User with nickname '{nickname}' not found.");
            }
    
            return Ok(user);
        }
    
        // DELETE user by nickname
        [HttpDelete("{nickname}")]
        public async Task<ActionResult<User>> DeleteUserByNickname(string nickname)
        {
            if (string.IsNullOrWhiteSpace(nickname))
            {
                return BadRequest("Nickname must be provided.");
            }
    
            var user = await _appDbContext.Users.FirstOrDefaultAsync(u => u.Nickname == nickname);
            if (user == null)
            {
                return NotFound($"User with nickname '{nickname}' not found.");
            }
    
            _appDbContext.Users.Remove(user);
            await _appDbContext.SaveChangesAsync();
            return Ok(user);
        }
    }
}
