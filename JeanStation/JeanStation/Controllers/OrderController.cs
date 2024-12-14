using JeanStation.Entities;
using JeanStation.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Controllers;
namespace JeanStation.Controllers
{
    [RoutePrefix("api/orders")]
    public class OrderController : ApiController
    {
        private IOrderRepository _orderRepository;

        public OrderController()
        {
            _orderRepository = new OrderRepository();
        }

        // GET: api/orders/{id}
        [HttpGet]
        [Route("{id}")]
        public IHttpActionResult GetOrderById(string id)
        {
            if (string.IsNullOrEmpty(id))
                return BadRequest("Order ID cannot be null or empty.");

            var order = _orderRepository.GetOrderById(id);
            if (order == null)
                return NotFound();

            return Ok(order);
        }

        // GET: api/orders/customer/{customerId}
        [HttpGet]
        [Route("customer/{customerId}")]
        public IHttpActionResult GetOrdersByCustomerId(string customerId)
        {
            if (string.IsNullOrEmpty(customerId))
                return BadRequest("Customer ID cannot be null or empty.");

            var orders = _orderRepository.GetOrdersByCustomerId(customerId);
            return Ok(orders);
        }

        // POST: api/orders
        [HttpPost]
        [Route("Create")]
        public IHttpActionResult CreateOrder([FromBody] Order order)
        {
            if (order == null)
                return BadRequest("Order data is null");

            try
            {
                _orderRepository.OrderCreate(order);
                return Ok("Created");
                //return StatusCode(HttpStatusCode.Created);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // PUT: api/orders/payment
        [HttpPut]
        [Route("payment")]
        public IHttpActionResult UpdateOrderPayment([FromBody] Order order)
        {
            if (order == null)
                return BadRequest("Order data is null.");

            try
            {
                _orderRepository.OrderUpdatePayment(order);
                return Ok();
            }
            catch (InvalidOperationException)
            {
                return NotFound();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // PUT: api/orders/status
        [HttpPut]
        [Route("status")]
        public IHttpActionResult UpdateOrderStatus([FromBody] Order order)
        {
            if (order == null)
                return BadRequest("Order data is null.");

            try
            {
                _orderRepository.OrderUpdateStatus(order);
                return Ok();
            }
            catch (InvalidOperationException)
            {
                return NotFound();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // DELETE: api/orders
        [HttpDelete]
        [Route("Delete/{OrderId}")]
        public IHttpActionResult DeleteOrder(string OrderId)
        {
            if (OrderId == null)
                return BadRequest("Order data is null.");

            try
            {
                _orderRepository.DeleteOrder(OrderId);
                return Ok("Deleted");
            }
            catch (InvalidOperationException)
            {
                return NotFound();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}

