using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Workshop_Basics.Database;
using Workshop_Basics.Database.Entities;
using Workshop_Basics.Models;
using Workshop_Basics.Services;
using User = Workshop_Basics.Database.Entities.User;

namespace Workshop_Basics.Controllers.ImprovedDummyControllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ImprovedDummyUserController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;
        private readonly Helper _helper;
        
        public ImprovedDummyUserController(AppDbContext appDbContext, Helper helper)
        {
            _appDbContext = appDbContext;
            _helper = helper;
        }

        // POST api/improveddummyuser/signup
        [HttpPost("signup")]
        public async Task<ActionResult<User>> SignUp([FromBody] UserSignup newUser)
        {
            if (!_helper.CheckUserSignUp(newUser))
            {
                return BadRequest("Invalid Data Provided");
            }
        
            var userExists = await _appDbContext.Users
                .AnyAsync(u => u.Email == newUser.email || u.Nickname == newUser.nickname);
            if (userExists)
            {
                return BadRequest("User already exists with provided email or nickname.");
            }
        
            var user = new User
            {
                Nickname = newUser.nickname,
                Email = newUser.email,
                Password = newUser.password,
                FirstName = newUser.firstname,
                LastName = newUser.lastname
            };
        
            try
            {
                await _appDbContext.Users.AddAsync(user);
                await _appDbContext.SaveChangesAsync();
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occurred while signing up.");
            }

            return Ok(user);
        }
        
        [HttpPut("login")]
        public async Task<ActionResult<User>> Login([FromBody] Credentials? loginData)
        {
            if (loginData == null ||
                string.IsNullOrWhiteSpace(loginData.nickname) ||
                string.IsNullOrWhiteSpace(loginData.password))
            {
                return BadRequest("Invalid credentials.");
            }
        
            var user = await _appDbContext.Users
                .FirstOrDefaultAsync(u => u.Nickname == loginData.nickname && u.Password == loginData.password);
        
            if (user == null)
            {
                return Unauthorized();
            }
        
            return Ok(user);
        }
        
        
        [HttpGet("posts/{postId}")]
        public async Task<ActionResult<Post>> GetPostById(int postId)
        {
            var post = await _appDbContext.Posts.FindAsync(postId);
            if (post == null)
            {
                return NotFound();
            }
            return Ok(post);
        }

        // Changed route to "user/edit" to avoid conflict
        [HttpPut("user/edit")]
        public async Task<ActionResult<User>> EditUserInfo([FromBody] UserSignup updatedUser)
        {
            if (!_helper.CheckUserSignUp(updatedUser))
            {
                return BadRequest("Invalid Data Provided");
            }

            var user = await _appDbContext.Users
                .FirstOrDefaultAsync(u => u.Password == updatedUser.password && u.Nickname == updatedUser.nickname);

            if (user == null)
            {
                return NotFound("Could not find the user with provided data");
            }

            user.Nickname = updatedUser.nickname;
            user.Email = updatedUser.email;
            user.Password = updatedUser.password;
            user.FirstName = updatedUser.firstname;
            user.LastName = updatedUser.lastname;
            
            try
            {
                await _appDbContext.SaveChangesAsync();
            }
            catch
            {
                return StatusCode(500, "An error occurred while updating the user.");
            }
            
            return Ok(user);
        }
        
        // POST api/improveddummyuser/posts
        [HttpPost("posts")]
        public async Task<ActionResult<Post>> CreatePost([FromBody] Post post)
        {
            if (!_helper.CheckPost(post))
            {
                return BadRequest("Post's data is not valid");
            }

            try
            {
                await _appDbContext.Posts.AddAsync(post);
                await _appDbContext.SaveChangesAsync();
            }
            catch
            {
                return BadRequest("Invalid UserId");
            }

            return Ok(post);
        }

        // Changed route to "posts/edit" to avoid conflict
        [HttpPut("posts/edit")]
        public async Task<ActionResult<Post>> EditPost([FromBody] Post updatedPost)
        {
            if (!_helper.CheckPost(updatedPost))
            {
                return BadRequest("Post's data is invalid ");
            }

            var post = await _appDbContext.Posts
                .FirstOrDefaultAsync(p => p.PostId == updatedPost.PostId && p.UserId == updatedPost.UserId);
            if (post == null)
            {
                return BadRequest("Post was not found");
            }

            post.Content = updatedPost.Content;
            post.Header = updatedPost.Header;
            try
            {
                await _appDbContext.SaveChangesAsync();
            }
            catch
            {
                return StatusCode(500, "An error occurred while updating the post.");
            }

            return Ok(post);
        }

        [HttpGet("{userId}/posts")]
        public async Task<ActionResult<List<Post>>> GetPostsByUserId(int userId)
        {
            var posts = await _appDbContext.Posts
                .Where(p => p.UserId == userId)
                .ToListAsync();
            return Ok(posts);
        }
    }
}
