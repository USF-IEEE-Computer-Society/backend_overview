using Microsoft.AspNetCore.Mvc;
using Workshop_Basics.DummyDatabase;
using Workshop_Basics.Models;

namespace Workshop_Basics.Controllers.DummyControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DummyUserController : ControllerBase
    {
        private readonly UserDummyDatabase _dummyDatabase;

        public DummyUserController()
        {
            _dummyDatabase = new UserDummyDatabase();
        }

        [HttpPost("login")]
        public ActionResult<DummyUser> GetUser([FromBody] Credentials credentials)
        {
            var user = _dummyDatabase.GetUser(credentials.nickname, credentials.password);
            if (user == null)
                return NotFound();

            return Ok(user);
        }
        
        [HttpPost("signup")]
        public ActionResult<DummyUser> Signup([FromBody] DummyUser user)
        {
            var newUser = _dummyDatabase.CreateUser(user.FirstName, user.Username, user.Password);
            return CreatedAtAction(nameof(GetUser), new { id = newUser.UserId }, newUser);
        }
        
        
        [HttpPut("{id}")]
        public ActionResult<DummyUser> UpdateMyAccount(int id, [FromBody] DummyUser user)
        {
            var updatedUser = _dummyDatabase.UpdateUser(id, user.FirstName, user.Username, user.Password);
            if (updatedUser == null)
                return NotFound("User not found.");
            return Ok(updatedUser);
        }
        
        
        
    }
}