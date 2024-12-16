using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace JeanStation.Entities
{
    public class OrderItem
    {
     
        public string OrderItemId {  get; set; }
        public int Quantity { get; set; }

        [ForeignKey("OrderNavigation")]

        public string OrderId {  get; set; }

        public int UnitPrice { get; set; }  

        public double TotalPrice { get; set; }

        [ForeignKey("JeanNavigation")]

        public string JeansId { get; set; }

        public Order OrderNavigation { get; set; }
        public Jeans JeanNavigation { get; set; }







    }
}