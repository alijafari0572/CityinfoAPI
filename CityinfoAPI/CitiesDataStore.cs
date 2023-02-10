using CityinfoAPI.Models;

namespace CityinfoAPI
{
    public class CitiesDataStore
    {
        public List<CityDto> cityDtos { get; set; }
        public static CitiesDataStore current { get;}=new CitiesDataStore();
        public CitiesDataStore()
        {
            cityDtos = new List<CityDto>(){

                new CityDto(){Id=1,Name="tehran",Description="this is iran"},
                new CityDto(){Id=1,Name="mashhad",Description="this is khorasan"},
                new CityDto(){Id=1,Name="shiraz",Description="this is fars"},
            };
        }
    }
}
