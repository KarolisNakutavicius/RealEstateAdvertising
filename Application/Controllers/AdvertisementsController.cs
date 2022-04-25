using Application.DTOs.InputModels;
using Application.DTOs.ViewModels;
using Application.Extensions;
using Application.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Application.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class Advertisements : ControllerBase
    {
        private readonly IAdvertisementService _advertisementService;

        public Advertisements(IAdvertisementService advertisementService)
        {
            _advertisementService = advertisementService;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create([FromForm] CreateAdvertisementRequest request, CancellationToken cancellationToken)
        {
            var result = await _advertisementService.CreateNewAdvertisement(request, cancellationToken);

            return result.ToHttpResponse();
        }

        [HttpGet("mine")]
        [Authorize]
        public async Task<IActionResult> GetCurrentUserAds(CancellationToken cancellationToken)
        {
            var result = await _advertisementService.GetAllUsersAdvertisements(cancellationToken);

            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAds([FromQuery] FilterRequest request, CancellationToken cancellationToken)
        {
            var result = await _advertisementService.GetAll(request, cancellationToken);

            return result.ToHttpResponse();
        }
    }
}
