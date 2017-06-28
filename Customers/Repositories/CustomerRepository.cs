using System;
using System.Collections.Generic;
using Customers.Entities;
using System.Linq;
using Customers.Contexts;
using CustomersDtoTypes.Helpers;
using WebApiHelpers;

namespace Customers.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private CustomersDbContext _context;
        public CustomerRepository(CustomersDbContext context)
        {
            _context = context;
        }

        public void AddCustomer(Customer customer)
        {
            _context.Customers.Add(customer);
        }

        public bool CustomerExists(int customerId)
        {
            return _context.Customers.Any(c => c.Id == customerId);
        }

        public void DeleteCustomer(Customer customer)
        {
            _context.Customers.Remove(customer);
        }

        public Customer GetCustomer(int customerId)
        {
            return _context.Customers.FirstOrDefault(c => c.Id == customerId);
        }

        public IQueryable<Customer> GetCustomers(CustomersPagingParameters pagingParameters)
        {
            return _context.Customers
                .Where(c => c.Name.Contains(pagingParameters.Name) 
                            || c.Address.Contains(pagingParameters.Address)
                            || c.Business.Contains(pagingParameters.Business))
                .Skip(pagingParameters.PageSize * (pagingParameters.PageNumber - 1))
                .Take(pagingParameters.PageSize);
        }

        public IQueryable<Customer> GetCustomers(IEnumerable<int> customerIds)
        {
            return _context.Customers.Where(c => customerIds.Contains(c.Id));
        }

        public bool Save()
        {
            return (_context.SaveChanges() >= 1);
        }

        public void UpdateCustomer(Customer customer)
        {
			_context.Customers.Update(customer);
			_context.Entry(customer).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        }
    }
}
