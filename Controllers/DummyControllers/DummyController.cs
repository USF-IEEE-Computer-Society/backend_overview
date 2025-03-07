using Microsoft.AspNetCore.Mvc;
using Workshop_Basics.Models;

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
        
        [HttpPut("using-query-string/{param}")]
        public string QueryString(string param)
        {
            return "Lets see if works, " + param;
        }
        
        [HttpPut("using-http-body")] 
        public string QueryString([FromBody] Credentials credential)
        {
            return $"Your nickname: {credential.nickname}, Your password: {credential.password}";
        }
        
        
        
        
    }
}