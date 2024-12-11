using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace JeanStation.Entities
{
    public class Order
    {
        [Key]
        public string OrderId {  get; set; }
        [ForeignKey("CustomerNavigation")]
        public string CustomerId { get; set; }
        public DateTime OrderDate { get; set; }
        public double Amount { get; set; }
        public string OrderStatus { get; set; }
        public string PaymentStatus { get; set; }
        public string City {  get; set; }
        public string Address { get; set; }
        public Customer CustomerNavigation { get; set; }
    }
}