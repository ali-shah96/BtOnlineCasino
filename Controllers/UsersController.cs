using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineCasino.DatabaseContext;
using OnlineCasino.DatabaseContext.Entities;
using OnlineCasino.Logic.Interfaces;
using OnlineCasino.Models.Response;
using Action = OnlineCasino.Models.Response.Action;

namespace OnlineCasino.Controllers
{
    [Authorize]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class UsersController : ControllerBase
    {
        private readonly OnlineCasinoContext _context;
        private readonly IUsersService _userService;

        public UsersController(OnlineCasinoContext context, IUsersService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Users>>> GetUsers([FromQuery] int page, [FromQuery] int pageSize)
        {
            var users = await _userService.GetUsers(page, pageSize);

            var response = new
            {
                Metadata = users.metadata,
                Games = users.users
            };

            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Users>> GetUser(int id)
        {
            var user = await _userService.GetUser(id);
            if (user == null)
                return NotFound();
            return Ok(user);
        }

        [HttpPost]
        public async Task<ActionResult<Users>> PostUser([FromBody] Users user)
        {
            var createdUser = await _userService.CreateUser(user);
            return Ok(new ReponseModel
            {
                Message = "Sucess",
                Action = Action.Created
            });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, [FromBody] Users user)
        {
            var result = await _userService.UpdateUser(id, user);
            if (!result)
                return BadRequest();
            return Ok(new ReponseModel
            {
                Message = "Sucess",
                Action = Action.Updated
            });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var result = await _userService.DeleteUser(id);
            if (!result)
                return NotFound();
            return Ok(new ReponseModel
            {
                Message = "Sucess",
                Action = Action.Deleted
            });
        }
    }
}
