using Data.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.CustomerImplementation
{
    public interface ICustomerService
    {
        Task<IEnumerable<Customer>> GetCustomers();
        Task<Customer> GetCustomer(string id);
        Task<IdentityResult> CreateCustomer(Customer customer,string password);
        void UpdateCustomer(Customer customer);
        void DeleteCustomer(Customer customer);
        Task<bool> Complete();
    }
}
