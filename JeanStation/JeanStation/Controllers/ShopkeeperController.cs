using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using JeanStation.Repository;
using JeanStation.Entities;
using JeanStation.Models;
using System.Runtime.Remoting.Contexts;

namespace JeanStation.Controllers
{
    [RoutePrefix("api/Shopkeeper")]
    public class ShopkeeperController : ApiController
    {
        private IShopkeeperRepository repository;
        public ShopkeeperController()
        {
            repository = new ShopkeeperRepository();
        }
        [HttpGet, Route("GetShopkeeperByUserId/{UserId}")]
        public IHttpActionResult GetShopkeeperByUserId(string UserId)
        {
            try
            {
                var shopkeeper = repository.GetShopkeeperByUserId(UserId);
                return Ok(shopkeeper);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
        [HttpPut, Route("UpdateShopkeeper")]
        public IHttpActionResult UpdateShopkeeper([FromBody] ShopkeeperDto shopkeeperDto)
        {
            try
            {
                repository.UpdateShopkeeper(shopkeeperDto);
                return Ok(shopkeeperDto);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
        [HttpDelete]
        [Route("DeleteShopkeeper/{shopkeeperId}")]
        public IHttpActionResult DeleteShopkeeper(string shopkeeperId)
        {
            if (shopkeeperId == null)
                return BadRequest("Shopkeeper data is null.");

            try
            {
                repository.DeleteShopkeeper(shopkeeperId);
                return Ok("Deleted");
            }
            catch (InvalidOperationException)
            {
                return NotFound();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
        [HttpGet]
        [Route("GetAllCustomers")]
        public IHttpActionResult GetAllCustomers()
        {
            try
            {
                var customers = repository.GetAllCustomers();
                return Ok(customers);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }




    }
}
