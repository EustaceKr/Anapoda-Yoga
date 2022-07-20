﻿using Data.Entities;
using Data.Repositories.GenericDAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories.YogaClassDAO
{
    public interface IYogaClassRepository : IGenericRepository<YogaClass>
    {
        Task<IEnumerable<YogaClass>> GetAllYogaClassesByDateAsync(DateTime time);
        Task<YogaClass> GetYogaClassByIdAsync(string customerId);
    }
}