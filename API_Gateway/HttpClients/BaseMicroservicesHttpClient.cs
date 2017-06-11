using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace API_Gateway.HttpClients
{
    /// <summary>
    /// All microservices inherit from this class. Sets the headers to protobuf formats for communication.
    /// TODO: Add auth headers/tokens
    /// </summary>
    public class BaseMicroservicesHttpClient : HttpClient
    {
        public BaseMicroservicesHttpClient()
        {
            this.DefaultRequestHeaders.Accept.Clear();
            this.DefaultRequestHeaders.Accept.Add(
                new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/x-protobuf"));
        }
    }
}
