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

            // Retrieve the existing order using the orderId
            var order = _context.Orders.SingleOrDefault(o => o.OrderId == orderId);
            if (order == null)
                throw new InvalidOperationException("Order not found.");

            // Retrieve the order items associated with the orderId
            var orderItems = _context.OrderItems.Where(o => o.OrderId == orderId).ToList();

            if (!orderItems.Any())
                throw new InvalidOperationException("No items associated with the order.");

            // Calculate the total price from the order items
            double totalPrice = orderItems.Sum(item => item.Quantity * item.UnitPrice);

            // Update the order details
            order.Amount = totalPrice;
            order.OrderStatus = "Pending"; // Set order status
            order.PaymentStatus = "Unpaid"; // Initially unpaid
            order.OrderDate = DateTime.Now; // Update the order date if required

            // Update the order in the database
            _context.Entry(order).State = EntityState.Modified;

            // Save changes to the order and associated items
            _context.SaveChanges();

            // Create payment details and associate it with the existing order
            var paymentDetails = new PaymentDetails
            {
                PaymentId = Guid.NewGuid().ToString(),
                OrderId = order.OrderId,
                PaymentMode = paymentMode,
                PaymentStatus = "Completed", // Mark as completed upon payment
                PaymentDate = DateTime.Now,
                OrderNavigation = order
            };

            // Update the order payment status to "Paid"
            order.PaymentStatus = "Paid";
            _context.Entry(order).State = EntityState.Modified;

            // Add payment details to the database
            _context.PaymentDetailss.Add(paymentDetails);

            // Save all changes to the database
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