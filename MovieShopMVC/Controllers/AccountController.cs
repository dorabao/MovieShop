using ApplicationCore.Models;
using ApplicationCore.ServiceInterfaces;
using Microsoft.AspNetCore.Mvc;

namespace MovieShopMVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;
        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(UserRegisterRequestModel userRegisterRequestModel)
        {
            //need save the data in database (At first send the data to service
            //then it will convert the data into user entity and send it to user repository
            //then the data will be saved in user table)
            //will return to login page
            var user = _accountService.RegisterUser(userRegisterRequestModel);
            if (user == 0)
            {
                // email already exists
                return View();
            }
            return RedirectToAction("Login");
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginRequestModel loginRequestModel)
        {
            return View();
        }
    }
}
