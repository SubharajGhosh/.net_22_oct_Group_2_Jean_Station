using JeanStation.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JeanStation.Repository
{
    internal interface IShopkeeperRepository
    {
        Shopkeeper GetShopkeeperByShopkeeperId(string shopkeeperId);
        Shopkeeper UpdateShopkeeper(Shopkeeper shopkeeper);
        bool DeleteShopkeeper(string shopkeeperId);
        IEnumerable<Customer> GetAllCustomers();

    }
}
