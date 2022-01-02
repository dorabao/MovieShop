using ApplicationCore.Models;
using ApplicationCore.ServiceInterfaces;
using Microsoft.AspNetCore.Authorization;
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
            var favoriteStatus = await _userService.AddFavoriteMovie(model, userId);
            if (favoriteStatus == false)
            {
                return BadRequest("Can't add this movie");
            }
            return Ok("Add Successfully");
        }

        [HttpPost]
        [Route("purchase")]
        public async Task<IActionResult> PurchaseMovie([FromBody] MovieDetailsResponseModel model)
        {
            var userId = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var purchaseStatus  = await _userService.PurchaseMovie(model, userId);
            if (purchaseStatus == false)
            {
                return BadRequest("Can't purchase this movie");
            }
            return Ok("Purchase Successfully");
        }

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

        [HttpPost]
        [Route("unfavorite")]
        public async Task<IActionResult> DeleteFavoriteMovie([FromBody] MovieCardResponseModel model)
        {
            var userId = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var unfavoriteStatus = await _userService.DeleteFavoriteMovie(model, userId);
            if (unfavoriteStatus == true)
            {
                return Ok("Unfavorite Successfully");
            }
            return BadRequest(); ;
        }

        [HttpPut]
        [Route("review")]
        public async Task<IActionResult> UpdateReview([FromBody] ReviewResponseModel model)
        {
            var updatedReview = await _userService.UpdateReview(model);
            if (updatedReview == null)
            {
                return NotFound();
            }
            return Ok("Updated Successfully");
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

        [HttpDelete]
        [Route("{userId:int}/movie/{movieId:int}")]
        public async Task<IActionResult> DeleteMovie(int userId, int movieId)
        {
            var deletedStatus = await _userService.DeleteMovie(userId, movieId);
            if (deletedStatus == false)
            {
                return NotFound();
            }
            return Ok("Deleted Successfully");
        }
    }
}
