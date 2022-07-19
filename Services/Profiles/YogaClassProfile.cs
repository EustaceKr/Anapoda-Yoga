using Application.DTOs.YogaClassDTOs;
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
    public class YogaClassProfile : Profile
    {
        public YogaClassProfile()
        {
            CreateMap<YogaClass, YogaClassReadDTO>();
            CreateMap<YogaClassCreateDTO, YogaClass>();
            CreateMap<YogaClassUpdateDTO, YogaClass>();
            CreateMap<YogaClass, YogaClassUpdateDTO>();
        }
    }
}
