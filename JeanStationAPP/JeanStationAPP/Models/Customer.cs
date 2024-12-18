using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace JeanStationAPP.Models
{
    public class Customer
    {
        [Required(ErrorMessage = "CustomerId is Required")]
        public string CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string UserId { get; set; }
        
    }
}