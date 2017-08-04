using System;
using System.Collections.Generic;
using System.Text;
using WebApiHelpersTypes.Helpers;

namespace ShipmentsDtoTypes.Helpers
{
    public class ShipmentsPagingParameters : PagingParameters
    {
        //Search values for the orders
        public string Id { get; set; }
        public string CustomerId { get; set; }
        public string OrderId { get; set; }
        public string Status { get; set; }
    }
}