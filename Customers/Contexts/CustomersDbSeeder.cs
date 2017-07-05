using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Customers.Entities;

namespace Customers.Contexts
{
    public static class CustomersDbSeeder
    {
        public static void SeedInMemoryDatabase(CustomersDbContext context)
        {
            List<Customer> customers = new List<Customer>()
            {
                new Customer {Id=1, Name = "Test", Address = "123 Cherry St", Business = "Production"},
                new Customer {Id=2, Name = "Test2", Address = "123 Apple St", Business = "Recreation"},
                new Customer {Id=3, Name = "Test3", Address = "123 Plum St", Business = "Sporting Goods"}
            };
            context.Customers.AddRange(customers);
            context.SaveChanges();
        }
    }
}
