using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JeanStation.Entities
{
    public class OrderItem
    {
        [Required]
        public string OrderItemId {  get; set; }
        public int Quantity { get; set; }

        [Foreignkey("OrderNavigation")]

        public string OrderId {  get; set; }

        public int UnitPrice { get; set; }  

        public int TotalPrice { get; set; }

        [Foreignkey("JeanNavigation")]

        public string JeansId { get; set; }

        public Order OrderNavigation { get; set; }
        public Jeans JeanNavigation { get; set; }







    }
}