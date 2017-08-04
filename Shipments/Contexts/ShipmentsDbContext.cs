using Microsoft.EntityFrameworkCore;
using Shipments.Entities;

namespace Shipments.Contexts
{
    public class ShipmentsDbContext : DbContext
    {
        public ShipmentsDbContext(DbContextOptions<ShipmentsDbContext> options): base(options) {}
        public DbSet<Shipment> Shipments { get; set; }
    }
}