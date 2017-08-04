using ProtoBuf;

namespace ShipmentsDtoTypes.Models
{
    [ProtoContract]
    [ProtoInclude(100, typeof(ShipmentDto))]
    [ProtoInclude(101, typeof(ShipmentToCreateDto))]
    public abstract class ShipmentBaseDto
    {        
        [ProtoMember(2)]
        public string OrderId { get; set; }
        
        [ProtoMember(3)]
        public int CustomerId { get; set; }
        
        [ProtoMember(4)]
        public string Status { get; set; }
        
    }
}