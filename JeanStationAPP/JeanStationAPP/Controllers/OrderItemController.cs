using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using JeanStationAPP.Models;
using System.Text;

namespace JeanStationAPP.Controllers
{
    public class OrderItemController : Controller
    {
        private readonly string apiBaseUrl = "http://localhost:61124/api/OrderItem/";
        private readonly string orderApiBaseUrl = "http://localhost:61124/api/Order/";

        public ActionResult Index(string orderId)
        {
            string customerId = Session["CustomerId"] as string;
            if (string.IsNullOrEmpty(customerId))
            {
                return RedirectToAction("Login", "Account"); // Redirect to login if no customerId in session
            }

            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = client.GetAsync(apiBaseUrl + $"GetOrderItemsByOrderId/{orderId}").Result;
                if (response.IsSuccessStatusCode)
                {
                    var data = response.Content.ReadAsStringAsync();
                    var orderItems = JsonConvert.DeserializeObject<List<OrderItem>>(data.Result);
                    return View(orderItems);
                }
                return View("Error");
            }
        }

        
        public ActionResult AddOrderItems()
        {
            string customerId = Session["CustomerId"] as string;
            if (string.IsNullOrEmpty(customerId))
            {
                return RedirectToAction("Login", "Account"); // Redirect to login if no customerId in session
            }

            if (TempData["CartItems"] is List<Cart> cartItems && cartItems.Any())
            {
                // Create the Order first
                Random random = new Random();
                string orderId = $"OID-{DateTime.Now:yyyyMMdd}-{random.Next(1000, 9999)}";
                var orderDto = new Order
                {
                    OrderId = orderId, // Generate a unique OrderId
                    CustomerId = customerId,
                    OrderDate = DateTime.Now,
                    Amount = cartItems.Sum(item => item.Price * item.Quantity), // Amount corresponds to the total of the cart items
                    OrderStatus = "Pending", // Set default status
                    PaymentStatus = "Unpaid", // Set default payment status
                    City = "", // You may want to fetch this from the user's profile or address input
                    Address = "" // Similarly, you can set this dynamically
                };

                using (HttpClient client = new HttpClient())
                {
                    // Create the order
                    var requestBody = orderDto;
                    string jsonContent = JsonConvert.SerializeObject(requestBody);
                    HttpContent content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
                    HttpResponseMessage orderResponse = client.PostAsync(orderApiBaseUrl + "CreateOrder", content).Result;
                    if (!orderResponse.IsSuccessStatusCode) return View("Error");

                    // Create Order Items for the Order
                    bool flag = false;
                    foreach (var cartItem in cartItems)
                    {
                        var orderItem = new OrderItem
                        {
                            OrderItemId = $"OIID-{DateTime.Now:yyyyMMdd}-{random.Next(1000, 9999)}",
                            OrderId = orderDto.OrderId, // Use the generated OrderId
                            JeansId = cartItem.JeansId,
                            Quantity = cartItem.Quantity,
                            UnitPrice = cartItem.Price
                        };

                        var requestBody1 = orderItem; // Single order item
                        string jsonContent1 = JsonConvert.SerializeObject(requestBody1);
                        var content1 = new StringContent(jsonContent1, Encoding.UTF8, "application/json");

                        // Send the order item to the API
                        HttpResponseMessage orderItemResponse =client.PostAsync(apiBaseUrl + "AddOrderItem", content1).Result;
                        if (!orderItemResponse.IsSuccessStatusCode)
                        {
                            return View("Error"); // Handle any error while adding order items
                        }
                        else
                        {
                            flag = true;
                        }
                    }

                    if (flag)
                    {
                        // Clear Cart after successfully creating order items
                        return RedirectToAction("Clear", "Cart", new { orderId });
                    }
                  
                }
            }
            return View("Error");
           
        }
    }
}
