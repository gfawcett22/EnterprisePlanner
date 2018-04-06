using System.Net.Http;

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
            DefaultRequestHeaders.Accept.Clear();
            DefaultRequestHeaders.Accept.Add(
                new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/x-protobuf"));
        }
    }
}
