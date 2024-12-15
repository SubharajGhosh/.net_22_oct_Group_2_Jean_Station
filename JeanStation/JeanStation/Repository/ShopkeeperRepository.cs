using JeanStation.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JeanStation.Repository
{
    public class ShopkeeperRepository : IShopkeeperRepository
    {
        private readonly JeanStationContext _context;

        public ShopkeeperRepository(JeanStationContext context)
        {
            _context = context;
        }

        // Get a shopkeeper by ShopkeeperId
        public Shopkeeper GetShopkeeperByShopkeeperId(string shopkeeperId)
        {
            return _context.Shopkeepers
                .Include("UserNavigation") // Include UserNavigation for User details
                .FirstOrDefault(s => s.ShopkeeperId == shopkeeperId);
        }

        // Update an existing shopkeeper
        public Shopkeeper UpdateShopkeeper(Shopkeeper shopkeeper)
        {
            var existingShopkeeper = _context.Shopkeepers.FirstOrDefault(s => s.ShopkeeperId == shopkeeper.ShopkeeperId);
            if (existingShopkeeper == null)
            {
                throw new InvalidOperationException("Shopkeeper not found.");
            }

            existingShopkeeper.ShopName = shopkeeper.ShopName;
            existingShopkeeper.Location = shopkeeper.Location;
            existingShopkeeper.Address = shopkeeper.Address;

            _context.SaveChanges(); // Commit the update to the database
            return existingShopkeeper;
        }

        // Delete a shopkeeper
        public bool DeleteShopkeeper(string shopkeeperId)
        {
            var shopkeeper = _context.Shopkeepers.FirstOrDefault(s => s.ShopkeeperId == shopkeeperId);
            if (shopkeeper == null)
            {
                return false;
            }

            _context.Shopkeepers.Remove(shopkeeper);
            _context.SaveChanges(); // Commit the deletion to the database
            return true;
        }
        public IEnumerable<Customer> GetAllCustomers()
        {
            return _context.Customers
                .Include("UserNavigation") // Include UserNavigation for User details
                .ToList();
        }



    }
}