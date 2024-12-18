using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JeanStation.Entities;
using JeanStation.Models;
namespace JeanStation.Repository
{
    internal interface IOrderRepository
    {
        void CreateOrder(Orderdto orderDto);
     void OrderUpdateStatus(string OrderId,string orderstatus);
     void DeleteOrder(string OrderId);
     Orderdto GetOrderById(string OrderId);
     List<Orderdto> GetOrdersByCustomerId(string customerId);
    }
}
