﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace JeanStationAPP.Models
{
    public class OrderItem
    {
        public string OrderItemId { get; set; }
        public int Quantity { get; set; }

        public string OrderId { get; set; }

        public double UnitPrice { get; set; }

        public double TotalPrice { get; set; }

        public string JeansId { get; set; }

    }
}