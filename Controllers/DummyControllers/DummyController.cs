using Microsoft.AspNetCore.Mvc;

namespace Workshop_Basics.Controllers.DummyControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DummyController : ControllerBase 
    {
        [HttpGet("montana")]
        public string GetMessage()
        {
            return "Say hello to my little friend!";
        }

        [HttpGet("anotherPhrase")]
        public string GetAnotherMessage()
        {
            return "I always tell the truth. Even when I lie";
        }
        
        
    }
}