using Data.Entities;
using Data.Repositories.CustomerDAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.CustomerImplementation
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _repository;

        public CustomerService(ICustomerRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> Complete()
        {
            try
            {
                return await _repository.SaveChangesAsync();
            }
            catch(Exception)
            {
                return false;
            }
            
        }

        public void CreateCustomer(Customer customer)
        {
            if (customer == null)
            {
                throw new ArgumentNullException(nameof(customer));
            }

            _repository.Create(customer);
        }

        public async Task<Customer> GetCustomer(Guid id)
        {
            return await _repository.GetCustomerByIdAsync(id);
        }

        public async Task<IEnumerable<Customer>> GetCustomers()
        {
            return await _repository.GetAllCustomersAsync();
        }
    }
}
