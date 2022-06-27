using Data.Entities;
using Data.Repositories.CustomerDAO;
using Microsoft.AspNetCore.Identity;
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
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<Customer> _userManager;

        public CustomerService(ICustomerRepository repository, RoleManager<IdentityRole> roleManager, UserManager<Customer> userManager)
        {
            _repository = repository;
            _roleManager = roleManager;
            _userManager = userManager;
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

        public async Task<IdentityResult> CreateCustomer(Customer customer,string password)
        {
            if (customer == null) throw new ArgumentNullException(nameof(customer));
            customer.Id = Guid.NewGuid().ToString();
            var result = await _userManager.CreateAsync(customer, password);
            await _userManager.AddToRoleAsync(customer, CustomUserRoles.User);
            return result;
        }

        public async Task<Customer> GetCustomer(string id)
        {
            return await _repository.GetCustomerByIdAsync(id);
        }

        public async Task<IEnumerable<Customer>> GetCustomers()
        {
            return await _repository.GetAllCustomersAsync();
        }

        public void UpdateCustomer(Customer customer)
        {
            if (customer == null) throw new ArgumentNullException(nameof(customer));
            customer.UpdatedDate = DateTime.Now;
            _repository.Update(customer);
        }

        public void DeleteCustomer(Customer customer)
        {
            if (customer == null) throw new ArgumentNullException(nameof(customer));
            _repository.Delete(customer);
        }
    }
}
