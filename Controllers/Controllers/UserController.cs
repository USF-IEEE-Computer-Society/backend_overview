using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Workshop_Basics.Database;
using Workshop_Basics.Database.Entities;
using Workshop_Basics.Models;
using Workshop_Basics.Services;
using User = Workshop_Basics.Database.Entities.User;

namespace Workshop_Basics.Controllers.Controllers;

[ApiController]
[Route("api/[controller]")]

public class UserController: ControllerBase
{
    private readonly AppDbContext _appDbContext;
    private readonly JwtAuthService _jwtAuthService;
    private readonly Helper _helper;
    public UserController(AppDbContext appDbContext, JwtAuthService jwtAuthService, Helper helper)
    {
        _appDbContext = appDbContext;
        _jwtAuthService = jwtAuthService;
        _helper = helper;
    }
    
    [Authorize("UserPolicy")]
    [HttpPost("create-post")]
    public async Task<IActionResult> CreatePost([FromBody] Post post)
    {
        // Retrieve the userId claim value
        var userIdClaim = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (userIdClaim == null)
        {
            return Unauthorized("User ID claim is missing.");
        }

        var isValidPost = _helper.CheckPost(post);
        if (!isValidPost)
        {
            return BadRequest("Post's data is not valid");
        }

        // Convert the userId to the expected type (e.g., int)
        if (!int.TryParse(userIdClaim, out var userId))
        {
            return BadRequest("Invalid user ID format.");
        }

        // Create a new post instance using an object initializer
        Post newPost = new Post
        {
            Content = post.Content,
            Header = post.Header,
            UserId = userId  // Assuming UserId is an int
        };
        
        // Add the post to the context and save changes
        await _appDbContext.Posts.AddAsync(newPost);
        await _appDbContext.SaveChangesAsync();

        // Return the newPost instance directly instead of res.Entity
        return Ok(newPost);
    }
    
    // Get all posts for the currently authenticated user.
    [HttpGet("posts/mine")]
    [Authorize("UserPolicy")]
    public async Task<ActionResult<List<Post>>> GetMyPosts()
    {
        // Retrieve the user id from the claims.
        var userIdClaim = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (userIdClaim == null)
        {
            return Unauthorized("User ID claim is missing.");
        }
    
        if (!int.TryParse(userIdClaim, out var userId))
        {
            return BadRequest("Invalid user ID format.");
        }
    
        var posts = await _appDbContext.Posts
            .Where(p => p.UserId == userId)
            .ToListAsync();
    
        return Ok(posts);
    }
    
    // PUT api/user/posts/{postId}
    // Edit an existing post; only the post owner can edit.
    [HttpPut("posts/edit/{postId}")]
    [Authorize("UserPolicy")]
    public async Task<IActionResult> EditPost(int postId, [FromBody] Post updatedPost)
    {
        // Retrieve the user id from the claims.
        var userIdClaim = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (userIdClaim == null)
        {
            return Unauthorized("User ID claim is missing.");
        }
    
        if (!int.TryParse(userIdClaim, out var userId))
        {
            return BadRequest("Invalid user ID format.");
        }
    
        if (!_helper.CheckPost(updatedPost))
        {
            return BadRequest("Updated post data is invalid.");
        }
    
        // Find the post that belongs to the current user.
        var post = await _appDbContext.Posts.FirstOrDefaultAsync(p => p.PostId == postId && p.UserId == userId);
        if (post == null)
        {
            return NotFound("Post not found or you are not authorized to edit this post.");
        }
    
        // Update post properties.
        post.Content = updatedPost.Content;
        post.Header = updatedPost.Header;
    
        await _appDbContext.SaveChangesAsync();
    
        return Ok(post);
    }
    
    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<ActionResult<string>> Login([FromBody] Credentials? userCredential)
    {
        // Implement logic to check if the user already has a valid token
        if (HttpContext.User.Identity != null && HttpContext.User.Identity.IsAuthenticated)
        {
            return Ok("User is already authenticated.");
        }
        
        if (userCredential == null ||
            string.IsNullOrWhiteSpace(userCredential.nickname) ||
            string.IsNullOrWhiteSpace(userCredential.password))
        {
            return BadRequest("Invalid credentials.");
        }

        var token = await _jwtAuthService.GenerateTokenAsync(userCredential);
    
        if (token == null)
        {
            return Unauthorized();
        }

        return Ok(token);
    }
    
    [AllowAnonymous]
    [HttpPost("signup")]
    public async Task<ActionResult<string>> Signup([FromBody] UserSignup userSignup)
    {
        var userExists = await _appDbContext.Users
            .AnyAsync(u => u.Email == userSignup.email || u.Nickname == userSignup.nickname);
        if (userExists)
        {
            return BadRequest("User already exists with provided email or nickname.");
        }
        
        var isUserGood = _helper.CheckUserSignUp(userSignup);
        if (HttpContext.User.Identity != null && HttpContext.User.Identity.IsAuthenticated)
        {
            return Ok("User is already authenticated.");
        }
        if (!isUserGood)
        {
            return BadRequest("Your data is not valid");
        }
        
        var newUser = new User
        {
            FirstName = userSignup.firstname,
            LastName = userSignup.lastname,
            Nickname = userSignup.nickname,
            Email = userSignup.email,
            Password = userSignup.password // Ensure password hashing is applied
        };

        
        await _appDbContext.Users.AddAsync(newUser);
        await _appDbContext.SaveChangesAsync();

        var credentials = new Credentials(userSignup.nickname, userSignup.password);
        var token = await _jwtAuthService.GenerateTokenAsync(credentials);
        if (token == null)
        {
            return StatusCode(500, "Account was created please login");
        }
        return Ok(token);
    }
}

