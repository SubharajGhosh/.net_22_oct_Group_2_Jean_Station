using JeanStation.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JeanStation.Repository
{
    internal interface IOrderItemInterface
    {
        
        void Add(OrderItem orderItem);
        decimal CalculateCartTotal(string OrderId);
        //IEnumerable<OrderItem> GetByCustomerId(string customerId);
        void Remove(string OrderId);
        void UpdateQuantity(string orderItemId, int newQuantity);
        void ClearCart();
        IEnumerable<OrderItem> GetByOrderId(string orderId);


    }
}
