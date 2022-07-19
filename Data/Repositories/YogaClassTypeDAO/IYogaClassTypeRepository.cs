using Data.Entities;
using Data.Repositories.GenericDAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories.YogaClassTypeDAO
{
    public interface IYogaClassTypeRepository : IGenericRepository<YogaClassType>
    {
        Task<IEnumerable<YogaClassType>> GetAllYogaClassTypesAsync();
        Task<YogaClassType> GetYogaClassTypeByIdAsync(string yogaClassTypeId);
    }
}
