using Microsoft.AspNetCore.Mvc;

namespace CityinfoAPI.Controllers
{
    [Route("api/Cities")]
    [ApiController]
    //[Route("api/[controller]")]
    public class CitiesController : ControllerBase
    {
        private readonly CitiesDataStore _dataStore;
        public CitiesController(CitiesDataStore dataStore)
        {
            _dataStore = dataStore;
        }

        [HttpGet]
        public IActionResult GetCities()
        {
            return Ok(_dataStore.Cities);
        }
        [HttpGet("{id}")]
        public IActionResult GetCities(int id)
        {
            var city = _dataStore.Cities
                .FirstOrDefault(c => c.Id == id);
            if (city == null)
            {
                return NotFound();
            }
            return Ok(city);
        }
    }
}
