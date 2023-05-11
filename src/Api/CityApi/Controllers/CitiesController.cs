using City_Appilcation.Contracts.Persistance;
using City_Appilcation.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace CityinfoAPI.Controllers
{
    [Route("api/Cities")]
    [ApiController]
    //[Route("api/[controller]")]
    public class CitiesController : ControllerBase
    {
        private readonly ICityInfoRepository _cityInfoRepository;
        public CitiesController(ICityInfoRepository cityInfoRepository)
        {
            _cityInfoRepository = cityInfoRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CityWithoutPointOfInterestDto>>> GetCities()
        {
            var Cities = await _cityInfoRepository.GetCitiesAsync();

            var result = new List<CityWithoutPointOfInterestDto>();

            foreach (var city in Cities)
            {
                result.Add(new CityWithoutPointOfInterestDto()
                {
                    Id = city.Id,
                    Description = city.Description,
                    Name = city.Name
                });
            }

            return Ok(result);
            // return Ok(citiesDataStore.Cities);
        }
        [HttpGet("{id}")]
        public IActionResult GetCities(int id)
        {
            return Ok();
        }
    }
}
