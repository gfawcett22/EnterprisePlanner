using System;
using Customers.Entities;

namespace Orders.Models
{
    public class OrderDto
    {
        public int Id { get; set; }
        public DateTime DatePlaced { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
    }
}
