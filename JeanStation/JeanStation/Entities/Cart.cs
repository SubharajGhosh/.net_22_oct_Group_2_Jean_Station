using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace JeanStation.Entities
{
    public class Cart
    {
        public string CartId { get; set; }
        [ForeignKey("JeanNavigation")]
        public string JeansId { get; set; }
        public double Price { get; set; }
        [ForeignKey("CustomerNavigation")]
        public string CustomerId { get; set; }
        public int Quantity { get; set; }
        public Customer CustomerNavigation { get; set; }
        public Jeans JeanNavigation { get; set; }
    }
}