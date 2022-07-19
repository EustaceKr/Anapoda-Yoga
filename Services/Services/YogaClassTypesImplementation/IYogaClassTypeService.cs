using Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.YogaClassTypesImplementation
{
    public interface IYogaClassTypeService
    {
        Task<IEnumerable<YogaClassType>> GetYogaClassTypes();
        Task<YogaClassType> GetYogaClassType(string id);
        void CreateYogaClassType(YogaClassType customer);
        void UpdateYogaClassType(YogaClassType yogaClassType);
        void DeleteYogaClassType(YogaClassType yogaClassType);
        Task<bool> Complete();
    }
}
