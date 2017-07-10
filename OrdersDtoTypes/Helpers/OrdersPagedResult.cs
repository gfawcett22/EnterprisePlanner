using System.Collections.Generic;
using OrdersDtoTypes.Models;
using ProtoBuf;

namespace OrdersDtoTypes.Helpers
{
    [ProtoContract(IgnoreListHandling=true)]
    public class OrdersPagedResult
    {
        [ProtoMember(1)]
        public int TotalResultCount { get; set; }
        [ProtoMember(2)]
        public List<OrderDto> data { get; set; }
    }
}