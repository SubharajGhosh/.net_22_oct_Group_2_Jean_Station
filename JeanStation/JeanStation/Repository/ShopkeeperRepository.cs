using JeanStation.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Web;
using JeanStation.Models;

namespace JeanStation.Repository
{
    public class ShopkeeperRepository : IShopkeeperRepository
    {
        private JeanStationContext _context;
        public ShopkeeperRepository()
        {
            _context = new JeanStationContext();
        }
        // Get a shopkeeper by ShopkeeperId
        public Shopkeeper GetShopkeeperByUserId(string UserId)
        {
            return _context.Shopkeepers
                .Include("UserNavigation") // Include UserNavigation for User details
                .FirstOrDefault(s => s.UserId == UserId);
        }

        // Update an existing shopkeeper
        public Shopkeeper UpdateShopkeeper(ShopkeeperDto shopkeeperDto)
        {
            var existingShopkeeper = _context.Shopkeepers.FirstOrDefault(s => s.ShopkeeperId == shopkeeperDto.ShopkeeperId);
            if (existingShopkeeper == null)
            {
                throw new InvalidOperationException("Shopkeeper not found.");
            }

            existingShopkeeper.ShopName = shopkeeperDto.ShopName;
            existingShopkeeper.Location = shopkeeperDto.Location;
            existingShopkeeper.Address = shopkeeperDto.Address;

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