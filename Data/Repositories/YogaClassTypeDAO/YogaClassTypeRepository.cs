using Data.Context;
using Data.Entities;
using Data.Repositories.GenericDAO;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories.YogaClassTypeDAO
{
    public class YogaClassTypeRepository: GenericRepository<YogaClassType>, IYogaClassTypeRepository
    {
        public YogaClassTypeRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<YogaClassType>> GetAllYogaClassTypesAsync()
        {
            return await FindAll().OrderBy(c => c.Name).ToListAsync();
        }
        public async Task<YogaClassType> GetYogaClassTypeByIdAsync(string yogaClassTypeId)
        {
            return await FindByCondition(yogaClassType => yogaClassType.YogaClassTypeId.Equals(yogaClassTypeId)).FirstOrDefaultAsync();
        }
    }
}
