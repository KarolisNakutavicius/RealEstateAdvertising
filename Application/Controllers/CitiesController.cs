using Microsoft.AspNetCore.Mvc;

namespace Application.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CitiesController
    {
        [HttpGet]
        public async Task<IActionResult> GetAllCities(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
