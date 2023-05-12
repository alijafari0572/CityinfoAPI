using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace City_Appilcation.Profiles
{
    public class CityProfile : Profile
    {
        public CityProfile()
        {
            CreateMap<City_Domain.City, City_Appilcation.DTOs.CityWithoutPointOfInterestDto>();
            CreateMap<City_Domain.City, City_Appilcation.DTOs.CityDto>();
        }
    }
}
