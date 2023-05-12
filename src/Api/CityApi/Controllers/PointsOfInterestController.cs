using AutoMapper;
using City_Appilcation.Contracts.Persistance;
using City_Appilcation.DTOs;
using City_Domain;
using CityInfo.API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace CityinfoAPI.Controllers
{
    [Route("api/Cities/{cityid}/pointsofinterest")]
    [ApiController]
    public class PointsOfInterestController : ControllerBase
    {
        private readonly ILogger<PointsOfInterestController> _logger;
        private readonly IMailService _mailService;
        private readonly CitiesDataStore _cityDataStore;
        private readonly ICityInfoRepository _cityInfoRepository;
        private readonly IMapper _mapper;
        public PointsOfInterestController(ILogger<PointsOfInterestController> logger, IMailService mailService, CitiesDataStore cityDataStore, ICityInfoRepository cityInfoRepository, IMapper mapper)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mailService = mailService;
            _cityDataStore = cityDataStore;
            _cityInfoRepository = cityInfoRepository ??
               throw new ArgumentNullException(nameof(cityInfoRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PointOfInterestDto>>>
            GetPointsOfInterest(int cityId)
        {

            if (!await _cityInfoRepository.CityExistsAsync(cityId))
            {
                _logger.LogInformation($"{cityId} Not Found ...");
                return NotFound();
            }

            var pointsOfInterestForCity = await _cityInfoRepository
                .GetPointsOfInterestForCityAsync(cityId);

            return Ok(_mapper.Map<IEnumerable<PointOfInterestDto>>(pointsOfInterestForCity));
        }


        [HttpGet("{pointofinterestId}", Name = "GetPointOfInterest")]
        public async Task<IActionResult> GetPointOfInterestForCityAsync(int cityid,int pointofinterestId)
        {
            if (!await _cityInfoRepository.CityExistsAsync(cityid))
            {
                _logger.LogInformation($"{cityid} Not Found ...");
                return NotFound();
            }
            var pointofinterest=await _cityInfoRepository.GetPointOfInterestForCityAsync(cityid, pointofinterestId);
            if (pointofinterest==null)
            {
                _logger.LogInformation($"{pointofinterestId} Not Found ...");
                return NotFound();
            }
            return Ok(_mapper.Map<PointOfInterestDto>(pointofinterest));
        }

        #region  Post
        [HttpPost]
        public async Task<ActionResult<PointOfInterestDto>> CreatePointOfInterest(
          int cityId,
          PointOfInterestForCreationDto pointOfInterest
          )
        {
            if (!await _cityInfoRepository.CityExistsAsync(cityId))
            {
                return NotFound();
            }

            var finalPoint = _mapper.Map<PointOfInterest>(pointOfInterest);
            await _cityInfoRepository.AddPointOfInterestForCityAsync(
                cityId, finalPoint);
            await _cityInfoRepository.SaveChangesAsync();

            var createdpoint = _mapper.Map<City_Appilcation.DTOs.PointOfInterestDto>(finalPoint);

            return CreatedAtRoute("GetPointOfInterest", new
            {
                cityId = cityId,
                pointOfInterestId = createdpoint.Id
            }, createdpoint);
        }
        #endregion

        #region Edit
        [HttpPut("{pontiOfInterestId}")]
        public async Task<ActionResult> UpdatePointOfInterest(int cityId,
             int pontiOfInterestId,
             PointOfInterestForUpdateDto pointOfInterest)
        {

            if (!await _cityInfoRepository.CityExistsAsync(cityId))
            {
                return NotFound();
            }

            var point = await _cityInfoRepository
                .GetPointOfInterestForCityAsync(cityId, pontiOfInterestId);

            if (point == null)
            {
                return NotFound();
            }

            _mapper.Map(pointOfInterest, point);

            await _cityInfoRepository.SaveChangesAsync();

            return NoContent();

        }
        #endregion

        #region  Edit with patch
        [HttpPatch("{pontiOfInterestid}")]
        public async Task<ActionResult> PartiallyUpdatePointOfOnterest(
            int cityId,
            int pontiOfInterestid,
            JsonPatchDocument<PointOfInterestForUpdateDto> patchDocument
            )
        {
            if (!await _cityInfoRepository.CityExistsAsync(cityId))
            {
                return NotFound();
            }

            var pointEntity = await _cityInfoRepository
                 .GetPointOfInterestForCityAsync(cityId, pontiOfInterestid);
            if (pointEntity == null)
            {
                return NotFound();
            }

            var pointToPatch = _mapper.Map<PointOfInterestForUpdateDto>
                (pointEntity);

            patchDocument.ApplyTo(pointToPatch, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!TryValidateModel(pointToPatch))
            {
                return BadRequest(ModelState);
            }
            _mapper.Map(pointToPatch, pointEntity);
            await _cityInfoRepository.SaveChangesAsync();

            return NoContent();
        }

        #endregion

        #region Delete

        [HttpDelete("{pontiOfInterestId}")]
        public async Task<ActionResult> DeletePointOfInterest(
            int cityId,
            int pontiOfInterestId)
        {

            if (!await _cityInfoRepository.CityExistsAsync(cityId))
            {
                return NotFound();
            }

            var pointOfInterestEntity =
                await _cityInfoRepository
                .GetPointOfInterestForCityAsync(cityId, pontiOfInterestId);

            if (pointOfInterestEntity == null)
            {
                return NotFound();
            }

            _cityInfoRepository.DeletePointOfInterest(pointOfInterestEntity);
            await _cityInfoRepository.SaveChangesAsync();

            _mailService
                .Send(
                "Point Of intrest deleted",
                $"Point Of Interest {pointOfInterestEntity.Name}with id {pointOfInterestEntity.Id}"
                );

            return NoContent();
        }

        #endregion
    }
}
