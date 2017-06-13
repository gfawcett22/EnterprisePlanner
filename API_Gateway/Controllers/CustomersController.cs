using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using API_Gateway.HttpClients;
using Microsoft.AspNetCore.Mvc;
using CustomersDtoTypes.Models;
using ProtoBuf;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API_Gateway.Controllers
{
    /// <summary>
    /// Controller for front end to interact with. Accepts JSON data from front end, converts to protobuf and sends to Customers Microservice
    /// </summary>
    [Route("api/[controller]")]
    public class CustomersController : Controller
    {
        private readonly CustomersHttpClient _client;

        public CustomersController(CustomersHttpClient client)
        {
            _client = client;
        }

        [HttpGet]
        public async Task<IActionResult> GetCustomers()
        {
            var customersResponse = await _client.GetAsync(_client.BaseAddress);
            var customers = Serializer.DeserializeItems<CustomerDto>(await customersResponse.Content.ReadAsStreamAsync(), PrefixStyle.None, -1);
            if (customers != null)
                return Ok(customers);
            else
                return StatusCode(500);
        }

        [HttpGet("{id}", Name ="GetCustomer")]
        public async Task<IActionResult> GetCustomer(int id)
        {
            Uri uri = new Uri(_client.BaseAddress, id.ToString());
            var customerResponse = await _client.GetAsync(uri);
            var customer = Serializer.Deserialize<CustomerDto>(await customerResponse.Content.ReadAsStreamAsync());
            if (customer != null)
                return Ok(customer);
            else
                return StatusCode(500);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCustomer([FromBody]CustomerToCreateDto customer)
        {
            //if (customer == null) return BadRequest();
            //Stream customerProtoStream = null;
            //Serializer.Serialize(customerProtoStream, customer);
            //var customerResponse = await _client.PostAsync(_client.BaseAddress, new HttpContent(customerProtoStream));
            return Ok();
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
