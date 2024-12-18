using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JeanStation.Models;
namespace JeanStation.Repository
{
    internal interface IUserRepository
    {
        
            bool SignUpUser(SignUpModel model);
            LoginModelObject Login(LoginModel login);
        
    }
}
