using ApplicationCore.Models;
using ApplicationCore.ServiceInterfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MovieShop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly IUserService _userService;

        public AccountController(IAccountService accountService, IUserService userService)
        {
            _accountService = accountService;
            _userService = userService;
        }

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> Register()
        {
            var userForm = await _accountService.GetRegisterForm();
            return Ok(userForm);
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var user = await _userService.GetUserDetails(id);
            if (user == null)
            {
                return NotFound("No User Found");
            }
            return Ok(user);
        }

        [HttpPost]
        [Route("login")]

        public async Task<IActionResult> Login([FromBody] LoginRequestModel model)
        {
            var user = await _accountService.ValidateUser(model);
            if (user == null)
            {
                return Unauthorized("wrong email/password");
            }
            // JWT AUthnetcion
            return Ok(user);
        }

        [HttpPost]
        [Route("")]
        public async Task<IActionResult> Register([FromBody] UserRegisterRequestModel model)
        { 
            var userId = await _accountService.RegisterUser(model);
            if (userId == 0)
            { 
                return BadRequest("Already a registed user, please login");
            }
            return Ok(userId);
        }

    }
}
