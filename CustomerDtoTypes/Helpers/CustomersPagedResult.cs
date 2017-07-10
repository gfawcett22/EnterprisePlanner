using System.Collections.Generic;
using CustomersDtoTypes.Models;
using ProtoBuf;

namespace CustomerDtoTypes.Helpers
{
    [ProtoContract(IgnoreListHandling=true)]
    public class CustomersPagedResult
    {
        [ProtoMember(1)]
        public int TotalResultCount { get; set; }
        [ProtoMember(2)]
        public List<CustomerDto> data { get; set; }
    }
}