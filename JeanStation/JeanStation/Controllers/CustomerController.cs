using JeanStation.Entities;
using JeanStation.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace JeanStation.Controllers
{
    [RoutePrefix("api/Customer")]
    public class CustomerController : ApiController
    {
        private readonly ICustomerRepository _customerRepository;
        public CustomerController()
        {
            _customerRepository = new CustomerRepository();
        }
        [HttpGet]
        [Route("GetCustomerById/{UserId}")]
        public IHttpActionResult GetCustomerById(string UserId)
        {
            var customer = _customerRepository.GetCustomerByUserId(UserId);
            if (customer == null)
            {
                return NotFound(); // Return 404 if customer not found
            }

            return Ok(customer); // Return the customer details if found
        }

        // PUT api/customers/{customerId}
        [HttpPut]
        [Route("UpdateCustomer/{customerId}")]
        public IHttpActionResult UpdateCustomer(string customerId, [FromBody] Customer customer)
        {
            if (customer == null || customer.CustomerId != customerId)
            {
                return BadRequest("Customer data is invalid or mismatch in customerId");
            }

            try
            {
                var updatedCustomer = _customerRepository.UpdateCustomer(customer);
                return Ok(updatedCustomer); // Return the updated customer data
            }
            catch (InvalidOperationException)
            {
                return NotFound(); // Return 404 if the customer was not found
            }
        }

        // DELETE api/customers/{customerId}
        [HttpDelete]
        [Route("DeleteCustomer/{customerId}")]
        public IHttpActionResult DeleteCustomer(string customerId)
        {
            var success = _customerRepository.DeleteCustomer(customerId);
            if (!success)
            {
                return NotFound(); // Return 404 if the customer was not found
            }

            return Ok("Customer deleted successfully.");
        }


    }
}
