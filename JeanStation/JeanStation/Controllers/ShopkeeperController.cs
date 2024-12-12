using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using JeanStation.Repository;
using JeanStation.Entities;

namespace JeanStation.Controllers
{
    public class ShopkeeperController : ApiController
    {
        private IShopkeeperRepository repository;
        public ShopkeeperController()
        {
            repository = new ShopkeeperRepository();
        }
        [HttpPost, Route("AddShopkeeper")]
        public IHttpActionResult Add([FromBody] Shopkeeper shopkeeper)
        {
            try
            {
                repository.AddShopkeeper(shopkeeper);
                return Ok(shopkeeper);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
        [HttpPost,Route("Validate")]
        public IHttpActionResult ValidateShopkeeper([FromBody] Shopkeeper shopkeeper)
        {
            if (shopkeeper == null)
            {
                return BadRequest("Shopkeeper cannot be null.");
            }

            try
            {
                bool isValid = repository.ValidateShopkeeper(shopkeeper); // Call to repository method

                if (isValid)
                {
                    return Ok("Shopkeeper exists.");
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                // Log the error (optional) and return a generic error message
                return BadRequest(ex.Message);
            }
        }

    }
}
