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
    [RoutePrefix("api/Jeans")]
    public class JeansController : ApiController
    {
        private JeansRepository JeansRepository { get; set; }

        public JeansController()
        {
            JeansRepository = new JeansRepository();
        }

        [HttpPost,Route("AddJeans")]
        public IHttpActionResult AddJeans([FromBody]Jeans jeans)
        {
            JeansRepository.Add(jeans);
            return Ok();
        }

        [HttpGet,Route("GetJeansById/{id}")]

        public IHttpActionResult GetJeansById(string id)
        {
           var v = JeansRepository.GetById(id);
            return Ok(v);
        }

        [HttpGet, Route("GetJeansByBrandName/{name}")]

        public IHttpActionResult GetJeansByName(string name)
        {
            var v = JeansRepository.GetJeansByBrandName(name);
            return Ok(v);
        }

        [HttpGet, Route("GetAll")]

        public IHttpActionResult GetAll()
        {
            var v = JeansRepository.GetAll();   
            return Ok(v);
        }

        [HttpPut, Route("UpdateJeans")]

        public IHttpActionResult UpdateJeans(Jeans jeans) 
        {
            JeansRepository.UpdateJeans(jeans); 
            return Ok(jeans);
        }

        [HttpDelete, Route("DeleteJeans/{id}")]

        public IHttpActionResult DeleteJeansById(string id) 
        {
            JeansRepository.Delete(id); 
            return Ok("Record Deleted");
        }

        [HttpPut, Route("UpdateStock")]

        public IHttpActionResult UpdateStock(Jeans jeans)
        {
            JeansRepository.UpdateStock(jeans);
            return Ok(jeans);
        }




    }
}
