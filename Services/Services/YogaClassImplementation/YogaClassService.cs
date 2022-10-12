using Data.Entities;
using Data.Repositories.YogaClassDAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Application.Services.YogaClassImplementation
{
    public class YogaClassService : IYogaClassService
    {
        private readonly IYogaClassRepository _repository;
        public YogaClassService(IYogaClassRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<YogaClass>> GetYogaClassesByDate(DateTime time)
        {
            return await _repository.GetAllYogaClassesByDateAsync(time);
        }

        public async Task<YogaClass> GetYogaClass(string id)
        {
            return await _repository.GetYogaClassByIdAsync(id);
        }

        public void CreateYogaClass(YogaClass yogaClass)
        {
            if(yogaClass == null) throw new ArgumentNullException(nameof(yogaClass));
            yogaClass.YogaClassId = Guid.NewGuid().ToString();
            _repository.Create(yogaClass);
        }

        public void DeleteYogaClass(YogaClass yogaClass)
        {
            if (yogaClass == null) throw new ArgumentNullException(nameof(yogaClass));
            _repository.Delete(yogaClass);
        }

        public void UpdateYogaClass(YogaClass yogaClass)
        {
            if (yogaClass == null) throw new ArgumentNullException(nameof(yogaClass));
            _repository.Update(yogaClass);
        }

        public async Task<bool> Complete()
        {
            try
            {
                return await _repository.SaveChangesAsync();
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<IEnumerable<YogaClass>> GetAllYogaClasses()
        {
            return await _repository.GetAllYogaClassesAsync();
        }
    }
}
