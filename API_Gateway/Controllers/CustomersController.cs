using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using API_Gateway.HttpClients;
using CustomersDtoTypes.Helpers;
using Microsoft.AspNetCore.Mvc;
using CustomersDtoTypes.Models;
using WebApiHelpersTypes.Helpers;
using ProtoBuf;
using WebApiHelpers.ObjectResults;
using System.Diagnostics;
using CustomerDtoTypes.Helpers;

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
        public async Task<IActionResult> GetCustomers(CustomersPagingParameters customerParams)
        {
            Debug.Write(customerParams);
            var uri = Microsoft.AspNetCore.WebUtilities.QueryHelpers.AddQueryString(_client.BaseAddress.ToString(),
                    ObjectToDictionaryConverter.ConvertToDictionary(customerParams));

            var customerResponse = await _client.GetAsync(uri);

            if (customerResponse.IsSuccessStatusCode)
            {
                var customersStream = await customerResponse.Content.ReadAsStreamAsync();
                var customers = Serializer.Deserialize<CustomersPagedResult>(customersStream);
                if (customers != null)
                    return StatusCode((int)customerResponse.StatusCode, customers);
            }
            return StatusCode((int)customerResponse.StatusCode);
        }

        [HttpGet("{id}", Name = "GetCustomer")]
        public async Task<IActionResult> GetCustomer(int id)
        {
            UriBuilder uriBuilder = new UriBuilder(_client.BaseAddress);
            uriBuilder.Path +=  "/" + id;

            var customerResponse = await _client.GetAsync(uriBuilder.Uri);
            if (customerResponse.IsSuccessStatusCode)
            {
                var customer = Serializer.Deserialize<CustomerDto>(await customerResponse.Content.ReadAsStreamAsync());
                if (customer != null)
                    return StatusCode((int)customerResponse.StatusCode, customer);
            }
            return StatusCode((int)customerResponse.StatusCode);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCustomer([FromBody]CustomerToCreateDto customerToCreate)
        {
            if (customerToCreate == null) return BadRequest();

            //basically dont wanna have to worry about deserializing 2 possible types of response object, so just gonna do the validation here
            if (!ModelState.IsValid)
            {
                return new UnprocessableEntityObjectResult(ModelState);
            }

            MemoryStream customerProtoStream = new MemoryStream();
            Serializer.Serialize(customerProtoStream, customerToCreate);
            ByteArrayContent bArray = new ByteArrayContent(customerProtoStream.ToArray());

            var customerResponse = await _client.PostAsync(_client.BaseAddress, bArray);

            if (customerResponse.IsSuccessStatusCode)
            {
                var customersStream = await customerResponse.Content.ReadAsStreamAsync();
                var customer = Serializer.Deserialize<CustomersPagedResult>(customersStream);
                if (customer != null)
                    return StatusCode((int) customerResponse.StatusCode, customer);
            }
            return StatusCode((int)customerResponse.StatusCode);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCustomer(int id, [FromBody]CustomerToUpdateDto customerToUpdate)
        {
            if (customerToUpdate == null) return BadRequest();

            //basically dont wanna have to worry about deserializing 2 possible types of response object, so just gonna do the validation here
            if (!ModelState.IsValid)
            {
                return new UnprocessableEntityObjectResult(ModelState);
            }

            UriBuilder uriBuilder = new UriBuilder(_client.BaseAddress);
            uriBuilder.Path +=  "/" + id;

            MemoryStream customerProtoStream = new MemoryStream();
            Serializer.Serialize(customerProtoStream, customerToUpdate);
            ByteArrayContent bArray = new ByteArrayContent(customerProtoStream.ToArray());
            var customerResponse = await _client.PutAsync(uriBuilder.Uri, bArray);

            if (customerResponse.IsSuccessStatusCode)
            {
                var customersStream = await customerResponse.Content.ReadAsStreamAsync();
                var customer = Serializer.Deserialize<CustomerDto>(customersStream);
                if (customer != null)
                    return StatusCode((int)customerResponse.StatusCode, customer);
            }
            return StatusCode((int)customerResponse.StatusCode);
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            UriBuilder uriBuilder = new UriBuilder(_client.BaseAddress);
            uriBuilder.Path += "/" + id;
            var customerResponse = await _client.DeleteAsync(uriBuilder.Uri);
            
            return StatusCode((int)customerResponse.StatusCode);
        }
    }
}
