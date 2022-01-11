using Microsoft.AspNetCore.Mvc;

namespace DigitalDoggy.Controllers
{
    [Route("api")]
    [ApiController]
    public class AppController : ControllerBase
    {
        public AppController()
        {

        }

        [HttpGet("ping")]
        public IActionResult Ping()
        {
            return Ok("Dogs house service. Version 1.0.1");
        }
    }
}
