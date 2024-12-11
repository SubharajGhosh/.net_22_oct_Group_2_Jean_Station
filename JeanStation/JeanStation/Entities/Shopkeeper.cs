using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
namespace JeanStation.Entities
{
    public class Shopkeeper
    {
        public string ShopkeeperId {  get; set; }   
        public string ShopName { get; set; }
        public string Location { get; set; }
        public string Address { get; set; }
        [ForeignKey("UserNavigation")]
        public string UserId { get; set; }
        public User UserNavigation { get; set; }

    }
}