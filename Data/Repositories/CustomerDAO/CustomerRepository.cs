﻿using Data.Context;
using Data.Entities;
using Data.Repositories.GenericDAO;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
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
            return await FindAll().Where(x => x.Id != "3b092af1-01ba-4c51-9dce-2c0db982a49d").OrderBy(c => c.LastName).ToListAsync();
        }
        public async Task<IEnumerable<Customer>> GetCustomersInClassAsync(string yogaClassId)
        {
            return await FindAll()
                .Where(x => x.Id != "3b092af1-01ba-4c51-9dce-2c0db982a49d")
                .Where(x => x.Reservations.Where(y => y.YogaClassId == yogaClassId)
                .Any())
                .OrderBy(c => c.LastName)
                .ToListAsync();
        }

        public async Task<IEnumerable<Customer>> GetCustomersNotInClassAsync(string yogaClassId)
        {
            return await FindAll()
                .Where(x => x.Id != "3b092af1-01ba-4c51-9dce-2c0db982a49d")
                .Where(x => x.TimesPerMonth > 0)
                .Where(x => !x.Reservations.Where(y => y.YogaClassId == yogaClassId)
                .Any())
                .OrderBy(c => c.LastName)
                .ToListAsync();
        }

        public async Task<Customer> GetCustomerByIdAsync(string customerId)
        {
            return await FindByCondition(customer => customer.Id.Equals(customerId)).FirstOrDefaultAsync();
        }

        public async Task<Customer> GetUserIdFromUserName(string userName)
        {
            return await (FindByCondition(customer => customer.UserName == userName)).FirstOrDefaultAsync();
        }
    }
}
