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

        public Customer GetCustomer(int id)
        {
           return _repository.GetById(id);
        }

        public IEnumerable<Customer> GetCustomers()
        {
            return _repository.GetAll();
        }
    }
}
