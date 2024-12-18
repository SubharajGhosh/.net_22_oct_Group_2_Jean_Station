using JeanStation.Entities;
using JeanStation.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using JeanStation.Models;


namespace JeanStation.Controllers
{
    [RoutePrefix("api/OrderItem")]
    public class OrderItemController : ApiController
    {
        private readonly IOrderItemInterface _orderItemRepository;
        private readonly IJeansRepository _jeansRepository;

        public OrderItemController()
        {
            _orderItemRepository = new OrderItemRepository();
            _jeansRepository = new JeansRepository();
        }

        // Add a new OrderItem for a Customer
        [HttpPost]
        [Route("AddOrderItem")]
        public IHttpActionResult AddItemToOrder(OrderItemdto orderItem)
        {
            try
            {
                var jeans = _jeansRepository.GetById(orderItem.JeansId);
                if (jeans == null || jeans.Stock < orderItem.Quantity)
                {
                    return BadRequest("Insufficient stock or invalid product.");
                }
                Random random = new Random();
                string orderItemId = $"ORID-{DateTime.Now:yyyyMMdd}-{random.Next(1000, 9999)}";
                orderItem.OrderItemId = orderItemId;
                orderItem.TotalPrice = jeans.Price * orderItem.Quantity;
                _orderItemRepository.Add(orderItem);
                return Ok("Order item added successfully.");
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // Update the quantity of an existing OrderItem
        //[HttpPut]
        //[Route("UpdateQuantity")]
        //public IHttpActionResult UpdateQuantity(string orderItemId, int newQuantity)
        //{
        //    try
        //    {
        //        var orderItem = _orderItemRepository.GetById(orderItemId);
        //        if (orderItem == null)
        //        {
        //            return NotFound();
        //        }

        //        var jeans = _jeansRepository.GetById(orderItem.JeansId);
        //        if (jeans == null)
        //        {
        //            return BadRequest("Invalid product.");
        //        }

        //        if ((newQuantity - orderItem.Quantity) > jeans.Stock)
        //        {
        //            return BadRequest("Insufficient stock for the updated quantity.");
        //        }

        //        _orderItemRepository.UpdateQuantity(orderItemId, newQuantity);
        //        return Ok("Order item quantity updated successfully.");
        //    }
        //    catch (Exception ex)
        //    {
        //        return InternalServerError(ex);
        //    }
        //}

        // Get all OrderItems by OrderId
        [HttpGet]
        [Route("GetOrderItemsByOrderId/{orderId}")]
        public IHttpActionResult GetOrderItemsByOrderId(string orderId)
        {
            try
            {
                var orderItems = _orderItemRepository.GetByOrderId(orderId);
                if (!orderItems.Any())
                {
                    return NotFound();
                }
                return Ok(orderItems);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // Clear all OrderItems with null OrderId
        //[HttpDelete]
        //[Route("ClearNullOrderItems")]
        //public IHttpActionResult ClearNullOrderItems(string OrderId)
        //{
        //    try
        //    {
        //        _orderItemRepository.ClearCart(OrderId);
        //        return Ok("All order items with null OrderId have been cleared.");
        //    }
        //    catch (Exception ex)
        //    {
        //        return InternalServerError(ex);
        //    }
        //}

        // Remove an OrderItem
        [HttpDelete]
        [Route("RemoveOrderItem/{orderItemId}")]
        public IHttpActionResult RemoveOrderItem(string orderItemId)
        {
            try
            {
                _orderItemRepository.Remove(orderItemId);
                //if (!success)
                //{
                //    return NotFound();
                //}
                return Ok("Order item removed successfully.");
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}

