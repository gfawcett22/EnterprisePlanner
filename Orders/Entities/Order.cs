using System;

namespace Orders.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime DatePlaced { get; set; }        
        public int CustomerId { get; set; }
    }
}
