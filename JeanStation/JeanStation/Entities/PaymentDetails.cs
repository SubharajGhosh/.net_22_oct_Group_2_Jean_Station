using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace JeanStation.Entities
{
    public class PaymentDetails
    {
        [Key]
        public string PaymentId {  get; set; }
        public string PaymentMode {  get; set; }
        public string PaymentStatus { get; set; }
        [ForeignKey("OrderNavigation")]
        public string OrderId {  get; set; }
        public Order OrderNavigation { get; set; }
        public double TotalAmount { get; set; }
        public DateTime PaymentDate { get; set; }
        
    }
}