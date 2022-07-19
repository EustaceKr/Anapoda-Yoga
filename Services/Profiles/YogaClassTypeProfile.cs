using Application.DTOs.YogaClassTypeDTOs;
using AutoMapper;
using Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Profiles
{
    public class YogaClassTypeProfile : Profile
    {
        public YogaClassTypeProfile()
        {
            CreateMap<YogaClassType, YogaClassTypeReadDTO>();
            CreateMap<YogaClassTypeCreateDTO, YogaClassType>();
            CreateMap<YogaClassTypeUpdateDTO, YogaClassType>();
            CreateMap<YogaClassType, YogaClassTypeUpdateDTO>();
        }
    }
}
