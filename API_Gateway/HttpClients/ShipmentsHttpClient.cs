using System;

namespace API_Gateway.HttpClients
{
    public class ShipmentsHttpClient : BaseMicroservicesHttpClient
    {
        public ShipmentsHttpClient()
        {
            this.BaseAddress = new Uri("http://localhost:5003/api/shipments");
        }
    }
}