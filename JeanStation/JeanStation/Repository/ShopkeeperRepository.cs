using JeanStation.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JeanStation.Repository
{
    public class ShopkeeperRepository : IShopkeeperRepository
    {
        private JeanStationContext context1;
        public ShopkeeperRepository()
        {
            context1 = new JeanStationContext();
        }
        public void AddShopkeeper(Shopkeeper shopkeeper)
        {
            context1.Shopkeepers.Add(shopkeeper);
            context1.SaveChanges();
        }

        //public void UpdateShopkeeper(Shopkeeper shopkeeper)
        //{
        //    var obj = context1.Shopkeepers.Find(shopkeeper.ShopkeeperId);
        //    obj.ShopName=shopkeeper.ShopName;

        //}

        public bool ValidateShopkeeper(Shopkeeper shopkeeper)
        {
            return context1.Shopkeepers.Any(x=>x.ShopkeeperId == shopkeeper.ShopkeeperId);
        }
    }
}