using System;

namespace OrdersDtoTypes.Models
{
    public class OrderDto
    {
        public int Id { get; set; }
        public DateTime DatePlaced { get; set; }
        public int CustomerId { get; set; }
    }
}
