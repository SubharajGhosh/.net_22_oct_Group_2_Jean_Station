using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace JeanStationAPP.Models
{
    public class User
    {
        [Required(ErrorMessage = "UserId is Required")]
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
    }
}