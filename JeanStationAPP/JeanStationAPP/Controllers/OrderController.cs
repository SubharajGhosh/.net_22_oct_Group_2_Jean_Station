using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using JeanStationAPP.Models;

namespace JeanStationAPP.Controllers
{
    public class OrderController : Controller
    {
        private readonly string apiBaseUrl = "http://localhost:61124/api/Order/";

        // GET: Order
        public ActionResult Index()
        {
            string customerId = Session["CustomerId"] as string;
            if (string.IsNullOrEmpty(customerId))
            {
                return RedirectToAction("Login", "User"); // Redirect to login if no customerId in session
            }

            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = client.GetAsync(apiBaseUrl + $"GetOrdersByCustomerId/{customerId}").Result;
                if (response.IsSuccessStatusCode)
                {
                    var data = response.Content.ReadAsStringAsync();
                    var orders = JsonConvert.DeserializeObject<List<Order>>(data.Result);
                    return View(orders);
                }
                return View("Error");
            }
        }

        [HttpPut]
        public ActionResult UpdateOrder(string orderId, string OrderStatus)
        {
            string customerId = Session["CustomerId"] as string;
            if (string.IsNullOrEmpty(customerId))
            {
                return RedirectToAction("Login", "User"); // Redirect to login if no customerId in session
            }

            using (HttpClient client = new HttpClient())
            {
                var requestBody = new { orderId, OrderStatus };
                string jsonContent = JsonConvert.SerializeObject(requestBody);
                HttpContent content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
                HttpResponseMessage response = client.PutAsync(apiBaseUrl + "UpdateOrderStatus/", content).Result;
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Details", new { orderId });
                }
                return View("Error");
            }
        }

        public ActionResult Details(string orderId)
        {
            string customerId = Session["CustomerId"] as string;
            if (string.IsNullOrEmpty(customerId))
            {
                return RedirectToAction("Login", "User"); // Redirect to login if no customerId in session
            }

            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = client.GetAsync(apiBaseUrl + $"GetOrderById/{orderId}").Result;
                if (response.IsSuccessStatusCode)
                {
                    var data = response.Content.ReadAsStringAsync();
                    var order = JsonConvert.DeserializeObject<Order>(data.Result);
                    return View(order);
                }
                return View("Error");
            }
        }

        public ActionResult Delete(string orderId)
        {
            string customerId = Session["CustomerId"] as string;
            if (string.IsNullOrEmpty(customerId))
            {
                return RedirectToAction("Login", "User"); // Redirect to login if no customerId in session
            }

            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = client.DeleteAsync(apiBaseUrl + $"DeleteOrder/{orderId}").Result;
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                return View("Error");
            }
        }
    }
}
