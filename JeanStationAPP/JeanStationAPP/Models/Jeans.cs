using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Web;

namespace JeanStationAPP.Models
{
    public class Jeans
    {
        public string JeansId { get; set; }
       
        public string BrandId { get; set; }
        public string BrandName {  get; set; }
        public string Type { get; set; }
        public string Color { get; set; }
        public string Size { get; set; }
        public double Price { get; set; }
        public int Stock { get; set; }
       public string ImageUrl { get; set; }
    }
}