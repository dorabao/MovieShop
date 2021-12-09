using ApplicationCore.Models;
using ApplicationCore.RepositoryInterfaces;
using ApplicationCore.ServiceInterfaces;
using Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class MovieService : IMovieService
    {
        private readonly IMovieRepository _movieRepository;
        public MovieService(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }

        public IEnumerable<MovieCardResponseModel> GetHighestGrossingMovies()
        {
            // call my MovieRepository and get the data
            var movies = _movieRepository.Get30HighestGrossingMovies();
            // 3rd party Automapper from Nuget
            var movieCards = new List<MovieCardResponseModel>();
            foreach (var movie in movies)
            {
                movieCards.Add(
                    new MovieCardResponseModel { Id = movie.Id, PosterUrl = movie.PosterUrl, Title = movie.Title }
                );
            }

            return movieCards;
        }

        public MovieDetailsResponseModel GetMovieDetailsById(int id)
        {
            var movie = _movieRepository.GetById(id);

            //map movie entity into MovieDetailsModel
            //use automapper that can ne used for mapping one object to another object

            var movieDetails = new MovieDetailsResponseModel
            {
                    Id = movie.Id,
                    PosterUrl = movie.PosterUrl,
                    Title = movie.Title,
                    OriginalLanguage = movie.OriginalLanguage,
                    Overview = movie.Overview,
                    Rating = movie.Rating,
                    RunTime = movie.RunTime,
                    Tagline = movie.Tagline,
                    BackdropUrl = movie.BackdropUrl,
                    TmdbUrl = movie.TmdbUrl,
                    ImdbUrl = movie.ImdbUrl,
                    Price = movie.Price
            };

            foreach (var movieCast in movie.CastsOfMovie)
            {
                movieDetails.Casts.Add(new CastResponseModel
                {
                    Id = movieCast.CastId,
                    Character = movieCast.Character,
                    Name = movieCast.Cast.Name,
                    PosterUrl = movieCast.Cast.ProfilePath
                });
            }

            foreach (var trailer in movie.Trailers)
            {
                movieDetails.Trailers.Add(new TrailerResponseModel
                {
                    Id = trailer.Id,
                    MovieId = trailer.MovieId,
                    Name = trailer.Name,
                    TrailerUrl = trailer.TrailerUrl
                });
            }

            foreach (var movieGenre in movie.GenresOfMovie)
            {
                movieDetails.Genres.Add(new GenreModel 
                {
                    Id = movieGenre.GenreId,
                    Name = movieGenre.Genre.Name,
                });
            }
            return movieDetails;
        }
    }
}
