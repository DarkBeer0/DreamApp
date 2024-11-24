using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DreamApp.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class DebugController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok("Олег работай!");
        }
    }
}
