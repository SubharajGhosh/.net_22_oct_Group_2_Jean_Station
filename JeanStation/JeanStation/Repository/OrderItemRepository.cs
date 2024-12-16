using JeanStation.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace JeanStation.Repository
{
    public class OrderItemRepository : IOrderItemInterface
    {
        private readonly JeanStationContext _context;

        public OrderItemRepository()
        {
            _context = new JeanStationContext();
        }

        // Add an order item to the cart and calculate the total cart value
        public void Add(OrderItem orderItem)
        {
            var product = _context.Jeans.Find(orderItem.JeansId);
            if (product == null || product.Stock < orderItem.Quantity)
            {
                throw new InvalidOperationException("Insufficient stock for the selected product.");
            }

            // Deduct stock for the added quantity
            product.Stock -= orderItem.Quantity;

            // Add the order item
            _context.OrderItems.Add(orderItem);
            _context.SaveChanges();

            // Calculate the total cost of the cart for the customer
            decimal totalCartValue = CalculateCartTotal(orderItem.OrderId);
            Console.WriteLine($"Total cart value after adding item: {totalCartValue}");
        }

        // Retrieve all order items by Order ID
        public IEnumerable<OrderItem> GetByOrderId(string orderId)
        {
            return _context.OrderItems.Include(o => o.JeansId)
                                      .Where(o => o.OrderId == orderId)
                                      .ToList();
        }

        // Clear the cart for a specific customer
        public void ClearCart()
        {
            var cartItems = _context.OrderItems.Include(o => o.JeansId)
                                               .Where(o=>o.OrderId == null)
                                               .ToList();

            foreach (var item in cartItems)
            {
                if (item.JeanNavigation !=null)
                {
                    item.JeanNavigation.Stock += item.Quantity; // Restore stock
                }
            }

            _context.OrderItems.RemoveRange(cartItems);
            _context.SaveChanges();
        }

        // Update the quantity of an order item (both increasing and decreasing)
        public void UpdateQuantity(string orderItemId, int newQuantity)
        {
            var orderItem = _context.OrderItems.Include(o => o.JeansId).FirstOrDefault(o => o.OrderItemId == orderItemId);
            if (orderItem == null)
            {
                throw new InvalidOperationException("Order item not found.");
            }

            var product = orderItem.JeanNavigation;
            if (product == null)
            {
                throw new InvalidOperationException("Product not found.");
            }

            if (newQuantity > orderItem.Quantity)
            {
                // If the new quantity is greater, check if enough stock is available
                if (newQuantity - orderItem.Quantity > product.Stock)
                {
                    throw new InvalidOperationException("Insufficient stock for the updated quantity.");
                }
                product.Stock -= (newQuantity - orderItem.Quantity); // Deduct stock
            }
            else
            {
                // If the new quantity is less, restore stock
                product.Stock += (orderItem.Quantity - newQuantity);
            }

            // Update order item details
            orderItem.Quantity = newQuantity;
            orderItem.TotalPrice = newQuantity * product.Price; // Recalculate price

            _context.Entry(orderItem).State = EntityState.Modified;
            _context.SaveChanges();
        }

        // Remove a specific order item
        public void Remove(string orderItemId)
        {
            var orderItem = _context.OrderItems.Include(o => o.JeansId).FirstOrDefault(o => o.OrderItemId == orderItemId);
            if (orderItem == null)
            {
                throw new InvalidOperationException("Order item not found.");
            }

            var product = orderItem.JeanNavigation;
            if (product != null)
            {
                product.Stock += orderItem.Quantity; // Restore stock
            }

            _context.OrderItems.Remove(orderItem);
            _context.SaveChanges();
        }

        // Retrieve all order items for a specific customer
        //public IEnumerable<OrderItem> GetByCustomerId(string customerId)
        //{
        //    return _context.OrderItems.Include(o => o.JeansId)
        //                              .Where(o => o.CustomerId == customerId && o.OrderId == null)
        //                              .ToList();
        //}

        // Calculate the total cost of the cart for a specific customer
        public decimal CalculateCartTotal(string OrderId)
        {
            return _context.OrderItems.Where(o => o.OrderId == OrderId && o.OrderId == null)
                                      .Sum(o => o.Quantity * o.UnitPrice);
        }
    }
}
