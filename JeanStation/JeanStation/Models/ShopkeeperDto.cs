using JeanStation.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace JeanStation.Models
{
    public class ShopkeeperDto
    {
            public string ShopkeeperId { get; set; }
            public string ShopName { get; set; }
            public string Location { get; set; }
            public string Address { get; set; }
            public string UserId { get; set; }

        
    }
}