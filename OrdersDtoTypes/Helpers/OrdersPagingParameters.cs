using System;
using System.Collections.Generic;
using System.Text;
using WebApiHelpersTypes.Helpers;

namespace OrdersDtoTypes.Helpers
{
    public class OrdersPagingParameters : PagingParameters
    {
        //Search values for the orders
        public int Id { get; set; }
        public virtual DateTime DatePlaced { get; set; }
        public virtual int CustomerId { get; set; }
    }
}
