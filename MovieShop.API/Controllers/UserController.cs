using ApplicationCore.Models;
using ApplicationCore.ServiceInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace MovieShop.API.Controllers
{
    [Authorize]
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
        [Route("{id:int}/movie/{movieId:int}/favorite")]
        public async Task<IActionResult> CheckFavoriteMovie(int id, int movieId)
        {
            var favorites = await _userService.GetUserFavoriteMovies(id);
            if (!favorites.Any())
            {
                return NotFound();
            }
            foreach (var favorite in favorites)
            {
                if (favorite.Id == movieId)
                { 
                    return Ok(favorite);
                }
            }
            return NotFound();
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

        [Authorize]
        [HttpPost]
        [Route("favorite")]
        public async Task<IActionResult> AddFavoriteMovie([FromBody] MovieCardResponseModel model)
        {
            var userId = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var favoriteStatus = await _userService.AddFavoriteMovie(model, userId);
            if (favoriteStatus == false)
            {
                return BadRequest();
            }
            return Ok("Add Successfully");
        }

        [Authorize]
        [HttpPost]
        [Route("purchase")]
        public async Task<IActionResult> PurchaseMovie([FromBody] MovieDetailsResponseModel model)
        {
            var userId = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var purchaseStatus  = await _userService.PurchaseMovie(model, userId);
            if (purchaseStatus == false)
            {
                return BadRequest();
            }
            return Ok("Purchase Successfully");
        }

        [Authorize]
        [HttpPost]
        [Route("review")]
        public async Task<IActionResult> AddReview([FromBody] ReviewResponseModel model)
        {
            var userId = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var reviewStatus = await _userService.AddReview(model, userId);
            if (reviewStatus == false)
            {
                return BadRequest();
            }
            return Ok("Add Successfully");
        }


    }
}
