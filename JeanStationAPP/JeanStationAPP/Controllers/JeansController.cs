using JeanStationAPP.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Net;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;


namespace JeanStation.Controllers
{
    public class JeansController : Controller
    {
        private HttpClient _httpClient;
        private string _apiBaseUrl = "http://localhost:61124/api/Jeans/";

        public JeansController()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new System.Uri(_apiBaseUrl);
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
        [HttpGet, Route("Shopkeeper/BrowseAllJeans1")]
        public ActionResult BrowseAllJeans1()
        {
            HttpResponseMessage response = _httpClient.GetAsync("GetAll").Result;
            if (response.IsSuccessStatusCode)
            {
                var data = response.Content.ReadAsStringAsync();
                var jeansList = JsonConvert.DeserializeObject<IEnumerable<Jeans>>(data.Result);
                return View(jeansList);
            }
            return View(new List<Jeans>());
        }
        [HttpGet, Route("Customer/BrowseAllJeans")]
        public ActionResult BrowseAllJeans()
        {
            HttpResponseMessage response = _httpClient.GetAsync("GetAll").Result;
            if (response.IsSuccessStatusCode)
            {
                var data =response.Content.ReadAsStringAsync();
                var jeansList = JsonConvert.DeserializeObject<IEnumerable<Jeans>>(data.Result);
                return View(jeansList);
            }
            return View(new List<Jeans>());
        }
        //Get by brand name
        public ActionResult SearchByBrand(string brandName)
        {
            if (string.IsNullOrEmpty(brandName))
            {
                TempData["Error"] = "Brand name cannot be empty.";
                return RedirectToAction("BrowseAllJeans");
            }

            try
            {
                // Call the Brand controller API to get the jeans by brand name
                HttpResponseMessage response = _httpClient.GetAsync($"GetJeansByBrandName/{brandName}").Result;

                if (response.IsSuccessStatusCode)
                {
                    var data = response.Content.ReadAsStringAsync();

                    // Directly deserialize the response to a list of Jeans
                    var jeansList = JsonConvert.DeserializeObject<List<Jeans>>(data.Result);

                    // If we have jeans data, pass it to the view
                    if (jeansList != null && jeansList.Count > 0)
                    {
                        return View("SearchByBrand", jeansList);
                    }
                    else
                    {
                        TempData["Error"] = "No jeans found for the specified brand.";
                    }
                }
                else
                {
                    TempData["Error"] = "Failed to fetch jeans data from the API.";
                }
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"Error: {ex.Message}";
            }

            // Redirect back to the Index page if something goes wrong
            return RedirectToAction("BrowseAllJeans");
        }


        // GET: Jeans/Details/{id}
        public ActionResult Details(string id)
        {
            HttpResponseMessage response = _httpClient.GetAsync($"GetJeansById/{id}").Result;
            if (response.IsSuccessStatusCode)
            {
                var data = response.Content.ReadAsStringAsync();
                var jeans = JsonConvert.DeserializeObject<Jeans>(data.Result);
                return View(jeans);
            }
            return HttpNotFound();
        }

        // GET: Jeans/Create
        public ActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Jeans jeans)
        {
            if (ModelState.IsValid)
            {
                var jsonContent = JsonConvert.SerializeObject(jeans);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
                HttpResponseMessage response = _httpClient.PostAsync("AddJeans", content).Result;
                if (response.IsSuccessStatusCode)
                    return RedirectToAction("BrowseAllJeans1");
            }
            return View("BrowseAllJeans1");
        }

        // GET: Jeans/Edit/{id}
        // GET: Jeans/Edit/{id}
        [HttpGet]
        public ActionResult Edit(string id)  // Add 'id' parameter
        {
            HttpResponseMessage response = _httpClient.GetAsync($"GetJeansById/{id}").Result;  // Pass the id to the API
            if (response.IsSuccessStatusCode)
            {
                var data = response.Content.ReadAsStringAsync();
                var jeans = JsonConvert.DeserializeObject<Jeans>(data.Result);
                return View(jeans);  // Pass the jeans object to the view
            }
            return HttpNotFound();  // Return a 404 if jeans not found
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Jeans jeans)
        {
            if (ModelState.IsValid)
            {
                var jsonContent = JsonConvert.SerializeObject(jeans);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                // Sending the updated jeans data to the API for updating
                HttpResponseMessage response = _httpClient.PutAsync($"UpdateJeans", content).Result;

                if (response.IsSuccessStatusCode)
                    return RedirectToAction("BrowseAllJeans1");  // Redirect to the Index page on success
            }
            return View("BrowseAllJeans1");  // Return to the Edit view if validation fails
        }

        // GET: Jeans/Delete/{id}
        [HttpPost]
        public ActionResult Delete(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Jeans ID cannot be empty.");
            }

            HttpResponseMessage response = _httpClient.DeleteAsync($"DeleteJeans/{id}").Result;

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("BrowseAllJeans1"); // Redirect to the list page after successful deletion
            }
            else
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, "Failed to delete the jeans.");
            }
        }

    }
}
