using JeanStationAPP.Models;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace JeanStation.Controllers
{
    public class CustomerController : Controller
    {
        private readonly Uri _baseApiUrl = new Uri("http://localhost:61124/api/Customer/"); // Replace with actual API base URL

        public CustomerController()
        {
        }

        // Redirect to Dashboard after login
        public ActionResult Dashboard()
        {
            var customerId = Session["CustomerId"] as string;
            
            if (string.IsNullOrEmpty(customerId))
            {
                return RedirectToAction("Login", "User"); // Redirect to login if not logged in
            }

            // Provide links to other functionalities
            return View(); // View contains links to BrowseAllJeans, UpdateCustomer, ShowDetails, and OrderHistory
        }

        // Redirect to BrowseAllJeans in JeansController
        public ActionResult BrowseAllJeans()
        {
            return RedirectToAction("BrowseAllJeans", "Jeans");
        }

        // Update Customer Details View
        public ActionResult UpdateCustomer()
        {
            var UserId = Session["UserId"] as string;
            if (string.IsNullOrEmpty(UserId))
            {
                return RedirectToAction("Login", "User"); // Redirect to login if not logged in
            }

            using (var httpClient = new HttpClient { BaseAddress = _baseApiUrl })
            {
                var response = httpClient.GetAsync($"GetCustomerById/{UserId}").Result;
                if (!response.IsSuccessStatusCode)
                {
                    return HttpNotFound("Customer not found.");
                }
                var data = response.Content.ReadAsStringAsync();
                var customer = JsonConvert.DeserializeObject<Customer>(data.Result);
                return View(customer); // Show update customer view with existing data
            }
        }

        
        public ActionResult UpdateCustomer(Customer customer)
        {
            if (!ModelState.IsValid)
            {
                return View(customer); // Return view with validation errors
            }

            using (var httpClient = new HttpClient { BaseAddress = _baseApiUrl })
            {
                string jsonContent = JsonConvert.SerializeObject(customer);
                HttpContent content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
                HttpResponseMessage response = httpClient.PutAsync("UpdateCustomer", content).Result;

                if (!response.IsSuccessStatusCode)
                {
                    ModelState.AddModelError("", "Failed to update customer.");
                    return View(customer);
                }

                TempData["SuccessMessage"] = "Customer details updated successfully.";
                return RedirectToAction("Dashboard");
            }
        }

        // Show Customer Details
        public ActionResult ShowDetails()
        {
            var UserId = Session["userId"] as string;
            if (string.IsNullOrEmpty(UserId))
            {
                return RedirectToAction("Login", "User"); // Redirect to login if not logged in
            }

            using (var httpClient = new HttpClient { BaseAddress = _baseApiUrl })
            {
                var response = httpClient.GetAsync($"GetCustomerById/{UserId}").Result;
                if (!response.IsSuccessStatusCode)
                {
                    return HttpNotFound("Customer not found.");
                }
                var data = response.Content.ReadAsStringAsync();
                var customer = JsonConvert.DeserializeObject<Customer>(data.Result);
                return View(customer); // Show customer details
            }
        }

        // Redirect to Order History in OrdersController
        public ActionResult OrderHistory()
        {
            var customerId = Session["CustomerId"] as string;
            if (string.IsNullOrEmpty(customerId))
            {
                return RedirectToAction("Login", "User"); // Redirect to login if not logged in
            }

            return RedirectToAction("Index", "Order");
        }
    }
}
