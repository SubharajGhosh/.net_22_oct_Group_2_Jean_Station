using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JeanStation.Entities
{
    public class User
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Role {  get; set; }
    }
}