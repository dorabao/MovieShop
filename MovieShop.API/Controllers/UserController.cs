using ApplicationCore.Models;
using ApplicationCore.ServiceInterfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace MovieShop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        [Route("{id:int}/favorites")]
        public async Task<IActionResult> GetFavoriteMovies(int id)
        {
            var favorites = await _userService.GetUserFavoriteMovies(id);
            if (!favorites.Any())
            {
                return NotFound();
            }
            return Ok(favorites);
        }

        [HttpGet]
        [Route("{id:int}/purchases")]
        public async Task<IActionResult> GetPurchasedMovies(int id)
        {
            var purchases = await _userService.GetUserPurchasedMovies(id);
            if (!purchases.Any())
            {
                return NotFound();
            }
            return Ok(purchases);
        }

        [HttpGet]
        [Route("{id:int}/reviews")]
        public async Task<IActionResult> GetAllReviewsOfUser(int id)
        {
            var reviews = await _userService.GetAllReviewsByUserId(id);
            if (!reviews.Any())
            {
                return NotFound();
            }
            return Ok(reviews);
        }

        [HttpPost]
        [Route("favorite")]
        public async Task<IActionResult> AddFavoriteMovie([FromBody] MovieCardResponseModel model)
        {
            var userId = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var favoriteId = await _userService.AddFavoriteMovie(model, userId);
            if (favoriteId == 0)
            {
                return BadRequest("Please try again later");
            }
            return Ok(favoriteId);
        }
    }
}
