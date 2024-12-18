using JeanStationAPP.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace JeanStationAPP.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        private string baseApiUrl = "http://localhost:61124/api/Customer/";
        private string baseApiUrl1 = "http://localhost:61124/api/Shopkeeper/";

        private readonly HttpClient _httpClient;

        public UserController()
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri("http://localhost:61124/api/User/") // Replace with your API's base URL
            };
        }

        public ActionResult SignUp()
        {
            return View();
        }

        // POST: User/SignUp
        [HttpPost]
        public ActionResult SignUp(SignUpModel model)
        {
            Random random = new Random();
            string UID = $"UID-{DateTime.Now:yyyyMMdd}-{random.Next(1000, 9999)}";
            string CID = $"CID-{DateTime.Now:yyyyMMdd}-{random.Next(1000, 9999)}";
            model.UserId = UID;
            model.CustomerId = CID;
            if (ModelState.IsValid)
            {
                var content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");

                // Make the HTTP POST request to the Web API asynchronously
                HttpResponseMessage response = _httpClient.PostAsync("signup", content).Result;

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Login");
                }
                else
                {
                    return RedirectToAction("/Error");
                }
            }
            else
            {
                return View();
            }

        }

        // GET: User/Login
        public ActionResult Login()
        {
            return View();
        }

        // POST: User/Login
        [HttpPost]
        public ActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");

                // Make the HTTP POST request to the Web API asynchronously
                HttpResponseMessage response = _httpClient.PostAsync("login", content).Result;

                
                
                
                if (response.IsSuccessStatusCode)
                {
                    // Parse the response (assume the role is returned in JSON format)
                    var responseBody = response.Content.ReadAsStringAsync().Result;
                    var role = JsonConvert.DeserializeObject<LoginModelObject>(responseBody);

                    
                        // Set session variables for the logged-in user
                        Session["Role"] = role.Role;
                        Session["UserName"] = model.UserName; // Assuming LoginModel has a UserName property
                        Session["UserId"] = role.UserId;
                    
                    

                   

                    
                   

                    
                    
                    if (role.Role == "Customer")
                        {
                        var customerId = "";
                        HttpClient httpClient = new HttpClient();
                        HttpResponseMessage response1 = httpClient.GetAsync(baseApiUrl + $"GetCustomerById/{role.UserId}").Result;

                        var data = response1.Content.ReadAsStringAsync();
                        var customer = JsonConvert.DeserializeObject<Customer>(data.Result);
                        customerId = customer.CustomerId;
                        Session["CustomerId"] = customerId;
                            return RedirectToAction("Dashboard", "Customer");
                        }
                        else if (role.Role == "Shopkeeper")
                        {
                        var shopkeeper1 = "";
                        HttpClient httpClient1 = new HttpClient();
                        HttpResponseMessage response12 = httpClient1.GetAsync(baseApiUrl1 + $"GetShopkeeperByUserId/{role.UserId}").Result;

                        var data1 = response12.Content.ReadAsStringAsync();
                        var shopkeeper = JsonConvert.DeserializeObject<Shopkeeper>(data1.Result);
                        shopkeeper1 = shopkeeper.ShopkeeperId;
                        Session["Shopkeeper"] = shopkeeper1;
                            
                            return RedirectToAction("SDashboard", "Shopkeeper");
                        }
                    
                }
            }
            return View();
        }

        // Logout method to clear session
        public ActionResult Logout()
        {
            Session.Clear(); // Clear all session variables
            return RedirectToAction("Login");
        }
    }
}
