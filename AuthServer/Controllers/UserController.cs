using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuthServer.Service;
using IdentityServer4.Extensions;
using User = AuthServer.Models.User;

namespace AuthServer.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserRepository _userRepository;
        public UserController(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }


        [HttpPost]
        public IActionResult RegisterUser([FromBody]User user)
        {
        
            var userId = _userRepository.RegisterUser(user.Username, user.Password, user.UserType);
            return Ok(userId);
        
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteUser([FromHeader]string username)
        {
            try
            {
                var result = await _userRepository.DeleteUser(username);
                return Ok();
            }
            catch (InvalidOperationException)
            {
                Console.WriteLine("Username does not exists");
                return NotFound();
            }
            catch (Exception)
            {
                Console.WriteLine("Provide some username");
                return BadRequest();
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateUser([FromHeader]string username, [FromBody]User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            if (username == null || user.Password.IsNullOrEmpty())
            {

                return BadRequest();
            }

            try
            {
                var result = await _userRepository.UpdateUser(username, user.Password, user.UserType);
                return Ok();
            }
            catch (InvalidOperationException)
            {
                Console.WriteLine("Username does not exist");
                return NotFound();
            }

        }
    }
}








