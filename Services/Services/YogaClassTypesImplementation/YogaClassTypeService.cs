using Data.Entities;
using Data.Repositories.YogaClassTypeDAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.YogaClassTypesImplementation
{
    public class YogaClassTypeService : IYogaClassTypeService
    {
        private readonly IYogaClassTypeRepository _repository;

        public YogaClassTypeService(IYogaClassTypeRepository repository)
        {
            _repository = repository;
        }
        
        public async Task<IEnumerable<YogaClassType>> GetYogaClassTypes()
        {
            return await _repository.GetAllYogaClassTypesAsync();
        }

        public async Task<YogaClassType> GetYogaClassType(string id)
        {
            return await _repository.GetYogaClassTypeByIdAsync(id);
        }

        public void CreateYogaClassType(YogaClassType yogaClassType)
        {
            if (yogaClassType == null) throw new ArgumentNullException(nameof(yogaClassType));
            yogaClassType.YogaClassTypeId = Guid.NewGuid().ToString();
            _repository.Create(yogaClassType);
        }

        public void DeleteYogaClassType(YogaClassType yogaClassType)
        {
            if (yogaClassType == null) throw new ArgumentNullException(nameof(yogaClassType));
            _repository.Delete(yogaClassType);
        }

        public void UpdateYogaClassType(YogaClassType yogaClassType)
        {
            if (yogaClassType == null) throw new ArgumentNullException(nameof(yogaClassType));
            _repository.Update(yogaClassType);
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
    }
}
