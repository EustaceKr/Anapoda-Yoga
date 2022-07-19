﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.YogaClassTypeDTOs
{
    public class YogaClassTypeReadDTO
    {
        public string YogaClassTypeId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Capacity { get; set; }
        public string Duration { get; set; }
    }
}
