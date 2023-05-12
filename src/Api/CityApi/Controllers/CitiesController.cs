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
            _cityInfoRepository = cityInfoRepository ??
                throw new ArgumentNullException(nameof(cityInfoRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
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
        public async Task<IActionResult>
            GetCity(int id, bool includePointsOfInterest = false)
        {
            var city = await _cityInfoRepository.GetCityAsync(id, includePointsOfInterest);
            if (city == null)
            {
                return NotFound();
            }

            if (includePointsOfInterest)
            {
                return Ok(_mapper.Map<CityDto>(city));
            }

            return Ok(_mapper.Map<CityWithoutPointOfInterestDto>(city));

        }
    }
}
