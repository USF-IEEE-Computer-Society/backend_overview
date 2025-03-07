using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Workshop_Basics.DummyDatabase;
using Workshop_Basics.Models;

namespace Workshop_Basics.Controllers.DummyControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DummyAdminController : ControllerBase
    {
        private readonly UserDummyDatabase _dummyDatabase;

        public DummyAdminController()
        {
            _dummyDatabase = new UserDummyDatabase();
        }
        
        [HttpGet("users")]
        public ActionResult<List<DummyUser>> GetAllUsers()
        {
            return Ok(_dummyDatabase.GetAllUsers());
        }
        
        [HttpDelete("delete-user/{userId}")]
        public ActionResult DeleteUser(int userId)
        {
            var deletedUser = _dummyDatabase.DeleteUser(userId);
            if (deletedUser == null)
                return NotFound();
            return NoContent();
        }
    }
}
