using ApplicationCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.ServiceInterfaces
{
    public interface IUserService
    {
        Task<IEnumerable<MovieCardResponseModel>> GetUserPurchasedMovies(int id);
        Task<IEnumerable<MovieCardResponseModel>> GetUserFavoriteMovies(int id);
        Task<UserDetailsModel> GetUserDetails(int id);
        Task<bool> EditUserProfile(UserDetailsModel userDetailsModel, int id);
        Task<IEnumerable<ReviewResponseModel>> GetAllReviewsByUserId(int id);
        Task<bool> AddFavoriteMovie(MovieCardResponseModel model, int userId);
        Task<bool> PurchaseMovie(MovieDetailsResponseModel model, int userId);
        Task<bool> AddReview(ReviewResponseModel model, int userId);
        Task<bool> DeleteFavoriteMovie(MovieCardResponseModel model, int userId);
    }
}
