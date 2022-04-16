using Microsoft.AspNetCore.Mvc;

namespace Application.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        [HttpPost("login")]
        public IActionResult Login([FromBody] TestRequest request)
        {
            return Ok(new TestRequest { Title = $"Saunuolis {request.Title}" });
        }
    }

    public class TestRequest
    {
        public string Title { get; set; }
    }
}
