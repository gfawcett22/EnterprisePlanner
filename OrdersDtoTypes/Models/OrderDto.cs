using System;
using System.ComponentModel.DataAnnotations;
using ProtoBuf;

namespace OrdersDtoTypes.Models
{
    public class OrderDto : OrderBaseDto
    {
        [Key]
        [Required]
        [ProtoMember(1)]
        public int Id { get; set; }
    }
}
