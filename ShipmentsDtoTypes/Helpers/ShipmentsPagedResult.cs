using System.Collections.Generic;
using ProtoBuf;
using ShipmentsDtoTypes.Models;

namespace ShipmentsDtoTypes.Helpers
{
    [ProtoContract(IgnoreListHandling=true)]
    public class ShipmentsPagedResult
    {
        [ProtoMember(1)]
        public int TotalResultCount { get; set; }
        [ProtoMember(2)]
        public List<ShipmentDto> data { get; set; }
    }
}