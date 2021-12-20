using ApplicationCore.ServiceInterfaces;
using Infrastructure.Repositories;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using MovieShopMVC.Models;
using System.Diagnostics;

namespace MovieShopMVC.Controllers
{
    public class HomeController : Controller
    {
        // C# readonly
        private readonly IMovieService _movieService;
        private readonly IGenreService _genreService;
        private readonly ILogger<HomeController> _logger;
        public HomeController(IMovieService movieService, IGenreService genreService, ILogger<HomeController> logger)
        {
            _movieService = movieService;
            _genreService = genreService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var movieCards = await _movieService.GetHighestGrossingMovies();
            return View(movieCards);
        }

        [HttpGet]
        public async Task<IActionResult> Privacy()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> TopMovies()
        {
            var genres = await _genreService.GetAllGenres();
            return View(genres);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public async Task<IActionResult> Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}