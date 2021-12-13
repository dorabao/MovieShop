using ApplicationCore.ServiceInterfaces;
using Microsoft.AspNetCore.Mvc;

namespace MovieShopMVC.Controllers
{
    public class MovieController : Controller
    {
        private readonly IMovieService _movieService;
        public MovieController(IMovieService movieService)
        { 
            _movieService = movieService;   
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            //call the MovieService to get the movie details (use DI)
            var movieDetails = await _movieService.GetMovieDetailsById(id);
            return View(movieDetails);
        }
    }
}
