using AutoMapper;
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
        private readonly IMapper _mapper;
        public CitiesController(ICityInfoRepository cityInfoRepository, IMapper mapper)
        {
            _cityInfoRepository = cityInfoRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CityWithoutPointOfInterestDto>>> GetCities()
        {
            var Cities = await _cityInfoRepository.GetCitiesAsync();

            return Ok(
                _mapper.Map<IEnumerable<CityWithoutPointOfInterestDto>>(Cities)
                );
        }
        [HttpGet("{id}")]
        public IActionResult GetCities(int id)
        {
            return Ok();
        }
    }
}
