using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories.GenericDAO
{
    public interface IGenericRepository<T>
    {
        IEnumerable<T> GetAll();
        T GetById(Guid id);
        bool Create(T item);
        bool Update(T item);
        bool Delete(T item);
        bool SaveChangesAsync();
    }
}
