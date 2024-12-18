using JeanStation.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JeanStation.Models;
namespace JeanStation.Repository
{
    internal interface IOrderItemInterface
    {
        
        void Add(OrderItemdto orderItem);
        //double CalculateCartTotal(string OrderId);
        //IEnumerable<OrderItemdto> GetByCustomerId(string customerId);
        void Remove(string OrderId);
        
        //
        IEnumerable<OrderItemdto> GetByOrderId(string orderId);
        //OrderItem GetById(string orderItemId);

    }
}
