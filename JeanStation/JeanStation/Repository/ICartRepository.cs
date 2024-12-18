using JeanStation.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JeanStation.Models;
namespace JeanStation.Repository
{
    internal interface ICartRepository
    {
        void AddToCart(Cartdto cart);
        List<Cartdto> GetCartItems(string customerId);
        void RemoveCartItem(string cartId);
        void EditCartItem(Cartdto cart);
        //void UpdateQuantity(string cartId, int newQuantity);
        void ClearCart(string customerId);
        bool UpdateQuantity(string cartId, int newQuantity);
    }
}
