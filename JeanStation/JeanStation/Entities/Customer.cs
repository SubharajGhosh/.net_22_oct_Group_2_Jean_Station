using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace JeanStation.Entities
{
    public class Customer
    {
        [Required(ErrorMessage = "CustomerId is Required")]
        public string CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string Email {  get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        [ForeignKey("UserNavigation")]
        public string UserId { get; set; }
        public User UserNavigation { get; set; }

    }
}