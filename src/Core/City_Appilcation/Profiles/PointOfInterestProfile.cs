using AutoMapper;
using City_Appilcation.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace City_Appilcation.Profiles
{
    public class PointOfInterestProfile : Profile
    {
        public PointOfInterestProfile()
        {
            CreateMap<City_Domain.PointOfInterest, City_Appilcation.DTOs.PointOfInterestDto>();
            CreateMap<City_Appilcation.DTOs.PointOfInterestForCreationDto, City_Domain.PointOfInterest>();
            CreateMap<City_Appilcation.DTOs.PointOfInterestForUpdateDto, City_Domain.PointOfInterest>();
            
        }
    }
}
