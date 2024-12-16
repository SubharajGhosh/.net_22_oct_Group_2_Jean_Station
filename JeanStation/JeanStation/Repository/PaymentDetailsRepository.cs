using JeanStation.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace JeanStation.Repository
{
    public class PaymentDetailsRepository : IPaymentDetailsRepository
    {
        JeanStationContext _context;
        public PaymentDetailsRepository()
        {
            _context = new JeanStationContext();
        }
        public PaymentDetails ProcessPayment(string orderId, string paymentMode)
        {
            if (string.IsNullOrEmpty(orderId))
                throw new ArgumentException("Order ID cannot be null or empty", nameof(orderId));

            if (paymentMode != "Cash" && paymentMode != "Credit Card")
                throw new ArgumentException("Invalid payment mode", nameof(paymentMode));

            // Retrieve the order items associated with the orderId
            var orderItems = _context.OrderItems.Include(o => o.JeansId)
                                                .Where(o => o.OrderId == null) // New order items with no order association yet
                                                .ToList();

            if (!orderItems.Any())
                throw new InvalidOperationException("No items to associate with the payment.");

            // Calculate total price from the order items
            double totalPrice = orderItems.Sum(item => item.Quantity * item.UnitPrice);

            // Create the new order
            var order = new Order
            {
                OrderId = Guid.NewGuid().ToString(),
                OrderDate = DateTime.Now,
                Amount = totalPrice,
                OrderStatus = "Pending", // Default status
                PaymentStatus = "Unpaid" // Initially, payment is not done
            };

            // Add the new order to the database
            _context.Orders.Add(order);
            _context.SaveChanges();

            // Now associate the orderId with the order items
            foreach (var orderItem in orderItems)
            {
                orderItem.OrderId = order.OrderId; // Map the orderId to the order items
                _context.Entry(orderItem).State = EntityState.Modified;
            }
            _context.SaveChanges();

            // Create payment details and associate it with the new order
            var paymentDetails = new PaymentDetails
            {
                PaymentId = Guid.NewGuid().ToString(),
                OrderId = order.OrderId,
                PaymentMode = paymentMode,
                PaymentStatus = "Completed", // Mark as completed upon payment
                PaymentDate = DateTime.Now,
                OrderNavigation = order
            };

            // Update order payment status to "Paid"
            order.PaymentStatus = "Paid";
            _context.Entry(order).State = EntityState.Modified;

            // Add payment details to the database
            _context.PaymentDetailss.Add(paymentDetails);
            _context.SaveChanges();

            return paymentDetails;
        }
        public IEnumerable<PaymentDetails> GetPaymentByOrderId(string orderId)
        {
            return _context.PaymentDetailss.Include(p => p.OrderNavigation)
                                           .Where(p => p.OrderId == orderId)
                                           .ToList();
        }
    }
}