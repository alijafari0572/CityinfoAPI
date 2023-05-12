﻿using AutoMapper;
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
        }
    }
}
