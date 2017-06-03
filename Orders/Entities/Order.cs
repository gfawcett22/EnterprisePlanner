using Customers.Entities;
using System;

namespace Orders.Entities
{
    public class Order
    {
        public Order()
        {
        }
        public int Id { get; set; }
        public DateTime DatePlaced { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
    }
}
