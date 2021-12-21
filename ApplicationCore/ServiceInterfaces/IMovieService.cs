using ApplicationCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.ServiceInterfaces
{
    public interface IMovieService
    {
        // Expose the methods that are required by the client/views
        //always return model
        Task<IEnumerable<MovieCardResponseModel>> GetHighestGrossingMovies();
        Task<IEnumerable<MovieCardResponseModel>> GetHighestRatedMovies();
        Task<IEnumerable<MovieCardResponseModel>> GetNewReleaseMovies();
        Task<MovieDetailsResponseModel> GetMovieDetailsById(int id);
        Task<IEnumerable<MovieCardResponseModel>> GetAllMoviesByGenreId(int genreId);
        Task<IEnumerable<ReviewResponseModel>> GetAllReviewsByMovieId(int id);
    }
}
