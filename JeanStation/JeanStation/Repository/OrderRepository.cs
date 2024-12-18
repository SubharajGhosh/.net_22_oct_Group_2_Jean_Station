using JeanStation.Entities;
using JeanStation.Migrations;
using JeanStation.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Remoting.Contexts;
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
            var customer = _context.Customers.Find(orderDto.CustomerId);
            // Map DTO to Order entity
            var order = new Order
            {
                OrderId = string.IsNullOrEmpty(orderDto.OrderId) ? Guid.NewGuid().ToString() : orderDto.OrderId,
                CustomerId = orderDto.CustomerId,
                Amount = orderDto.Amount,
                OrderStatus = orderDto.OrderStatus ?? "Pending", // Default to "Pending" if not provided
                PaymentStatus = orderDto.PaymentStatus ?? "Unpaid", // Default to "Unpaid" if not provided
                OrderDate = orderDto.OrderDate == default ? DateTime.Now : orderDto.OrderDate,
                
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

        public Orderdto GetOrderById(string orderId)
        {
            if (string.IsNullOrEmpty(orderId))
                throw new ArgumentException("OrderId cannot be null or empty", nameof(orderId));

            // Fetch the order and map it to OrderDTO
            var order = _context.Orders
                                .Include(o => o.CustomerNavigation) // Include related CustomerNavigation if necessary
                                .Where(o => o.OrderId == orderId)
                                .Select(o => new Orderdto
                                {
                                    OrderId = o.OrderId,
                                    CustomerId = o.CustomerId,
                                    OrderDate = o.OrderDate,
                                    Amount = o.Amount,
                                    OrderStatus = o.OrderStatus,
                                    PaymentStatus = o.PaymentStatus,
                                    City = o.City,
                                    Address = o.Address,
                                    // You can add more properties or map them from related entities
                                })
                                .FirstOrDefault(); // Returns the first matched order or null

            return order;
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
        public List<Orderdto> GetOrdersByCustomerId(string customerId)
        {
            if (string.IsNullOrEmpty(customerId))
                throw new ArgumentException("CustomerId cannot be null or empty", nameof(customerId));

            // Fetch orders and map them to OrderDTO
            var orders = _context.Orders
                                 .Include(o => o.CustomerNavigation) // Include related CustomerNavigation if needed
                                 .Where(o => o.CustomerId == customerId)
                                 .Select(o => new Orderdto
                                 {
                                     OrderId = o.OrderId,
                                     CustomerId = o.CustomerId,
                                     OrderDate = o.OrderDate,
                                     Amount = o.Amount, // Adjust if needed based on your Order entity
                                     OrderStatus = o.OrderStatus,
                                     PaymentStatus = o.PaymentStatus,
                                     City = o.City,
                                     Address = o.Address
                                     
                                 })
                                 .ToList();

            return orders;
        }


    }
}