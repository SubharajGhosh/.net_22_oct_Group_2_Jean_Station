using JeanStation.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace JeanStation.Models
{
    public class PaymentDetailsdto
    {
        public string PaymentId { get; set; }
        public string PaymentMode { get; set; }
        public string PaymentStatus { get; set; }
       
        public string OrderId { get; set; }
        
        public double TotalAmount { get; set; }
        public DateTime PaymentDate { get; set; }
    }
}