using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using JeanStation.Entities;
using JeanStation.Repository;

namespace JeanStation.Controllers
{
    public class BrandController : ApiController
    {
        private BrandRepository brandRepository { get; set; }

        public BrandController()
        {
            brandRepository = new BrandRepository();
        }

        [HttpGet,Route("GetAll")]
        public IHttpActionResult GetAll()
        {
            var v =brandRepository.GetAllBrands();
            return Ok(v);
        }

        [HttpGet,Route("GetBrandsByName/{name}")]
        public IHttpActionResult GetBrandsByName(string name)
        {
            var v = brandRepository.GetBrandByName(name);
            return Ok(v);
        }

        [HttpGet, Route("GetBrandsById/{name}")]
        public IHttpActionResult GetBrandsById(string id)
        {
            var v = brandRepository.GetBrandById(id);
            return Ok(v);
        }

        [HttpDelete, Route("DeleteBrand/{id}")]
        public IHttpActionResult DeleteBrand(string id) 
        {
            brandRepository.DeleteBrand(id);
            return Ok("Brand Record Deleted");
        }

        [HttpPost,Route("AddBrand")]
        public IHttpActionResult AddBrand(Brand brand)
        {
            brandRepository.AddBrand(brand);
            return Ok();

        }

        [HttpPut, Route("UpdateBrand")]
        public IHttpActionResult UpdateBrand(Brand brand) 
        {
            brandRepository.UpdateBrand(brand);
            return Ok(brand);
        }


    }
}
