using JeanStation.Entities;
using JeanStation.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace JeanStation.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly JeanStationContext _context;

        public OrderRepository()
        {
            _context = new JeanStationContext();
        }
        public void CreateOrder(Orderdto orderDto)
        {
            if (orderDto == null)
                throw new ArgumentNullException(nameof(orderDto), "Order DTO cannot be null.");

            // Map DTO to Order entity
            var order = new Order
            {
                OrderId = string.IsNullOrEmpty(orderDto.OrderId) ? Guid.NewGuid().ToString() : orderDto.OrderId,
                CustomerId = orderDto.CustomerId,
                Amount = orderDto.Amount,
                OrderStatus = orderDto.OrderStatus ?? "Pending", // Default to "Pending" if not provided
                PaymentStatus = orderDto.PaymentStatus ?? "Unpaid", // Default to "Unpaid" if not provided
                OrderDate = orderDto.OrderDate == default ? DateTime.Now : orderDto.OrderDate
            };

            _context.Orders.Add(order);
            _context.SaveChanges();
        }

        
    
        public void DeleteOrder(string OrderId)
        {
            if (OrderId == null)
                throw new ArgumentNullException(nameof(OrderId));

            var existingOrder = _context.Orders.Find(OrderId);
            if (existingOrder != null)
            {
                _context.Orders.Remove(existingOrder);
                _context.SaveChanges();
            }
            else
            {
                throw new InvalidOperationException("Order not found");
            }
        }

        public Order GetOrderById(string OrderId)
        {
            if (string.IsNullOrEmpty(OrderId))
                throw new ArgumentException("OrderId cannot be null or empty", nameof(OrderId));

            return _context.Orders.Include(o => o.CustomerNavigation).FirstOrDefault(o => o.OrderId == OrderId);
        }

        
        

        public void OrderUpdateStatus(string OrderId,string orderstatus)
        {
            if (orderstatus == null)
                throw new ArgumentNullException(nameof(orderstatus));

            var existingOrder = _context.Orders.Find(OrderId);
            if (existingOrder != null)
            {
                existingOrder.OrderStatus = orderstatus;
                _context.Entry(existingOrder).State = EntityState.Modified;
                _context.SaveChanges();
            }
            else
            {
                throw new InvalidOperationException("Order not found");
            }
        }
        public List<Order> GetOrdersByCustomerId(string customerId)
        {
            if (string.IsNullOrEmpty(customerId))
                throw new ArgumentException("CustomerId cannot be null or empty", nameof(customerId));

            return _context.Orders.Include(o => o.CustomerNavigation)
                                  .Where(o => o.CustomerId == customerId)
                                  .ToList();
        }

    }
}