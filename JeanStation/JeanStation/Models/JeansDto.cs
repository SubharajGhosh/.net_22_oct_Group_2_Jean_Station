using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JeanStation.Models
{
    public class JeansDto
    {
        public string JeansId { get; set; }
        public string BrandId { get; set; }
        public string BrandName { get; set; }
        public string Type { get; set; }
        public string Color { get; set; }
        public string Size { get; set; }
        public double Price { get; set; }
        public int Stock { get; set; }
        public string ImageUrl { get; set; }
    }
}