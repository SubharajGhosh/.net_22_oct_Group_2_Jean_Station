using JeanStation.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JeanStation.Repository
{
    internal interface IUserRepository
    {
        User GetUserById(string userId);
        List<User> GetAllUsers();
        bool SignUpCustomer(string username, string password, string customerName, string email, string phoneNumber, string address);
        bool UpdateUser(User user);
        bool DeleteUser(string userId);
        bool ValidateUser(User user);
    }
}
