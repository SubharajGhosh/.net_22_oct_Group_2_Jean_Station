using JeanStationAPP.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace JeanStationAPP.Controllers
{
    public class ShopkeeperController : Controller
    {
        private HttpClient _httpClient;
        private string _apiBaseUrl = "http://localhost:61124/api/Shopkeeper/";

        public ShopkeeperController()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(_apiBaseUrl);
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        // GET: Shopkeeper/Dashboard
        public ActionResult SDashboard()
        {
            var UserId = Session["UserId"] as string;
            if (string.IsNullOrEmpty(UserId))
            {
                return RedirectToAction("Login", "User"); // Redirect to login if not logged in
            }

            try
            {
                var shopkeeperResponse = _httpClient.GetAsync($"GetShopkeeperByUserId/{UserId}").Result;
                Shopkeeper shopkeeper = null;

                if (shopkeeperResponse.IsSuccessStatusCode)
                {
                    var data = shopkeeperResponse.Content.ReadAsStringAsync();
                    shopkeeper = JsonConvert.DeserializeObject<Shopkeeper>(data.Result);
                }

                return View(shopkeeper); // Pass shopkeeper details to view
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        // GET: Shopkeeper/EditShopkeeper
        [HttpGet]
        public ActionResult EditShopkeeper()
        {
            var UserId = Session["UserId"] as string;
            if (string.IsNullOrEmpty(UserId))
            {
                return RedirectToAction("Login", "User"); // Redirect to login if not logged in
            }

            var response = _httpClient.GetAsync($"GetShopkeeperByUserId/{UserId}").Result;
            if (response.IsSuccessStatusCode)
            {
                var data = response.Content.ReadAsStringAsync();
                var shopkeeper = JsonConvert.DeserializeObject<Shopkeeper>(data.Result);
                return View(shopkeeper); // Pass the shopkeeper object to the view for editing
            }
            return HttpNotFound();
        }

        // POST: Shopkeeper/EditShopkeeper
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditShopkeeper(Shopkeeper shopkeeper)
        {
            var shopkeeperId = Session["ShopkeeperId"] as string;
            if (string.IsNullOrEmpty(shopkeeperId))
            {
                return RedirectToAction("Login", "User"); // Redirect to login if not logged in
            }

            if (ModelState.IsValid)
            {
                var jsonContent = JsonConvert.SerializeObject(shopkeeper);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                HttpResponseMessage response = _httpClient.PutAsync("UpdateShopkeeper", content).Result;
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Dashboard"); // Redirect to the dashboard after successful update
                }
            }
            return View(shopkeeper); // Return to the EditShopkeeper view if validation fails
        }

        // POST: Shopkeeper/DeleteShopkeeper
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> DeleteShopkeeper()
        //{
        //    var shopkeeperId = Session["ShopkeeperId"] as string;
        //    if (string.IsNullOrEmpty(shopkeeperId))
        //    {
        //        return RedirectToAction("Login", "User"); // Redirect to login if not logged in
        //    }

        //    var response = await _httpClient.DeleteAsync($"DeleteShopkeeper/{shopkeeperId}");
        //    if (response.IsSuccessStatusCode)
        //    {
        //        return RedirectToAction("Login", "User"); // Redirect to login after successful deletion
        //    }
        //    return new HttpStatusCodeResult(System.Net.HttpStatusCode.InternalServerError, "Failed to delete the shopkeeper.");
        //}

        // GET: Shopkeeper/ViewAllCustomers
        public ActionResult ViewAllCustomers()
        {
            var shopkeeperId = Session["ShopkeeperId"] as string;
            if (string.IsNullOrEmpty(shopkeeperId))
            {
                return RedirectToAction("Login", "User"); // Redirect to login if not logged in
            }

            HttpResponseMessage response = _httpClient.GetAsync("GetAllCustomers").Result;
            if (response.IsSuccessStatusCode)
            {
                var data = response.Content.ReadAsStringAsync();
                var customers = JsonConvert.DeserializeObject<List<Customer>>(data.Result);
                return View(customers); // Return customers list to the view
            }
            return View(new List<Customer>());
        }
    }
}
