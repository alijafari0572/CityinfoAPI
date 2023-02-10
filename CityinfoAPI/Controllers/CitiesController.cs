using Microsoft.AspNetCore.Mvc;

namespace CityinfoAPI.Controllers
{
    [ApiController]
    //[Route("api/[controller]")]
    [Route("api/Cities")]
    public class CitiesController : ControllerBase
    {
        [HttpGet]
        public JsonResult GetCities()
        {
            return new JsonResult(CitiesDataStore.current);
        }
        [HttpGet("{id}")]
        public JsonResult GetCities(int id)
        {
            return new JsonResult(CitiesDataStore.current.cityDtos
                .FirstOrDefault(c=>c.Id==id));
        }
    }
}
