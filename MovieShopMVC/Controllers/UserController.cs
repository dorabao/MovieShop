using ApplicationCore.Models;
using ApplicationCore.ServiceInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace MovieShopMVC.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> Purchases()
        {
            // go to User Service and call User Repository and get the Movies Purchased by user who loged in
            var userId = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            // pass above user id to User Service
            var purchases = await _userService.GetUserPurchasedMovies(userId);
            return View(purchases);
        }

        [HttpGet]
        public async Task<IActionResult> Favorites()
        {
            var userId = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var favorites = await _userService.GetUserFavoriteMovies(userId);
            return View(favorites);
        }

        [HttpGet]
        public async Task<IActionResult> Profile()
        {
            var userId = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var userProfile = await _userService.GetUserDetails(userId);
            return View(userProfile);
        }

        [HttpGet]
        public async Task<IActionResult> EditProfile()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> EditProfile(UserDetailsModel userDetailsModel)
        {
            var userId = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var updatedProfile = await _userService.EditUserProfile(userDetailsModel, userId);
            if (updatedProfile == true)
            {
                return RedirectToAction("Profile");
            }
            return View();
        }
    }
}
