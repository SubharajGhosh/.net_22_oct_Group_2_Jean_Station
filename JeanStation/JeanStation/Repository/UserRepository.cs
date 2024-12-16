using JeanStation.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text.RegularExpressions;
using JeanStation.Models;
namespace JeanStation.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly JeanStationContext _context;

        public UserRepository()
        {
            _context = new JeanStationContext();
        }
        public string Login(string username, string password)
        {
            var user = _context.Users.FirstOrDefault(u => u.UserName == username && u.Password == password);
            if (user == null)
            {
                return "Invalid credentials"; // Invalid login
            }
            return user.Role;
        }

        public bool SignUpUser(SignUpModel model)
        {
            if (_context.Users.Any(u => u.UserName == model.UserName))
            {
                return false; // Username already exists
            }

            // Create user entry
            var user = new User
            {
                UserId = Guid.NewGuid().ToString(),
                UserName = model.UserName,
                Password = model.Password,  // Store the password as plain text for simplicity, ideally hash it
                Role = model.Role,
            };
            _context.Users.Add(user);

            // Create customer or shopkeeper based on the role
            if (model.Role == "Customer")
            {
                var customer = new Customer
                {
                    CustomerId = Guid.NewGuid().ToString(),
                    CustomerName = model.CustomerName,
                    Email = model.Email,
                    PhoneNumber = model.PhoneNumber,
                    Address = model.Address,
                    UserId = user.UserId
                };
                _context.Customers.Add(customer);
            }
            else if (model.Role == "Shopkeeper")
            {
                var shopkeeper = new Shopkeeper
                {
                    ShopkeeperId = Guid.NewGuid().ToString(),
                    Address = model.Address,
                    UserId = user.UserId
                };
                _context.Shopkeepers.Add(shopkeeper);
            }

            // Commit the transaction to save to the database
            _context.SaveChanges();
            return true;
        }

        }
}