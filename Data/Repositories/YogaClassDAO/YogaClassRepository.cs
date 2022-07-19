using Data.Context;
using Data.Entities;
using Data.Repositories.GenericDAO;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories.YogaClassDAO
{
    public class YogaClassRepository : GenericRepository<YogaClass>,IYogaClassRepository
    {
        public YogaClassRepository(ApplicationDbContext context) : base(context)
        {
        }
        public async Task<IEnumerable<YogaClass>> GetAllYogaClassesAsync()
        {
            return await FindAll().OrderBy(c => c.Date).ToListAsync();
        }
        public async Task<YogaClass> GetYogaClassByIdAsync(string yogaClassId)
        {
            return await FindByCondition(yogaClass => yogaClass.YogaClassId.Equals(yogaClassId)).FirstOrDefaultAsync();
        }
    }
}
