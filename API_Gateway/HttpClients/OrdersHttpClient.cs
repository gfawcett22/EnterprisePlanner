using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace API_Gateway.HttpClients
{
    public class OrdersHttpClient : BaseMicroservicesHttpClient
    {
        public OrdersHttpClient()
        {
            this.BaseAddress = new Uri("http://localhost:5001/api/orders");
        }
    }
}
