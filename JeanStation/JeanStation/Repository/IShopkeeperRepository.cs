using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JeanStation.Entities;

namespace JeanStation.Repository
{
    internal interface IShopkeeperRepository
    {
        void AddShopkeeper (Shopkeeper shopkeeper);
        //void UpdateShopkeeper (Shopkeeper shopkeeper);
        bool ValidateShopkeeper(Shopkeeper shopkeeper);
    }
}
