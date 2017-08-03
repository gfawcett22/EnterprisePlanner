using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using API_Gateway.HttpClients;
using OrdersDtoTypes.Helpers;
using Microsoft.AspNetCore.Mvc;
using OrdersDtoTypes.Models;
using WebApiHelpersTypes.Helpers;
using ProtoBuf;
using WebApiHelpers.ObjectResults;

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
            var uri = Microsoft.AspNetCore.WebUtilities.QueryHelpers.AddQueryString(_client.BaseAddress.ToString(),
                    ObjectToDictionaryConverter.ConvertToDictionary(orderParams));

            var orderResponse = await _client.GetAsync(uri);

            if (orderResponse.IsSuccessStatusCode)
            {
                var ordersStream = await orderResponse.Content.ReadAsStreamAsync();
                var orders = Serializer.Deserialize<OrdersPagedResult>(ordersStream);
                if (orders != null)
                    return StatusCode((int)orderResponse.StatusCode, orders);
            }
            return StatusCode((int)orderResponse.StatusCode);
        }

        [HttpGet("{id}", Name = "GetOrder")]
        public async Task<IActionResult> GetOrder(int id)
        {
            UriBuilder uriBuilder = new UriBuilder(_client.BaseAddress);
            uriBuilder.Path +=  "/" + id;

            var orderResponse = await _client.GetAsync(uriBuilder.Uri);
            if (orderResponse.IsSuccessStatusCode)
            {
                var order = Serializer.Deserialize<OrderDto>(await orderResponse.Content.ReadAsStreamAsync());
                if (order != null)
                    return StatusCode((int)orderResponse.StatusCode, order);
            }
            return StatusCode((int)orderResponse.StatusCode);
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody]OrderToCreateDto orderToCreate)
        {
            if (orderToCreate == null) return BadRequest();

            //basically dont wanna have to worry about deserializing 2 possible types of response object, so just gonna do the validation here
            if (!ModelState.IsValid)
            {
                return new UnprocessableEntityObjectResult(ModelState);
            }

            MemoryStream orderProtoStream = new MemoryStream();
            Serializer.Serialize(orderProtoStream, orderToCreate);
            ByteArrayContent bArray = new ByteArrayContent(orderProtoStream.ToArray());

            var orderResponse = await _client.PostAsync(_client.BaseAddress, bArray);

            if (orderResponse.IsSuccessStatusCode)
            {
                var ordersStream = await orderResponse.Content.ReadAsStreamAsync();
                var order = Serializer.Deserialize<OrderDto>(ordersStream);
                if (order != null)
                    return StatusCode((int) orderResponse.StatusCode, order);
            }
            return StatusCode((int)orderResponse.StatusCode);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOrder(int id, [FromBody]OrderToUpdateDto orderToUpdate)
        {
            if (orderToUpdate == null) return BadRequest();

            //basically dont wanna have to worry about deserializing 2 possible types of response object, so just gonna do the validation here
            if (!ModelState.IsValid)
            {
                return new UnprocessableEntityObjectResult(ModelState);
            }

            MemoryStream orderProtoStream = new MemoryStream();
            Serializer.Serialize(orderProtoStream, orderToUpdate);
            ByteArrayContent bArray = new ByteArrayContent(orderProtoStream.ToArray());

            UriBuilder uriBuilder = new UriBuilder(_client.BaseAddress);
            uriBuilder.Path +=  "/" + id;

            var orderResponse = await _client.PutAsync(uriBuilder.Uri, bArray);

            return StatusCode((int)orderResponse.StatusCode);
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            UriBuilder uriBuilder = new UriBuilder(_client.BaseAddress);
            uriBuilder.Path += "/" + id;
            var orderResponse = await _client.DeleteAsync(uriBuilder.Uri);
            
            return StatusCode((int)orderResponse.StatusCode);
        }
    }
}
