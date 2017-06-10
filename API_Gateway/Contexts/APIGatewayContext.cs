using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API_Gateway.Entities;
using Microsoft.EntityFrameworkCore;

namespace API_Gateway.Contexts
{
    public class ApiGatewayContext : DbContext
    {
        public ApiGatewayContext(DbContextOptions<ApiGatewayContext> options) : base(options) { }
        public DbSet<Endpoint> Endpoints { get; set; }
    }

}
