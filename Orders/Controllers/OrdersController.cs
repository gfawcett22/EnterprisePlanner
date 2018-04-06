using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EnterprisePlanner.Messaging.Models.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Orders.Entities;
using Orders.IntegrationEvents;
using Orders.Repositories;
using OrdersDtoTypes.Helpers;
using OrdersDtoTypes.Models;
using WebApiHelpers.ObjectResults;

namespace Orders.Controllers
{
    [Route("api/[controller]")]
    public class OrdersController : Controller
    {
        private readonly IOrderRepository _repo;
        private readonly IEventBus _eventBus;
        public OrdersController(IOrderRepository repo, IEventBus eventBus)
        {
            _repo = repo;
            _eventBus = eventBus;
        }

        [HttpGet]
        public IActionResult GetOrders(OrdersPagingParameters ordersParams)
        {
            try
            {
                var ordersFromRepo = _repo.GetOrders(ordersParams).ToList();
                var orders = Mapper.Map<IEnumerable<OrderDto>>(ordersFromRepo);
                OrdersPagedResult result = new OrdersPagedResult {
                    TotalResultCount = _repo.GetOrdersCount(),
                    data = orders.ToList()
                };
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpGet("{id}", Name = "GetOrder")]
        public IActionResult GetOrder([FromRoute]int id)
        {
            try
            {
                if (!_repo.OrderExists(id)) { return NotFound(); }
                var order = Mapper.Map<OrderDto>(_repo.GetOrder(id));
                return Ok(order);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult CreateOrder([FromBody]OrderToCreateDto orderToCreate)
        {
            try
            {
                if (orderToCreate == null) return BadRequest();

                if (!ModelState.IsValid)
                {
                    return new UnprocessableEntityObjectResult(ModelState);
                }

                var orderEntity = Mapper.Map<Order>(orderToCreate);
                _repo.AddOrder(orderEntity);
                if (!_repo.Save())
                {
                    throw new Exception("Error creating order.");
                }

                var eventMessage = new OrderCreatedIntegrationEvent(orderEntity);
                _eventBus.Publish(eventMessage);

                var orderToReturn = Mapper.Map<OrderDto>(orderEntity);
                return CreatedAtRoute("GetOrder", new { id = orderToReturn.Id }, orderToReturn);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdateOrder(int id, [FromBody]OrderToUpdateDto orderToUpdate)
        {
            if (orderToUpdate == null) return BadRequest();

            if (!ModelState.IsValid)
            {
                return new UnprocessableEntityObjectResult(ModelState);
            }

            var orderFromRepo = _repo.GetOrder(id);
            if (orderFromRepo == null) return NotFound();
            try
            {
                Mapper.Map(orderToUpdate, orderFromRepo);
                _repo.UpdateOrder(orderFromRepo);
                _repo.Save();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteOrder(int id)
        {
            var orderFromRepo = _repo.GetOrder(id);
            if (orderFromRepo == null) return NotFound();
            _repo.DeleteOrder(orderFromRepo);
            if (!_repo.Save())
            {
                throw new Exception($"Error deleting order {id}.");
            }

            return NoContent();
        }
    }
}
