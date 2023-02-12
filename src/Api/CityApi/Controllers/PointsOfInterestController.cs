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


        [HttpGet("{pointofinterestId}")]
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
    }
}
