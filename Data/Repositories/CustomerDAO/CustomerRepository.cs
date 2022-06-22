using Data.Context;
using Data.Entities;
using Data.Repositories.GenericDAO;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories.CustomerDAO
{
    public class CustomerRepository : GenericRepository<Customer>, ICustomerRepository
    {
        public CustomerRepository(ApplicationDbContext context) : base(context)
        {
        }
        public async Task<IEnumerable<Customer>> GetAllCustomersAsync()
        {
            return await FindAll().OrderBy(c => c.LastName).ToListAsync();
        }
        public async Task<Customer> GetCustomerByIdAsync(Guid ownerId)
        {
            return await FindByCondition(customer => customer.Id.Equals(ownerId)).FirstOrDefaultAsync();
        }
    }
}
