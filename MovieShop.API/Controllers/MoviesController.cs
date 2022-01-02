using ApplicationCore.ServiceInterfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MovieShop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly IMovieService _movieService;

        public MoviesController(IMovieService movieService)
        {
            _movieService = movieService;
        }

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> GetNewReleaseMovies()
        {
            var movies = await _movieService.GetNewReleaseMovies();
            if (!movies.Any())
            {
                return NotFound();
            }
            return Ok(movies);
        }

        [HttpGet]
        [Route("toprated")]
        public async Task<IActionResult> GetTopRatedMovies()
        {
            var movies = await _movieService.GetHighestRatedMovies();
            if (!movies.Any())
            {
                return NotFound();
            }
            return Ok(movies);
        }

        [HttpGet]
        [Route("toprevenue")]
        public async Task<IActionResult> GetTopRevenueMovies()
        {
            var movies = await _movieService.GetHighestGrossingMovies();
            return Ok(movies);
        }
     

        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> Details(int id)
        {
            var movie = await _movieService.GetMovieDetailsById(id);
            if (movie == null) return NotFound();
            return Ok(movie);
        }

        [HttpGet]
        [Route("genre/{genreId:int}")]
        public async Task<IActionResult> GetAllMoviesOfGenre(int genreId)
        {
            var movies = await _movieService.GetAllMoviesByGenreId(genreId);
            if (!movies.Any())
            {
                return NotFound();
            }
            return Ok(movies);
        }

        [HttpGet]
        [Route("{id:int}/reviews")]
        public async Task<IActionResult> GetAllReviewsOfMovie(int id)
        {
            var reviews = await _movieService.GetAllReviewsByMovieId(id);
            if (!reviews.Any())
            {
                return NotFound();
            }
            return Ok(reviews);
        }
    }
}
