using JeanStation.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text.RegularExpressions;

namespace JeanStation.Repository
{
    public class UserRepository : IUserRepository
    {
        private  JeanStationContext _context;

        public UserRepository()
        {
            _context = new JeanStationContext();
        }
        public bool SignUpCustomer(string username, string password, string customerName, string email, string phoneNumber, string address)
        {
            // Ensure username is unique
            if (_context.Users.Any(u => u.UserName == username))
            {
                return false;
            }

            // Create user entry
            var user = new User
            {
                UserId = Guid.NewGuid().ToString(),
                UserName = username,
                Password = password,
                Role = "Customer" // Default role
            };
            _context.Users.Add(user);

            // Create customer entry
            var customer = new Customer
            {
                CustomerId = Guid.NewGuid().ToString(), // Generate unique CustomerId
                CustomerName = customerName,
                Email = email,
                PhoneNumber = phoneNumber,
                Address = address,
                UserId = user.UserId
            };
            _context.Customers.Add(customer);

            return true;
        }


        public bool DeleteUser(string userId)
        {
            var user = _context.Users.Find(userId);

            // If the user does not exist, throw an exception
            if (user == null)
            {
                throw new InvalidOperationException("User not found.");
            }
            else if(user.Role=="Customer")
            {
                _context.Users.Remove(user);

                // Save changes to the database to persist the deletion
                _context.SaveChanges();

            }
            // Remove the user from the Users DbSet
            

            // Return true to indicate the user was successfully deleted
            return true;
        }

        public List<User> GetAllUsers()
        {
            return _context.Users.ToList();
        }

        public User GetUserById(string userId)
        {
            return _context.Users.FirstOrDefault(u => u.UserId == userId);
        }

        public bool UpdateUser(User user)
        {
            var existingUser = _context.Users.FirstOrDefault(u => u.UserId == user.UserId);

            // If the user does not exist, throw an exception
            if (existingUser == null)
            {
                throw new InvalidOperationException("User not found.");
            }

            // Update the properties of the existing user with the values from the provided user object
            //existingUser.UserName = user.UserName;
            existingUser.Password = user.Password;
            //existingUser.Role = user.Role;

            // Optionally, validate the updated user
            ValidateUser(existingUser);

            // Save changes to the database to persist the updates
            _context.SaveChanges();

            // Return true to indicate the user was successfully updated
            return true;
        }

        public bool ValidateUser(User user)
        {
            if (user == null)
            {
                throw new ArgumentException("User cannot be null.");
            }

            if (string.IsNullOrEmpty(user.UserId) || string.IsNullOrEmpty(user.UserName) || string.IsNullOrEmpty(user.Password))
            {
                throw new ArgumentException("UserId, UserName, and Password are required.");
            }

            // Validate password (must contain uppercase, lowercase, numeric, and special characters)
            if (!Regex.IsMatch(user.Password, @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{6,}$"))
            {
                throw new ArgumentException("Password must contain at least one lowercase letter, one uppercase letter, one number, and one special character.");
            }

            return true;
        }
        public string Login(string username, string password)
        {
            var user = _context.Users.FirstOrDefault(u => u.UserName == username && u.Password == password);

            if (user == null)
            {
                return "Invalid credentials";
            }

            return user.Role; // Return the role (Customer or Shopkeeper)
        }

        public bool UserExists(string username)
        {
            return _context.Users.Any(u => u.UserName == username);
        }

    }
}