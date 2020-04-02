using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GoalsMover.DAL.Context;
using GoalsMover.BLL.IServices;
using GoalsMover.DTO.DTO;

namespace GoalsMover.Controllers
{
    [Route("api")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly GoalsMoverDbContext _context;

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

        [Route("CreateUser")]
        [HttpPost]
        public async Task<IActionResult> PostUser(UserDTO user)     
        {
            await _userService.Create(user);
            return Ok();
        }
    }
}
