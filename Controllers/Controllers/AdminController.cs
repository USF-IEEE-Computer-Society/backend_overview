using Microsoft.EntityFrameworkCore; 
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Workshop_Basics.Database;
using Workshop_Basics.Database.Entities;

namespace Workshop_Basics.Controllers.Controllers;

[ApiController]
[Authorize("AdminPolicy")]
[Route("api/[controller]")]

public class AdminController: ControllerBase
{
    private AppDbContext _appDbContext;
    public AdminController(AppDbContext appDbContext)
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
    public async Task<ActionResult<Post>> GetAllPosts()
    {
        var posts = await _appDbContext.Posts.ToListAsync();
        return Ok(posts);
    }

    [HttpGet("user/{nickname}")]
    public async Task<ActionResult<User>> GetUserAndPostByUserNickname(string nickname)
    {
        var userWithPosts = await _appDbContext.Users
            .Include(u => u.Posts)
            .FirstOrDefaultAsync(u => u.Nickname == nickname);
        if (userWithPosts == null)
        {
            return NotFound();
        }
        return Ok(userWithPosts);
    }
    
    [HttpDelete("post/{postId}")]
    public async Task<ActionResult<User>> DeletePostByPostId(int postId)
    {
        var postToDelete = await _appDbContext.Posts.FirstOrDefaultAsync(p => p.PostId == postId);
        if (postToDelete != null)
        {
            // Mark the entity for deletion
            _appDbContext.Posts.Remove(postToDelete);
    
            // Save the deletion to the database
           await _appDbContext.SaveChangesAsync();
        }
          
        if (postToDelete == null)
        {
            return NotFound();
        }
        return Ok(postToDelete);
    }
    
    //Todo: method/endpoint to delete user
}