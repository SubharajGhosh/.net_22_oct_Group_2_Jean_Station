﻿using JeanStation.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JeanStation.Models;
namespace JeanStation.Repository
{
    internal interface IPaymentDetailsRepository
    {
        IEnumerable<PaymentDetailsdto> GetPaymentByOrderId(string orderId);
        PaymentDetails ProcessPayment(string orderId, string paymentMode);
    }
}
