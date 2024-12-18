using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace JeanStationAPP.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Username is required.")]
        [Display(Name = "User Name")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [DataType(DataType.Password)] // Ensures proper rendering of password fields
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Role")]
        public string Role { get; set; } // Optional field for user role (e.g., Customer, Shopkeeper)
    }
}
