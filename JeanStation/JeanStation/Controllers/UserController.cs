using JeanStation.Models;
using JeanStation.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace JeanStation.Controllers
{
    [RoutePrefix("api/User")]
    public class UserController : ApiController
    {
       
            private readonly IUserRepository _userRepository;

            // Constructor to inject UserRepository
            public UserController()
            {
                _userRepository = new UserRepository();
            }

            // POST api/users/signup
            [HttpPost]
            [Route("signup")]
            public IHttpActionResult SignUp([FromBody] SignUpModel model)
            {
                if (model == null)
                {
                    return BadRequest("Invalid user data.");
                }

                // Sign up the user by calling UserRepository
                bool isSignUpSuccessful = _userRepository.SignUpUser(model);


                if (isSignUpSuccessful)
                {
                    return Ok("User registered successfully.");
                }
                else
                {
                    return BadRequest("Username already exists.");
                }
            }

            // POST api/users/login
            [HttpPost]
            [Route("login")]
            public IHttpActionResult Login([FromBody] LoginModel model)
            {
                if (model == null)
                {
                    return BadRequest("Invalid login data.");
                }

                // Call the login method from UserRepository
                LoginModelObject role = _userRepository.Login(model);

                

                return Ok(role); // Return the user's role (Customer or Shopkeeper)
            
        }
    }
}
