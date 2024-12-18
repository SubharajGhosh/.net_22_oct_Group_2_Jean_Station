using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JeanStationAPP.Models
{
    public class SignUpModel
    {
        public string UserId { get; set; }
        public string CustomerId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Role { get; set; } // "Customer" or "Shopkeeper"
        public string CustomerName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
    }
}