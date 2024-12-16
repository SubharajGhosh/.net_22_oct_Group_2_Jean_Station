using JeanStation.Entities;
using JeanStation.Models;
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
    [RoutePrefix("api/Order")]
    public class OrderController : ApiController
    {
        private readonly IOrderRepository _orderRepository;

        public OrderController()
        {
            _orderRepository = new OrderRepository();
        }
        [HttpPost]
        [Route("CreateOrder")]
        public IHttpActionResult CreateOrder([FromBody] Orderdto orderDto)
        {
            try
            {
                if (orderDto == null)
                {
                    return BadRequest("Order data cannot be null.");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                _orderRepository.CreateOrder(orderDto);
                return Ok("Order created successfully.");
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // Get Order by OrderId
        [HttpGet]
        [Route("GetOrderById/{orderId}")]
        public IHttpActionResult GetOrderById(string orderId)
        {
            try
            {
                var order = _orderRepository.GetOrderById(orderId);
                if (order == null)
                {
                    return NotFound();
                }
                return Ok(order);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // Update Order Status
        [HttpPut]
        [Route("UpdateOrderStatus")]
        public IHttpActionResult UpdateOrderStatus(string OrderId,string orderstatus)
        {
            try
            {
                if (OrderId == null)
                {
                    return BadRequest("Order data cannot be null.");
                }

                _orderRepository.OrderUpdateStatus(OrderId,orderstatus);
                return Ok("Order status updated successfully.");
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // Get Orders by CustomerId
        [HttpGet]
        [Route("GetOrdersByCustomerId/{customerId}")]
        public IHttpActionResult GetOrdersByCustomerId(string customerId)
        {
            try
            {
                var orders = _orderRepository.GetOrdersByCustomerId(customerId);
                if (!orders.Any())
                {
                    return NotFound();
                }
                return Ok(orders);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // Delete an Order
        [HttpDelete]
        [Route("DeleteOrder/{orderId}")]
        public IHttpActionResult DeleteOrder(string orderId)
        {
            try
            {
                _orderRepository.DeleteOrder(orderId);
                return Ok("Order deleted successfully.");
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}

