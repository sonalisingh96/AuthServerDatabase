using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuthServer.Database;
using AuthServer.Service;
using IdentityServer4.Extensions;
using User = AuthServer.Models.User;

namespace AuthServer.Controllers
{
    //TBD: the default route should be ending with users not user
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
        public async Task<IActionResult> RegisterUser([FromBody]User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            //TBD: do the model validation here
            var userId = await _userRepository.RegisterUser(user.Username, user.Password, user.UserType);
            return Ok(userId);
        }

        [HttpDelete]
        //TBD : userName should not be from header. this should be from path or query string //check the standard
        //TBD: delete should be based on userid
        public async Task<IActionResult> DeleteUser([FromHeader]string username)
        {
            await _userRepository.DeleteUser(username);
            return Ok();
        }

        [HttpPut]
        //TBD: Update should be part based on user id
        //TBD: the id should come from path and not header
        public async Task<IActionResult> UpdateUser([FromHeader]string username, [FromBody]User user)
        {
            //TBD: return error based on model error
            if (!ModelState.IsValid) return BadRequest();

            await _userRepository.UpdateUser(username, user.Password, user.UserType);
            return Ok();

        }
    }
}








