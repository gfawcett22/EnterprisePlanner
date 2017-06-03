using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Customers.Models
{
    public class CustomerToCreateDto
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string Business { get; set; }
    }
}
