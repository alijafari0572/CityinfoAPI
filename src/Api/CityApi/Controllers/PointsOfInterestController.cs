﻿using CityApi.Models;
using CityinfoAPI.Models;
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

        public PointsOfInterestController(ILogger<PointsOfInterestController> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet]
        public IActionResult GetPointsOfInterst(int cityid) 
        {
            try
            {
                throw new Exception("Exeption sample ...");
                var city = CitiesDataStore.current.Cities.FirstOrDefault(c => c.Id == cityid);
                if (city == null)
                {
                    _logger.LogInformation($"City with id {cityid} wasn't found");
                    return NotFound();
                }
                return Ok(city.PointsOfInterest);
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Exeption getting  {cityid}", ex);
                return StatusCode(500, "A Problem happend while ....");
            }
            
        }


        [HttpGet("{pointofinterestId}", Name = "GetPointOfInterest")]
        public IActionResult GetPointOfInterst(int cityid,int pointofinterestId)
        {
            var city = CitiesDataStore.current.Cities.FirstOrDefault(c => c.Id == cityid);
            if (city == null)
            {
                return NotFound();
            }
            var point=city.PointsOfInterest.FirstOrDefault(p=>p.Id==pointofinterestId);
            if (point==null)
            {
                return NotFound();
            }
            return Ok(point);
        }

        #region  Post
        [HttpPost]
        public ActionResult<PointOfInterestDto> CreatePointOfInterest(
          int cityId,
          PointOfInterestForCreationDto pointOfInterest
          )
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var city = CitiesDataStore.current
                .Cities.FirstOrDefault(c => c.Id == cityId);
            if (city == null)
            {
                return NotFound();
            }

            var maxpointOfInterestId = CitiesDataStore.current.Cities
                .SelectMany(c => c.PointsOfInterest)
                .Max(p => p.Id);

            var createPoint = new PointOfInterestDto()
            {
                Id = ++maxpointOfInterestId,
                Name = pointOfInterest.Name,
                Description = pointOfInterest.Description
            };

            city.PointsOfInterest.Add(createPoint);



            return CreatedAtAction("GetPointOfInterest",
                new
                {
                    cityId = cityId,
                    pointOfInterestId = createPoint.Id

                },
                createPoint
                );
        }
        #endregion

        #region Edit
        [HttpPut("{pontiOfInterestId}")]
        public ActionResult UpdatePointOfInterest(int cityId,
            int pontiOfInterestId,
            PointOfInterestForUpdateDto pointOfInterest)
        {
            //find  city
            var city = CitiesDataStore.current.Cities
                .FirstOrDefault(c => c.Id == cityId);
            if (city == null)
                return NotFound();

            // find point of interest
            var point = city.PointsOfInterest
                .FirstOrDefault(p => p.Id == pontiOfInterestId);
            if (point == null)
                return NotFound();

            point.Name = pointOfInterest.Name;
            point.Description = pointOfInterest.Description;

            return NoContent();

        }
        #endregion

        #region  Edit with patch
        [HttpPatch("{pontiOfInterestid}")]
        public ActionResult PartiallyUpdatePointOfOnterest(
            int cityId,
            int pontiOfInterestid,
            JsonPatchDocument<PointOfInterestForUpdateDto> patchDocument
            )
        {
            //find  city
            var city = CitiesDataStore.current.Cities
                .FirstOrDefault(c => c.Id == cityId);
            if (city == null)
                return NotFound();

            // find point of interest
            var pointOfInterestFromStore = city.PointsOfInterest
                .FirstOrDefault(p => p.Id == pontiOfInterestid);
            if (pointOfInterestFromStore == null)
                return NotFound();

            var pointOfInterestToPatch = new PointOfInterestForUpdateDto()
            {
                Name = pointOfInterestFromStore.Name,
                Description = pointOfInterestFromStore.Description
            };

            patchDocument.ApplyTo(pointOfInterestToPatch, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            if (!TryValidateModel(pointOfInterestToPatch))
            {
                return BadRequest(modelState: ModelState);
            }
            pointOfInterestFromStore.Name = pointOfInterestToPatch.Name;
            pointOfInterestFromStore.Description = pointOfInterestToPatch.Description;

            return NoContent();
        }

        #endregion

        #region Delete

        [HttpDelete("{pontiOfInterestId}")]
        public ActionResult DeletePointOfInterest(
            int cityId,
            int pontiOfInterestId)
        {
            //find  city
            var city = CitiesDataStore.current.Cities
                .FirstOrDefault(c => c.Id == cityId);
            if (city == null)
                return NotFound();

            // find point of interest
            var point = city.PointsOfInterest
                .FirstOrDefault(p => p.Id == pontiOfInterestId);
            if (point == null)
                return NotFound();

            city.PointsOfInterest.Remove(point);

            return NoContent();
        }
        #endregion
    }
}
