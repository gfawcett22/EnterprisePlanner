using System;

namespace Orders.Models
{
    public class OrderToCreateDto
    {
        public DateTime DatePlaced { get; set; }
        public int CustomerId { get; set; }
    }
}
