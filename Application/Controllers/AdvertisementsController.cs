using Application.DTOs.InputModels;
using Application.DTOs.ViewModels;
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

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] bool userOnly = true)
        {
            return Ok(new List<AdvertismentResponse>()
                {
                new AdvertismentResponse()
                {
                    City = "City",
                    Description = "Desc",
                    Name = "Pagiegiai",
                    Number = 56,
                    Size = 99
                }
            }
            );
        }
    }
}
