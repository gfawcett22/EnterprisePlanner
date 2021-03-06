using System;
using System.ComponentModel.DataAnnotations;
using ProtoBuf;

namespace OrdersDtoTypes.Models
{
    [ProtoContract]
    [ProtoInclude(100, typeof(OrderDto))]
    [ProtoInclude(101, typeof(OrderToCreateDto))]
    [ProtoInclude(102, typeof(OrderToUpdateDto))]
    public abstract class OrderBaseDto
    {        
        [ProtoMember(2)]
        public DateTime DatePlaced { get; set; }
        [Required]
        [ProtoMember(3)]
        public int CustomerId { get; set; }
    }
}