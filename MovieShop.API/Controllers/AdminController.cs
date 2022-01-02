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
    public class AdminController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly IMovieService _movieService;
        public AdminController(IAccountService accountService, IMovieService movieService)
        {
            _accountService = accountService;
            _movieService = movieService;
        }

        [HttpGet]
        [Route("purchases")]
        public async Task<IActionResult> GetAllUsersPurchases()
        {
            var purchases = await _accountService.GetAllUsersPurchasedMovies();
            if (!purchases.Any())
            {
                return NotFound();
            }
            return Ok(purchases);
        }

        [HttpPost]
        [Route("movie")]
        public async Task<IActionResult> AddNewMovie([FromBody] MovieDetailsResponseModel model)
        {
            var admin = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var newMovieId = await _movieService.AddNewMovie(model, admin);
            if (newMovieId == -1)
            {
                return BadRequest("Can't add the movie");
            }
            return Ok(newMovieId);
        }

        [HttpPut]
        [Route("movie")]
        public async Task<IActionResult> UpdateMovie([FromBody] MovieDetailsResponseModel model)
        {
            var admin = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var updateStatus = await _movieService.UpdateMovie(model, admin);
            if (updateStatus == false)
            {
                return BadRequest("Can't edit the movie");
            }
            return Ok();
        }
    }
}
