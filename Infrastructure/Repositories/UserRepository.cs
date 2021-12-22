using ApplicationCore.Entities;
using ApplicationCore.RepositoryInterfaces;
using Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ApplicationCore.Models;

namespace Infrastructure.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(MovieShopDbContext dbContext) : base(dbContext)
        {

        }
        public async Task<User> GetUserByEmail(string email)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.Email == email);
            return user;
        }

        public async Task<IEnumerable<Purchase>> GetUserPurchasedMovies(int id)
        {
            var purchasedMovies = await _dbContext.Purchases.Include(p => p.Movie).Where(p => p.UserId == id).ToListAsync();
            if (purchasedMovies == null) return null;

            return purchasedMovies;
        }

        public async Task<IEnumerable<Favorite>> GetUserFavoriteMovies(int id)
        {
            var favoriteMovies = await _dbContext.Favorites.Include(f => f.Movie).Where(f => f.UserId == id).ToListAsync();
            if (favoriteMovies == null) return null;
            return favoriteMovies;
        }

        public async Task<User> GetUserDetails(int id)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == id);
            return user;
        }
        public async Task<bool> EditUserProfile(UserDetailsModel userDetailsModel, int id)
        {
            var editedUser = await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == id);
            bool editable = false;

            if (editedUser.FirstName != userDetailsModel.FirstName)
            {
                editable = true;
                editedUser.FirstName = userDetailsModel.FirstName;
            }

            if (editedUser.LastName != userDetailsModel.LastName)
            {
                editable = true;
                editedUser.LastName = userDetailsModel.LastName;
            }

            if (editedUser.DateOfBirth.Equals(userDetailsModel.DateOfBirth))
            {
                editable = true;
                editedUser.DateOfBirth = (DateTime)userDetailsModel.DateOfBirth;
            }
            _dbContext.SaveChanges();
            return editable;
        }

        public async Task<IEnumerable<Review>> GetAllReviewsByUserId(int id)
        {
            var reviews = await _dbContext.Reviews.Include(r => r.User).Where(r => r.UserId == id).ToListAsync();
            if (reviews == null) return null;
            return reviews;
        }

        public async Task<Favorite> AddFavorite(Favorite favorite)
        {
            var addedFavorite = await _dbContext.Favorites.AddAsync(favorite);
            _dbContext.SaveChanges();
            return addedFavorite.Entity;
        }

        public async Task<Favorite> DeleteFavorite(Favorite favorite)
        {
            var addedFavorite = _dbContext.Favorites.Remove(favorite);
            _dbContext.SaveChanges();
            return addedFavorite.Entity;
        }

        public async Task<Purchase> PurchaseMovie(Purchase purchase)
        {
            var addedPurchase = await _dbContext.Purchases.AddAsync(purchase);
            _dbContext.SaveChanges();
            return addedPurchase.Entity;
        }

        public async Task<Review> AddReview(Review review)
        {
            var addedReview = await _dbContext.Reviews.AddAsync(review);
            _dbContext.SaveChanges();
            return addedReview.Entity;
        }
    }
}
