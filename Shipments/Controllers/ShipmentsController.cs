using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Shipments.Entities;
using Shipments.Repositories;
using ShipmentsDtoTypes.Helpers;
using ShipmentsDtoTypes.Models;
using WebApiHelpers.ObjectResults;

namespace Shipments.Controllers
{
    [Route("api/[controller]")]
    public class ShipmentsController : Controller
    {
        private readonly IShipmentRepository _repo;

        public ShipmentsController(IShipmentRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public IActionResult GetShipments(ShipmentsPagingParameters shipmentsParams)
        {
            try
            {
                var shipmentsFromRepo = _repo.GetShipments(shipmentsParams).ToList();
                var shipments = Mapper.Map<IEnumerable<ShipmentDto>>(shipmentsFromRepo);
                ShipmentsPagedResult result = new ShipmentsPagedResult {
                    TotalResultCount = _repo.GetShipmentsCount(),
                    data = shipments.ToList()
                };
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpGet("{id}", Name = "GetShipment")]
        public IActionResult GetShipment([FromRoute]int id)
        {
            try
            {
                if (!_repo.ShipmentExists(id)) { return NotFound(); }
                var shipment = Mapper.Map<ShipmentDto>(_repo.GetShipment(id));
                return Ok(shipment);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult CreateShipment([FromBody]ShipmentToCreateDto shipmentToCreate)
        {
            try
            {
                if (shipmentToCreate == null) return BadRequest();

                if (!ModelState.IsValid)
                {
                    return new UnprocessableEntityObjectResult(ModelState);
                }

                var shipmentEntity = Mapper.Map<Shipment>(shipmentToCreate);
                _repo.AddShipment(shipmentEntity);
                if (!_repo.Save())
                {
                    throw new Exception("Error creating shipment.");
                }
                var shipmentToReturn = Mapper.Map<ShipmentDto>(shipmentEntity);
                return CreatedAtRoute("GetShipment", new { id = shipmentToReturn.Id }, shipmentToReturn);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

//        [HttpPut("{id}")]
//        public IActionResult UpdateShipment(int id, [FromBody]ShipmentToUpdateDto shipmentToUpdate)
//        {
//            if (shipmentToUpdate == null) return BadRequest();
//
//            if (!ModelState.IsValid)
//            {
//                return new UnprocessableEntityObjectResult(ModelState);
//            }
//
//            var shipmentFromRepo = _repo.GetShipment(id);
//            if (shipmentFromRepo == null) return NotFound();
//            try
//            {
//                Mapper.Map(shipmentToUpdate, shipmentFromRepo);
//                _repo.UpdateShipment(shipmentFromRepo);
//                _repo.Save();
//            }
//            catch (Exception ex)
//            {
//                throw new Exception(ex.Message);
//            }
//            return NoContent();
//        }

        [HttpDelete("{id}")]
        public IActionResult DeleteShipment(int id)
        {
            var shipmentFromRepo = _repo.GetShipment(id);
            if (shipmentFromRepo == null) return NotFound();
            _repo.DeleteShipment(shipmentFromRepo);
            if (!_repo.Save())
            {
                throw new Exception($"Error deleting shipment {id}.");
            }

            return NoContent();
        }
    }
}
