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
        public DummyUserController(UserDummyDatabase dummyDatabase)
        {
            _dummyDatabase = dummyDatabase;
        }

        [HttpPost("login")]
        public ActionResult<DummyUser> GetUser([FromBody] Credentials credentials)
        {
            var user = _dummyDatabase.GetUser(credentials.nickname, credentials.password);
            if (user == null)
                return StatusCode(500, "Account was created please login");

            return Ok(user);
        }
        
        [HttpPost("signup")]
        public ActionResult<DummyUser> Signup([FromBody] DummyUserSignup user)
        {
            var newUser = _dummyDatabase.CreateUser(user.FirstName, user.Username, user.Password);
            var checkUser = _dummyDatabase.GetUser(user.Username, user.Password);
            if (checkUser == null)
                return StatusCode(500, "Could not save the user");
            return Ok(newUser);
        }
        
        
        [HttpPut("edit-user")]
        public ActionResult<DummyUser> UpdateMyAccount([FromBody] DummyUser user)
        {
            var updatedUser = _dummyDatabase.UpdateUser(user.UserId, user.FirstName, user.Username, user.Password);
            if (updatedUser == null)
                return NotFound("User not found.");
            return Ok(updatedUser);
        }
        
        
        
    }
}