using AuthServer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuthServer.RestModels;
using IdentityServer4.Extensions;
using User = AuthServer.RestModels.User;

namespace AuthServer.Controllers
{
    [Route("api/User")]
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
           
            try
            {
                var userId = _userRepository.RegisterUser(user.Username, user.Password, user.UserType);
                return Ok(userId);
            }

            catch (InvalidOperationException)
            {
                Console.WriteLine("Username already exists");
                return BadRequest();
            }
            catch (Exception e)
            {
                Console.WriteLine($"Parameters are not valid ,Provide both username and password: {e.Message}");
                return BadRequest();
            }


        }

        [HttpDelete]
        public async Task<IActionResult> DeleteUser([FromHeader]string username)
        {
            try
            {
                var result = await _userRepository.DeleteUser(username);
                return Ok();
            }
            catch (InvalidOperationException )
            {
                Console.WriteLine("Username does not exists");
                return NotFound();
            }
            catch (Exception )
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
                    var result = await _userRepository.UpdateUser(username, user.Password,user.UserType);
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








