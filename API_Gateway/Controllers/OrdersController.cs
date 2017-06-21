using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using API_Gateway.HttpClients;
using Microsoft.AspNetCore.Mvc;
using OrdersDtoTypes.Models;
using OrdersDtoTypes.Helpers;
using WebApiHelpersTypes.Helpers;
using ProtoBuf;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API_Gateway.Controllers
{
    /// <summary>
    /// Controller for front end to interact with. Accepts JSON data from front end, converts to protobuf and sends to Orders Microservice
    /// </summary>
    [Route("api/[controller]")]
    public class OrdersController : Controller
    {
        private readonly OrdersHttpClient _client;

        public OrdersController(OrdersHttpClient client)
        {
            _client = client;
        }

        [HttpGet]
        public async Task<IActionResult> GetOrders(OrdersPagingParameters orderParams)
        {
            //UriBuilder uriBuilder = new UriBuilder(_client.BaseAddress);
            var uri = Microsoft.AspNetCore.WebUtilities.QueryHelpers.AddQueryString(_client.BaseAddress.ToString(),
                    ObjectToDictionaryConverter.ConvertToDictionary(orderParams));

            var orderResponse = await _client.GetAsync(uri);

            if (orderResponse.IsSuccessStatusCode)
            {
                var ordersStream = await orderResponse.Content.ReadAsStreamAsync();
                var orders = Serializer.DeserializeItems<OrderDto>(ordersStream, PrefixStyle.Base128, 1);
                if (orders != null)
                    return StatusCode((int)orderResponse.StatusCode, orders);
                else
                    return StatusCode((int)orderResponse.StatusCode);
            }
            else
                return StatusCode((int)orderResponse.StatusCode);
        }

        [HttpGet("{id}", Name = "GetOrder")]
        public async Task<IActionResult> GetOrder(int id)
        {
            UriBuilder uriBuilder = new UriBuilder(_client.BaseAddress);
            uriBuilder.Path +=  "/" + id;

            var orderResponse = await _client.GetAsync(uriBuilder.Uri);
            var order = Serializer.Deserialize<OrderDto>(await orderResponse.Content.ReadAsStreamAsync());
            if (order != null)
                return Ok(order);
            else
                return StatusCode((int)orderResponse.StatusCode);
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody]OrderToCreateDto order)
        {
            if (order == null) return BadRequest();
            MemoryStream orderProtoStream = new MemoryStream();
            Serializer.Serialize(orderProtoStream, order);
            ByteArrayContent bArray = new ByteArrayContent(orderProtoStream.ToArray());
            var orderResponse = await _client.PostAsync(_client.BaseAddress, bArray);
            if (orderResponse.IsSuccessStatusCode)
            {
                return Ok(orderResponse);
            }
            return StatusCode((int)orderResponse.StatusCode);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            UriBuilder uriBuilder = new UriBuilder(_client.BaseAddress);
            uriBuilder.Path += "/" + id;
            var orderResponse = await _client.DeleteAsync(uriBuilder.Uri);
            if (orderResponse.IsSuccessStatusCode)
            {
                return NoContent();
            }
            return StatusCode((int)orderResponse.StatusCode);
        }
    }
}
