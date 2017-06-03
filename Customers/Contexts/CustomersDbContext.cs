using Microsoft.EntityFrameworkCore;
using Customers.Entities;

namespace Customers.Contexts
{
    public class CustomersDbContext : DbContext
	{
		public CustomersDbContext(DbContextOptions<CustomersDbContext> options) : base(options) { }
		public DbSet<Customer> Customers { get; set; }
	}
}
