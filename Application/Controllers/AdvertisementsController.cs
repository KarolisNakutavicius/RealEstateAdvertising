using Application.DTOs.InputModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Application.Controllers
{

    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class AdvertismentsController : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateAdvertisementRequest request)
        {
            return Ok(request);
        }
    }
}
