﻿using System;
using System.Collections.Generic;
using System.Linq;
using Customers.Entities;
using CustomersDtoTypes.Helpers;
using WebApiHelpers;

namespace Customers.Repositories
{
    public interface ICustomerRepository
    {
			IQueryable<Customer> GetCustomers(CustomersPagingParameters pagingParameters);
			Customer GetCustomer(int customerId);
            IQueryable<Customer> GetCustomers(IEnumerable<int> customerIds);
			int GetResultCount();
			void AddCustomer(Customer customer);
			void DeleteCustomer(Customer customer);
			void UpdateCustomer(Customer customer);
			bool CustomerExists(int customerId);
			bool Save();
    }
}
