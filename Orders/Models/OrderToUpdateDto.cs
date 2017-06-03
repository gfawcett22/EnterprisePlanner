using System;
using Customers.Entities;

namespace Orders.Models
{
    public class OrderToUpdateDto
    {
        public DateTime DatePlaced { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
    }
}
