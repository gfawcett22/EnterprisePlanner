using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Customers.Repositories;
using AutoMapper;
using Customers.Models;
using Customers.Entities;

namespace Customers.Controllers
{
    [Route("api/[controller]")]
    public class CustomersController : Controller
    {
        private readonly ICustomerRepository _repo;

        public CustomersController(ICustomerRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public IActionResult GetCustomers()
        {
            try
            {
                var customersFromRepo = _repo.GetCustomers().ToList();
                var customers = Mapper.Map<IEnumerable<CustomerDto>>(customersFromRepo);
                return Ok(customers);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpGet("{id}", Name ="GetCustomer")]
        public IActionResult GetCustomer([FromRoute]int id)
        {
            try
            {
                if (!_repo.CustomerExists(id)) { return NotFound(); }
                var customer = Mapper.Map<CustomerDto>(_repo.GetCustomer(id));
                return Ok(customer);
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult CreateCustomer([FromBody]CustomerToCreateDto customerToCreate)
        {
            try
            {
                if (customerToCreate == null) return BadRequest();
                var customerEntity = Mapper.Map<Customer>(customerToCreate);
                _repo.AddCustomer(customerEntity);
                if (!_repo.Save())
                {
                    throw new Exception("Error creating customer.");
                }
                var customerToReturn = Mapper.Map<CustomerDto>(customerEntity);
                return CreatedAtRoute("GetCustomer", new { id = customerToReturn.Id }, customerToReturn);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdateCustomer(int id, [FromBody]CustomerToUpdateDto customerToUpdate)
        {
            if (customerToUpdate == null) return BadRequest();

            var customerFromRepo = _repo.GetCustomer(id);
            if (customerFromRepo == null) return NotFound();
            try
            {
                Mapper.Map(customerToUpdate, customerFromRepo);
                _repo.UpdateCustomer(customerFromRepo);
                _repo.Save();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteCustomer(int id)
        {
            var customerFromRepo = _repo.GetCustomer(id);
            if (customerFromRepo == null) return NotFound();
            _repo.DeleteCustomer(customerFromRepo);
            if (!_repo.Save())
            {
                throw new Exception($"Error deleting customer {id}.");
            }

            return NoContent();
        }
    }
}
