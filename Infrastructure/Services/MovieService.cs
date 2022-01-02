using ApplicationCore.Entities;
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

        public async Task<IEnumerable<MovieCardResponseModel>> GetNewReleaseMovies()
        {
            var movies = await _movieRepository.Get100NewReleaseMovies();
            var movieCards = new List<MovieCardResponseModel>();
            foreach (var movie in movies)
            {
                movieCards.Add(
                    new MovieCardResponseModel { Id = movie.Id, PosterUrl = movie.PosterUrl, Title = movie.Title }
                );
            }
            return movieCards;
        }

        public async Task<IEnumerable<MovieCardResponseModel>> GetHighestGrossingMovies()
        {
            // call my MovieRepository and get the data
            var movies = await _movieRepository.Get30HighestGrossingMovies();
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

        public async Task<IEnumerable<MovieCardResponseModel>> GetHighestRatedMovies()
        {
            var movies = await _movieRepository.Get30HighestRatedMovies();
            var movieCards = new List<MovieCardResponseModel>();
            foreach (var movie in movies)
            {
                movieCards.Add(
                    new MovieCardResponseModel { Id = movie.Id, PosterUrl = movie.PosterUrl, Title = movie.Title }
                );
            }
            return movieCards;
        }

        public async Task<IEnumerable<MovieCardResponseModel>> GetAllMoviesByGenreId(int genreId)
        {
            var movies = await _movieRepository.GetMoviesByGenreId(genreId);
            var movieCards = new List<MovieCardResponseModel>();
            foreach (var movie in movies)
            {
                movieCards.Add(
                    new MovieCardResponseModel { Id = movie.Movie.Id, PosterUrl = movie.Movie.PosterUrl, Title = movie.Movie.Title }
                );
            }
            return movieCards;
        }

        public async Task<IEnumerable<ReviewResponseModel>> GetAllReviewsByMovieId(int id)
        {
            var reviews = await _movieRepository.GetAllReviewsByMovieId(id);
            var reviewCards = new List<ReviewResponseModel>();
            foreach (var review in reviews)
            {
                reviewCards.Add(
                    new ReviewResponseModel {MovieId = review.MovieId, UserId = review.UserId, Rating = review.Rating, ReviewText = review.ReviewText }
                );
            }
            return reviewCards;
        }

        public async Task<MovieDetailsResponseModel> GetMovieDetailsById(int id)
        {
            var movie = await _movieRepository.GetById(id);

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
                    Price = movie.Price,
                    Budget = movie.Budget,
                    Revenue = movie.Revenue,
                    ReleaseDate = movie.ReleaseDate,
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

        public async Task<int> AddNewMovie(MovieDetailsResponseModel model, string admin)
        {
            var newMovie = new Movie
            {
                Title = model.Title,
                Overview = model.Overview,
                Tagline = model.Tagline,
                Budget = model.Budget,
                Revenue = model.Revenue,
                ImdbUrl = model.ImdbUrl,
                TmdbUrl = model.TmdbUrl,
                PosterUrl = model.PosterUrl,
                BackdropUrl = model.BackdropUrl,
                OriginalLanguage = model.OriginalLanguage,
                ReleaseDate = model.ReleaseDate,
                RunTime = model.RunTime,
                Price = model.Price,
                CreatedDate = DateTime.Today,
                UpdatedDate = DateTime.Today,
                UpdatedBy = admin,
                CreatedBy = admin
            };
            var addedMovie = await _movieRepository.AddNewMovie(newMovie);
            return addedMovie != null ? addedMovie.Id : -1;
        }

        public async Task<bool> UpdateMovie(MovieDetailsResponseModel model, string admin)
        {
            var movieId = model.Id;
            var targetMovie = await _movieRepository.GetById(movieId);
            if (targetMovie != null)
            {
                await _movieRepository.UpdateMovie(model, admin);
                return true;
            }
            return false;
        }
    }
}
