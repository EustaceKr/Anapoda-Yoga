using Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.YogaClassImplementation
{
    public interface IYogaClassService
    {
        Task<IEnumerable<YogaClass>> GetYogaClassesByDate(DateTime time);
        Task<YogaClass> GetYogaClass(string id);
        void CreateYogaClass(YogaClass yogaClass);
        void UpdateYogaClass(YogaClass yogaClass);
        void DeleteYogaClass(YogaClass yogaClass);
        Task<bool> Complete();
    }
}
