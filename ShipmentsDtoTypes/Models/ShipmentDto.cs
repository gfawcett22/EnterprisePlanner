using ProtoBuf;

namespace ShipmentsDtoTypes.Models
{
    [ProtoContract]
    public class ShipmentDto : ShipmentBaseDto
    {
        [ProtoMember(1)]
        public int Id { get; set; }
    }
}