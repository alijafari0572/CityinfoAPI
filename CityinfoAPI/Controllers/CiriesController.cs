using Microsoft.AspNetCore.Mvc;

namespace CityinfoAPI.Controllers
{
    [ApiController]
    public class CiriesController : ControllerBase
    {
        [HttpGet("api/Cities")]
        public JsonResult GetCities()
        {
            return new JsonResult(new List<object>
            {
            new {id=1,name="Tehran"},
            new {id=2,name="shiraz"},
            });
        }
    }
}
