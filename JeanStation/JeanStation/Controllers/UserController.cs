using JeanStation.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using JeanStation.Entities;


namespace JeanStation.Controllers
{
    [RoutePrefix("api/user")]
    public class UserController : ApiController
    {
        private  IUserRepository _userRepository;

        // Constructor to inject IUserRepository
        public UserController()
        {
            _userRepository = new UserRepository();
        }

        [HttpGet]
        [Route("{userId}", Name = "GetUserById")]
        public IHttpActionResult GetUserById(string userId)
        {
            var user = _userRepository.GetUserById(userId);
            if (user == null)
            {
                return NotFound(); // Returns HTTP 404 if user is not found
            }
            return Ok(user); // Returns HTTP 200 with user data
        }
        [HttpGet]
        [Route("")]
        public IHttpActionResult GetAllUsers()
        {
            var users = _userRepository.GetAllUsers();
            return Ok(users); // Returns HTTP 200 with list of users
        }

        [HttpPost]
        [Route("")]
        public IHttpActionResult CreateUser([FromBody] User user)
        {
            if (user == null)
            {
                return BadRequest("User data is required."); // Returns HTTP 400 if data is missing
            }

            try
            {
                // Validate the user before attempting to create
                _userRepository.ValidateUser(user);

                bool result = _userRepository.CreateUser(user);
                if (result)
                {
                    return CreatedAtRoute("GetUserById", new { userId = user.UserId }, user); // Returns HTTP 201 (Created) along with user data
                }
                else
                {
                    return BadRequest("Error creating user."); // Returns HTTP 400 if creation fails
                }
            }
            catch (ArgumentException ex)
            {
                // Return a custom error message for validation issues (like password validation)
                return BadRequest(ex.Message); // Returns HTTP 400 with the specific error message
            }
            catch (Exception ex)
            {
                return InternalServerError(ex); // Returns HTTP 500 for internal errors
            }
        }

        [HttpPut]
        [Route("{userId}")]
        public IHttpActionResult UpdateUser(string userId, [FromBody] User user)
        {
            if (user == null || user.UserId != userId)
            {
                return BadRequest("User data is incorrect or mismatched."); // Returns HTTP 400 if user data is incorrect
            }

            try
            {
                bool result = _userRepository.UpdateUser(user);
                if (result)
                {
                    return Ok(user); // Returns HTTP 200 with updated user data
                }
                else
                {
                    return NotFound(); // Returns HTTP 404 if user not found
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex); // Returns HTTP 500 for internal errors
            }
        }
        [HttpDelete]
        [Route("{userId}")]
        public IHttpActionResult DeleteUser(string userId)
        {
            try
            {
                bool result = _userRepository.DeleteUser(userId);
                if (result)
                {
                    return StatusCode(System.Net.HttpStatusCode.NoContent); // Returns HTTP 204 for successful deletion
                }
                else
                {
                    return NotFound(); // Returns HTTP 404 if user not found
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex); // Returns HTTP 500 for internal errors
            }
        }
    }
}
