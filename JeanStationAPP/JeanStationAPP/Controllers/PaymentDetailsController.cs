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
    public class PaymentDetailsController : Controller
    {
        private readonly string apiBaseUrl = "http://localhost:61124/api/PaymentDetails/";

        public ActionResult Details(string orderId)
        {
            string customerId = Session["CustomerId"] as string;
            if (string.IsNullOrEmpty(customerId))
            {
                return RedirectToAction("Login", "User"); // Redirect to login if no customerId in session
            }

            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = client.GetAsync(apiBaseUrl + $"GetPaymentByOrderId/{orderId}").Result;
                if (response.IsSuccessStatusCode)
                {
                    var data = response.Content.ReadAsStringAsync();
                    var paymentDetails = JsonConvert.DeserializeObject<List<PaymentDetails>>(data.Result);
                    return View(paymentDetails);
                }
                return View("Error");
            }
        }
        //[HttpGet]
        //public ActionResult PaymentGateway(string orderId)
        //{
        //    string customerId = Session["CustomerId"] as string;
        //    if (string.IsNullOrEmpty(customerId))
        //    {
        //        return RedirectToAction("Login", "User"); // Redirect to login if no customerId in session
        //    }

        //    // Pass the order details to the view
        //    ViewBag.OrderId = orderId;
        //    return View();
        //}

        
        public ActionResult ProcessPayment(string orderId, string paymentMode = "Cash")
        {
            string customerId = Session["CustomerId"] as string;
            if (string.IsNullOrEmpty(customerId))
            {
                return RedirectToAction("Login", "User"); // Redirect to login if no customerId in session
            }

            HttpClient client = new HttpClient();
            try
            {
                // Prepare the request body
                var requestBody = new { orderId, paymentMode };
                string jsonContent = JsonConvert.SerializeObject(requestBody);
                HttpContent content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                // Send the payment processing request
                HttpResponseMessage response = client.PostAsync(apiBaseUrl + "ProcessPayment?orderId="+orderId+"&"+"paymentMode="+paymentMode, null).Result;

                if (response.IsSuccessStatusCode)
                {
                    // Redirect to OrderController's CreateOrder action on success
                    return RedirectToAction("Details", new { orderId=orderId });
                }
                else
                {
                    // Handle failure - show an error view or message
                    var errorContent = response.Content.ReadAsStringAsync();
                    ViewBag.ErrorMessage = $"Failed to process payment. Status: {response.StatusCode}, Error: {errorContent}";
                    return View("Error"); // Return an error view
                }
            }
            catch (HttpRequestException ex)
            {
                // Handle HTTP request errors
                ViewBag.ErrorMessage = $"Request error: {ex.Message}";
                return View("Error"); // Return an error view
            }
            catch (Exception ex)
            {
                // Handle unexpected errors
                ViewBag.ErrorMessage = $"Unexpected error: {ex.Message}";
                return View("Error"); // Return an error view
            }
        }
    }
}
