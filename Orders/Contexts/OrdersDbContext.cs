using Microsoft.EntityFrameworkCore;
using Orders.Entities;

namespace Orders.Contexts
{
    public class OrdersDbContext : DbContext
    {
        public OrdersDbContext(DbContextOptions<OrdersDbContext> options): base(options) {}
        public DbSet<Order> Orders { get; set; }
    }
}
