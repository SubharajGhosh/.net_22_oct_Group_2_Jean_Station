using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JeanStation.Entities;
using JeanStation.Models;

namespace JeanStation.Repository
{
    internal interface IShopkeeperRepository
    {
        Shopkeeper GetShopkeeperByUserId(string UserId);
        Shopkeeper UpdateShopkeeper(ShopkeeperDto shopkeeperDto);
        bool DeleteShopkeeper(string shopkeeperId);
        IEnumerable<Customer> GetAllCustomers();
    }
}
