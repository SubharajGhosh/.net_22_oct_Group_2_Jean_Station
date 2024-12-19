using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using JeanStationAPP.Models;
using System.Text;

namespace JeanStationAPP.Controllers
{
    public class CartController : Controller
    {
        private string apiBaseUrl = "http://localhost:61124/api/Cart/";

        // GET: Cart
        public ActionResult Index()
        {
            string customerId = Session["CustomerId"] as string;
            if (string.IsNullOrEmpty(customerId))
            {
                return RedirectToAction("Login", "User"); // Redirect to login if no customerId in session
            }

            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = client.GetAsync(apiBaseUrl + $"items/{customerId}").Result;
                if (response.IsSuccessStatusCode)
                {
                    var data = response.Content.ReadAsStringAsync();
                    var cartItems = JsonConvert.DeserializeObject<List<Cart>>(data.Result);
                    return View(cartItems);
                }
                return View("Error");
            }
        }

        public ActionResult GetCartDetails(string jeansId, double price)
        {
            string customerId = Session["CustomerId"] as string;
            if (string.IsNullOrEmpty(customerId))
            {
                return RedirectToAction("Login", "User"); // Redirect to login if no customerId in session
            }

            Random random = new Random();
            string cartId = $"CID-{DateTime.Now:yyyyMMdd}-{random.Next(1000, 9999)}";
            var cart = new Cart()
            {
                CartId = cartId,
                Price = price,
                Quantity = 1,
                CustomerId = customerId, // Use customerId from session
                JeansId = jeansId
            };
            using (HttpClient client = new HttpClient())
            {
                var updateData = cart;
                string jsonContent = JsonConvert.SerializeObject(updateData);
                HttpContent content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
                HttpResponseMessage response = client.PostAsync(apiBaseUrl + "add", content).Result;
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("BrowseAllJeans", "Jeans");
                }
                return View("Error");
            }

            return  RedirectToAction("BrowseAllJeans", "Jeans");
        }

     
        public ActionResult AddToCart(Cart cart)
        {
            string customerId = Session["CustomerId"] as string;
            if (string.IsNullOrEmpty(customerId))
            {
                return RedirectToAction("Login", "User"); // Redirect to login if no customerId in session
            }

            using (HttpClient client = new HttpClient())
            {
                var updateData = cart;
                string jsonContent = JsonConvert.SerializeObject(updateData);
                HttpContent content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
                HttpResponseMessage response = client.PostAsync(apiBaseUrl + "add", content).Result;
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("BrowseAllJeans", "Jeans");
                }
                return View("Error");
            }
        }

        public ActionResult Remove(string cartId)
        {
            string customerId = Session["CustomerId"] as string;
            if (string.IsNullOrEmpty(customerId))
            {
                return RedirectToAction("Login", "User"); // Redirect to login if no customerId in session
            }

            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = client.DeleteAsync(apiBaseUrl + $"remove/{cartId}").Result;
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                return View("Error");
            }
        }

        public ActionResult Clear(string orderId)
        {
            string customerId = Session["CustomerId"] as string;
            if (string.IsNullOrEmpty(customerId))
            {
                return RedirectToAction("Login", "User"); // Redirect to login if no customerId in session
            }

            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = client.DeleteAsync(apiBaseUrl + $"clear/{customerId}").Result;
                if (response.IsSuccessStatusCode)
                {
                    // Redirect to ProcessPayment after clearing the cart
                    return RedirectToAction("ProcessPayment", "PaymentDetails", new { orderId =orderId});
                }
                return View("Error");
            }
        }

       
        public ActionResult UpdateQuantity(string cartId, int newQuantity)
        {
            string customerId = Session["CustomerId"] as string;
            if (string.IsNullOrEmpty(customerId))
            {
                return RedirectToAction("Login", "User"); // Redirect to login if no customerId in session
            }

            using (HttpClient client = new HttpClient())
            {
                var updateData = new { cartId, newQuantity };
                string jsonContent = JsonConvert.SerializeObject(updateData);
                HttpContent content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
                HttpResponseMessage response = client.PutAsync(apiBaseUrl + "update-quantity?CartId="+cartId+"&"+"Quantity="+newQuantity, null).Result;
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                return View("Error");
            }
        }

        public ActionResult Checkout()
        {
            string customerId = Session["CustomerId"] as string;
            if (string.IsNullOrEmpty(customerId))
            {
                return RedirectToAction("Login", "User"); // Redirect to login if no customerId in session
            }

            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = client.GetAsync(apiBaseUrl + $"items/{customerId}").Result;
                if (response.IsSuccessStatusCode)
                {
                    var data = response.Content.ReadAsStringAsync();
                    var cartItems = JsonConvert.DeserializeObject<List<Cart>>(data.Result);

                    // Redirect to OrderItemController with cart items
                    TempData["CartItems"] = cartItems;
                    return RedirectToAction("AddOrderItems", "OrderItem");
                }   
                return View("Error");
            }
        }
    }
}
