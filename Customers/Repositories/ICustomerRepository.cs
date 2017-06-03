using System;
using System.Collections.Generic;
using Customers.Entities;

namespace Customers.Repositories
{
    public interface ICustomerRepository
    {
			IEnumerable<Customer> GetCustomers();
			Customer GetCustomer(int customerId);
			IEnumerable<Customer> GetCustomers(IEnumerable<int> customerIds);
			void AddCustomer(Customer customer);
			void DeleteCustomer(Customer customer);
			void UpdateCustomer(Customer customer);
			bool CustomerExists(int customerId);
			bool Save();
    }
}
