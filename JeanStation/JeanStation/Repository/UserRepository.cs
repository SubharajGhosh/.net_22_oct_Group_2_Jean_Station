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
        public bool CreateUser(User user)
        {
            ValidateUser(user);
            var existingUser = _context.Users.FirstOrDefault(u => u.UserId == user.UserId);
            if (existingUser != null)
            {
                throw new InvalidOperationException("A user with the same UserId already exists.");
            }
            _context.Users.Add(user);

            _context.SaveChanges();

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

            // Remove the user from the Users DbSet
            _context.Users.Remove(user);

            // Save changes to the database to persist the deletion
            _context.SaveChanges();

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
            existingUser.UserName = user.UserName;
            existingUser.Password = user.Password;
            existingUser.Role = user.Role;

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


    }
}