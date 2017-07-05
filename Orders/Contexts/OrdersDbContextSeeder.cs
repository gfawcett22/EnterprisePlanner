using System;
using System.Collections.Generic;
using Orders.Entities;

namespace Orders.Contexts
{
    public static class OrdersDbContextSeeder
    {
        public static void SeedInMemoryDatabase(OrdersDbContext context)
        {
            List<Order> orders = new List<Order>()
            {
                new Order {DatePlaced = DateTime.Now, CustomerId=1},
                new Order {DatePlaced = DateTime.Now, CustomerId=1},
                new Order {DatePlaced = DateTime.Now, CustomerId=1},
                new Order {DatePlaced = DateTime.Now, CustomerId=2},
                new Order {DatePlaced = DateTime.Now, CustomerId=3},
                new Order {DatePlaced = DateTime.Now, CustomerId=3},
            };
            context.Orders.AddRange(orders);
            context.SaveChanges();
        }
    }
}