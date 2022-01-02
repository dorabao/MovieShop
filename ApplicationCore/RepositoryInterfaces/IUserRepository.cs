using ApplicationCore.Entities;
using ApplicationCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.RepositoryInterfaces
{
    public interface IUserRepository :  IRepository<User>
    {
        Task<User> GetUserByEmail(string email);

        Task<IEnumerable<Purchase>> GetUserPurchasedMovies(int id);

        Task<IEnumerable<Favorite>> GetUserFavoriteMovies(int id);

        Task<User> GetUserDetails(int id);

        Task<bool> EditUserProfile(UserDetailsModel userDetailsModel, int id);
        Task<IEnumerable<Review>> GetAllReviewsByUserId(int id);
        Task<Favorite> AddFavorite(Favorite favorite);
        Task<Purchase> PurchaseMovie(Purchase purchase);
        Task<Review> AddReview(Review review);
        Task<Favorite> DeleteFavorite(Favorite favorite);
        Task<Review> UpdateReview(ReviewResponseModel model);
        Task<Purchase> DeletePurchase(Purchase purchase);
        Task<IEnumerable<PurchaseTotalResponseModel>> GetAllUsersPurchasedMovies();
    }
}
