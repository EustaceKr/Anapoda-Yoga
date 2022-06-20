using Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories.CustomerDAO
{
    internal class CustomerRepository : ICustomerRepository
    {
        public bool Create(Customer item)
        {
            throw new NotImplementedException();
        }

        public bool Delete(Customer item)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Customer> GetAll()
        {
             List<Customer> customers = new List<Customer>
            {
                new Customer(2,"lala","lolo"),
                new Customer(3,"lele","lulu")
            };

            return customers;
        }

        public Customer GetById(Guid id)
        {
            return new Customer(1,"St","Kr");
        }

        public bool SaveChangesAsync()
        {
            throw new NotImplementedException();
        }

        public bool Update(Customer item)
        {
            throw new NotImplementedException();
        }
    }
}
