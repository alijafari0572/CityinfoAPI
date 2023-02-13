using CityApi.Models;
using CityinfoAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CityinfoAPI.Controllers
{
    [Route("api/cities/{cityid}/pointsofinterest")]
    [ApiController]
    public class PointsOfInterestController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetPointsOfInterst(int cityid) 
        {
            var city=CitiesDataStore.current.Cities.FirstOrDefault(c=>c.Id==cityid);
            if (city==null)
            {
                return NotFound();
            }
            return Ok(city.PointsOfInterest);
        }


        [HttpGet("{pointOfInterestId}")]
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

        //[HttpPost]
        //public ActionResult<PointOfInterestDto> CreatePointOfInterest(
        //   int cityId,
        //   PointOfInterestForCreationDto pointOfInterest
        //   )
        //{
        //    var city = CitiesDataStore.current
        //        .Cities.FirstOrDefault(c => c.Id == cityId);
        //    if (city == null)
        //    {
        //        return NotFound();
        //    }

        //    var maxpointOfInterestId = CitiesDataStore.current.Cities
        //        .SelectMany(c => c.PointsOfInterest)
        //        .Max(p => p.Id);

        //    var createPoint = new PointOfInterestDto()
        //    {
        //        Id = ++maxpointOfInterestId,
        //        Name = pointOfInterest.Name,
        //        Description = pointOfInterest.Description
        //    };

        //    city.PointsOfInterest.Add(createPoint);



        //    return CreatedAtAction("GetPointOfInterest",
        //        new
        //        {
        //            cityId = cityId,
        //            pointOfInterestId = createPoint.Id

        //        },
        //        createPoint
        //        );
        //}
    }
}
