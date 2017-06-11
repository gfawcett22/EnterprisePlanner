using System;

namespace OrdersDtoTypes.Models
{
    public class OrderToCreateDto
    {
        public DateTime DatePlaced { get; set; }
        public int CustomerId { get; set; }
    }
}
