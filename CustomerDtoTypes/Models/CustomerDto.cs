using System.ComponentModel.DataAnnotations;
using ProtoBuf;

namespace CustomersDtoTypes.Models
{
    [ProtoContract]
    public class CustomerDto : CustomerBaseDto
    {
        [ProtoMember(1)]
        [Required]
        public int Id { get; set; }       
    }
}
