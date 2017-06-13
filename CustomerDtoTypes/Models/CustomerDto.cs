using ProtoBuf;

namespace CustomersDtoTypes.Models
{
    [ProtoContract]
    public class CustomerDto : CustomerBaseDto
    {
        [ProtoMember(1)]
        public int Id { get; set; }       
    }
}
