using ProtoBuf;

namespace CustomersDtoTypes.Models
{
    [ProtoContract]
    public class CustomerDto
    {
        [ProtoMember(1)]
        public int Id { get; set; }
        [ProtoMember(2)]
        public string Name { get; set; }
        [ProtoMember(3)]
        public string Address { get; set; }
        [ProtoMember(4)]
        public string Business { get; set; }
    }
}
