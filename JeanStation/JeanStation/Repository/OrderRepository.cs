using JeanStation.Entities;
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

        public void OrderCreate(Order order)
        {
            if (order == null)
                throw new ArgumentNullException(nameof(order));

            _context.Orders.Add(order);
            _context.SaveChanges();
        }

        public void OrderUpdatePayment(Order order)
        {
            if (order == null)
                throw new ArgumentNullException(nameof(order));

            var existingOrder = _context.Orders.Find(order.OrderId);
            if (existingOrder != null)
            {
                existingOrder.PaymentStatus = order.PaymentStatus;
                _context.Entry(existingOrder).State = EntityState.Modified;
                _context.SaveChanges();
            }
            else
            {
                throw new InvalidOperationException("Order not found");
            }
        }

        public void OrderUpdateStatus(Order order)
        {
            if (order == null)
                throw new ArgumentNullException(nameof(order));

            var existingOrder = _context.Orders.Find(order.OrderId);
            if (existingOrder != null)
            {
                existingOrder.OrderStatus = order.OrderStatus;
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