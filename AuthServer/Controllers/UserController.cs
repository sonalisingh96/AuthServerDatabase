using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using AuthServer.ErrorHandling;
using AuthServer.Service;
using User = AuthServer.Models.User;

namespace AuthServer.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserRepository _userRepository;
        public UsersController(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpPost]
        public async Task<IActionResult> RegisterUser([FromBody]User user)
        {
            if (!ModelState.IsValid) throw new AppException(400, "Give Valid Details");
            var userId = await _userRepository.RegisterUser(user.Username, user.Password, user.UserType);
            return Ok(userId);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser([FromRoute]int id)
        {
            await _userRepository.DeleteUser(id);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser([FromRoute]int id, [FromBody]User user)
        {
            if (!ModelState.IsValid) throw new AppException(400, "Give Valid Details");
            await _userRepository.UpdateUser(user.Username, user.Password, user.UserType,id);
            return Ok();

        }
    }
}








