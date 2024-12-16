using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JeanStation.Models
{
    public class SignUpModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Role { get; set; } // "Customer" or "Shopkeeper"
        public string CustomerName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
    }
}