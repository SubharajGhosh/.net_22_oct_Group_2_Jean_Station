using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace JeanStationAPP.Models
{
    public class Cart
    {
        public string CartId { get; set; }
       
        public string JeansId { get; set; }
        public double Price { get; set; }
        
        public string CustomerId { get; set; }
        public int Quantity { get; set; }
    }
}