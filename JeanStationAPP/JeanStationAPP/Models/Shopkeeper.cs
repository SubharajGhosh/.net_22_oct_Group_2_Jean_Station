using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace JeanStationAPP.Models
{
    public class Shopkeeper
    {
        
            public string ShopkeeperId { get; set; }
            public string ShopName { get; set; }
            public string Location { get; set; }
            public string Address { get; set; }
            public string UserId { get; set; }
        }
    }
