using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using API_Gateway.HttpClients;
using ShipmentsDtoTypes.Helpers;
using Microsoft.AspNetCore.Mvc;
using ShipmentsDtoTypes.Models;
using WebApiHelpersTypes.Helpers;
using ProtoBuf;
using WebApiHelpers.ObjectResults;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API_Gateway.Controllers
{
    /// <summary>
    /// Controller for front end to interact with. Accepts JSON data from front end, converts to protobuf and sends to Shipments Microservice
    /// </summary>
    [Route("api/[controller]")]
    public class ShipmentsController : Controller
    {
        private readonly ShipmentsHttpClient _client;

        public ShipmentsController(ShipmentsHttpClient client)
        {
            _client = client;
        }

        [HttpGet]
        public async Task<IActionResult> GetShipments(ShipmentsPagingParameters shipmentParams)
        {
            var uri = Microsoft.AspNetCore.WebUtilities.QueryHelpers.AddQueryString(_client.BaseAddress.ToString(),
                    ObjectToDictionaryConverter.ConvertToDictionary(shipmentParams));

            var shipmentResponse = await _client.GetAsync(uri);

            if (shipmentResponse.IsSuccessStatusCode)
            {
                var shipmentsStream = await shipmentResponse.Content.ReadAsStreamAsync();
                var shipments = Serializer.Deserialize<ShipmentsPagedResult>(shipmentsStream);
                if (shipments != null)
                    return StatusCode((int)shipmentResponse.StatusCode, shipments);
            }
            return StatusCode((int)shipmentResponse.StatusCode);
        }

        [HttpGet("{id}", Name = "GetShipment")]
        public async Task<IActionResult> GetShipment(int id)
        {
            UriBuilder uriBuilder = new UriBuilder(_client.BaseAddress);
            uriBuilder.Path +=  "/" + id;

            var shipmentResponse = await _client.GetAsync(uriBuilder.Uri);
            if (shipmentResponse.IsSuccessStatusCode)
            {
                var shipment = Serializer.Deserialize<ShipmentDto>(await shipmentResponse.Content.ReadAsStreamAsync());
                if (shipment != null)
                    return StatusCode((int)shipmentResponse.StatusCode, shipment);
            }
            return StatusCode((int)shipmentResponse.StatusCode);
        }

        [HttpPost]
        public async Task<IActionResult> CreateShipment([FromBody]ShipmentToCreateDto shipmentToCreate)
        {
            if (shipmentToCreate == null) return BadRequest();

            //basically dont wanna have to worry about deserializing 2 possible types of response object, so just gonna do the validation here
            if (!ModelState.IsValid)
            {
                return new UnprocessableEntityObjectResult(ModelState);
            }

            MemoryStream shipmentProtoStream = new MemoryStream();
            Serializer.Serialize(shipmentProtoStream, shipmentToCreate);
            ByteArrayContent bArray = new ByteArrayContent(shipmentProtoStream.ToArray());

            var shipmentResponse = await _client.PostAsync(_client.BaseAddress, bArray);

            if (shipmentResponse.IsSuccessStatusCode)
            {
                var shipmentsStream = await shipmentResponse.Content.ReadAsStreamAsync();
                var shipment = Serializer.Deserialize<ShipmentDto>(shipmentsStream);
                if (shipment != null)
                    return StatusCode((int) shipmentResponse.StatusCode, shipment);
            }
            return StatusCode((int)shipmentResponse.StatusCode);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public IActionResult UpdateShipment(int id, [FromBody]ShipmentToUpdateDto shipmentToUpdate)
        {
            return NotFound();
//            if (shipmentToUpdate == null) return BadRequest();
//
//            //basically dont wanna have to worry about deserializing 2 possible types of response object, so just gonna do the validation here
//            if (!ModelState.IsValid)
//            {
//                return new UnprocessableEntityObjectResult(ModelState);
//            }
//
//            UriBuilder uriBuilder = new UriBuilder(_client.BaseAddress);
//            uriBuilder.Path +=  "/" + id;
//
//            MemoryStream shipmentProtoStream = new MemoryStream();
//            Serializer.Serialize(shipmentProtoStream, shipmentToUpdate);
//            ByteArrayContent bArray = new ByteArrayContent(shipmentProtoStream.ToArray());
//            
//            var shipmentResponse = await _client.PutAsync(uriBuilder.Uri, bArray);
//            
//            return StatusCode((int)shipmentResponse.StatusCode);
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            UriBuilder uriBuilder = new UriBuilder(_client.BaseAddress);
            uriBuilder.Path += "/" + id;
            var shipmentResponse = await _client.DeleteAsync(uriBuilder.Uri);
            
            return StatusCode((int)shipmentResponse.StatusCode);
        }
    }
}
