using KOM.WebApi.Interfaces;
using KOM.WebApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KOM.WebApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody]AuthenticateModel model)
        {
            var user = _userService.Authenticate(model.Username, model.Password);

            if (user == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(user);
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public IActionResult Register([FromBody] RegisterModel model)
        {
            var user = _userService.Register(model);

            if (user == null)
                return BadRequest(new { message = "Could not register user" });

            return Ok(user);
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var users = _userService.GetAllUsers();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public IActionResult GetUserById(int id)
        {
            var user = _userService.GetUserById(id);
            return Ok(user);
        } 

        [HttpGet("{id}/rides")]
        public IActionResult GetRidesForUser(int id)
        {
            var rides = _userService.GetRidesByUserId(id);
            return Ok(rides);
        }

        [HttpPost("addRide")]
        public IActionResult AddRideForUser(AddRideModel addRideModel)
        {
            var addRide = _userService.AddRideByUserId(addRideModel);
            return Ok(addRide);
        }

    }
}
