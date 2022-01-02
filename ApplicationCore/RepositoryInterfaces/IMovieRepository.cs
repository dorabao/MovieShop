using ApplicationCore.Entities;
using ApplicationCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.RepositoryInterfaces
{
    public interface IMovieRepository:IRepository<Movie>
    {
        Task<IEnumerable<Movie>> Get30HighestGrossingMovies();
        Task<IEnumerable<Movie>> Get30HighestRatedMovies();
        Task<IEnumerable<Movie>> Get100NewReleaseMovies();
        Task<IEnumerable<MovieGenre>> GetMoviesByGenreId(int genreId);
        Task<IEnumerable<Review>> GetAllReviewsByMovieId(int id);
        Task<Movie> AddNewMovie(Movie movie);
        Task<Movie> UpdateMovie(MovieDetailsResponseModel model, string admin);
    }
}
