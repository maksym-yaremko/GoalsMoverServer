using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GoalsMover.DAL.Context;
using GoalsMover.BLL.IServices;
using GoalsMover.DTO.DTO;
using Microsoft.AspNetCore.Authorization;

namespace GoalsMover.Controllers
{
    [Authorize]
    [Route("api")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [Route("GetUsers")]
        [HttpGet]
        public async Task<ActionResult<UserDTO>> GetUsers()
        {
            var allUsers = await _userService.GetAll();
            return Ok(allUsers);
        }

        [Route("GetUser/{id}")]
        [HttpGet]
        public async Task<ActionResult<UserDTO>> GetUser(int id)
        {
            var user = await _userService.Get(id);

            return user;
        }

        [AllowAnonymous]
        [Route("Authenticate")]
        [HttpPost]
        public async Task<IActionResult> Authenticate([FromBody]UserDTO user)
        {
            var _user = await _userService.Authenticate(user.Email);

            if (_user == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(_user);
        }

        [AllowAnonymous]
        [Route("CreateUser")]
        [HttpPost]
        public async Task<IActionResult> PostUser(UserDTO user)     
        {
            await _userService.Create(user);
            return Ok();
        }
    }
}
