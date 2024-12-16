using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace JeanStation.Models
{
    public class Orderdto
    {
        public string OrderId { get; set; }
        public string CustomerId { get; set; }
        public DateTime OrderDate { get; set; }
        public double Amount { get; set; }
        public string OrderStatus { get; set; }
        public string PaymentStatus { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
    }
}