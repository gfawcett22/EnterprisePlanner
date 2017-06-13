using System;

namespace Orders.Models
{
    public class OrderToUpdateDto
    {
        public DateTime DatePlaced { get; set; }
        public int CustomerId { get; set; }
    }
}
