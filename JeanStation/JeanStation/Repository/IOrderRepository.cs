using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JeanStation.Entities;
namespace JeanStation.Repository
{
    internal interface IOrderRepository
    {
     void OrderCreate(Order order);
     void OrderUpdateStatus(Order order);
     void DeleteOrder(string OrderId);
     Order GetOrderById(string OrderId);
     void OrderUpdatePayment(Order order);
     List<Order> GetOrdersByCustomerId(string customerId);
    }
}
