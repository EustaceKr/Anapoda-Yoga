﻿using Data.Entities;
using Data.Repositories.GenericDAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories.CustomerDAO
{
    public interface ICustomerRepository : IGenericRepository<Customer>
    {
        Task<IEnumerable<Customer>> GetAllCustomersAsync();
        Task<IEnumerable<Customer>> GetCustomersInClassAsync(string yogaClassId);
        Task<IEnumerable<Customer>> GetCustomersNotInClassAsync(string yogaClassId);
        Task<Customer> GetCustomerByIdAsync(string customerId);
        Task<Customer> GetUserIdFromUserName(string userName);
    }
}
