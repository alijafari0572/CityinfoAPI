using Microsoft.AspNetCore.Mvc;

namespace CityinfoAPI.Controllers
{
    [Route("api/Cities")]
    [ApiController]
    //[Route("api/[controller]")]
    public class CitiesController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetCities()
        {
            return Ok(CitiesDataStore.current.Cities);
        }
        [HttpGet("{id}")]
        public IActionResult GetCities(int id)
        {
            var city = CitiesDataStore.current.Cities
                .FirstOrDefault(c => c.Id == id);
            return Ok(city);
        }
    }
}
