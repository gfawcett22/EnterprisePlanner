using ProtoBuf;

namespace CustomersDtoTypes.Models
{
    [ProtoContract]
    public class CustomerToCreateDto
    {
        [ProtoMember(1)]
        public string Name { get; set; }
        [ProtoMember(2)]
        public string Address { get; set; }
        [ProtoMember(3)]
        public string Business { get; set; }
    }
}
