using ApplicationCore.Entities;
using ApplicationCore.Models;
using ApplicationCore.RepositoryInterfaces;
using ApplicationCore.ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMovieRepository _movieRepository;

        public UserService(IUserRepository userRepository, IMovieRepository movieRepository)
        {
            _userRepository = userRepository;
            _movieRepository = movieRepository;
        }

        public async Task<bool> EditUserProfile(UserDetailsModel userDetailsModel, int id)
        {
            bool editStatus = await _userRepository.EditUserProfile(userDetailsModel, id);
            return editStatus;
        }

        public async Task<IEnumerable<MovieCardResponseModel>> GetUserFavoriteMovies(int id)
        {
            var favoriteMovies = await _userRepository.GetUserFavoriteMovies(id);
            var favoriteMovieCards = new List<MovieCardResponseModel>();
            foreach (var favoriteMovie in favoriteMovies)
            {
                favoriteMovieCards.Add(
                    new MovieCardResponseModel { Id = favoriteMovie.Movie.Id, PosterUrl = favoriteMovie.Movie.PosterUrl, Title = favoriteMovie.Movie.Title }
                );
            }

            return favoriteMovieCards;
        }
  
        public async Task<IEnumerable<MovieCardResponseModel>> GetUserPurchasedMovies(int id)
        {
            var purchasedMovies = await _userRepository.GetUserPurchasedMovies(id);
            var purchasedMovieCards = new List<MovieCardResponseModel>();
            foreach (var purchasedMovie in purchasedMovies)
            {
                purchasedMovieCards.Add(
                    new MovieCardResponseModel { Id = purchasedMovie.Movie.Id, PosterUrl = purchasedMovie.Movie.PosterUrl, Title = purchasedMovie.Movie.Title }
                );
            }

            return purchasedMovieCards;
        }

        public async Task<UserDetailsModel> GetUserDetails(int id)
        {
            var userDetail = await _userRepository.GetUserDetails(id);
            var userDetails = new UserDetailsModel
            {
                FirstName = userDetail.FirstName,
                LastName = userDetail.LastName,
                DateOfBirth = userDetail.DateOfBirth
            };
            return userDetails;
        }

        public async Task<IEnumerable<ReviewResponseModel>> GetAllReviewsByUserId(int id)
        {
            var reviews = await _userRepository.GetAllReviewsByUserId(id);
            var reviewCards = new List<ReviewResponseModel>();
            foreach (var review in reviews)
            {
                reviewCards.Add(
                    new ReviewResponseModel { MovieId = review.MovieId, UserId = review.UserId, Rating = review.Rating, ReviewText = review.ReviewText }
                );
            }
            return reviewCards;
        }

        public async Task<bool> AddFavoriteMovie(MovieCardResponseModel model, int userId)
        {
            var allUserFavrites = await _userRepository.GetUserFavoriteMovies(userId);
            foreach (var favorite in allUserFavrites)
            {
                if (favorite.MovieId == model.Id)
                {
                    return false;
                }
            }
            var createdFavorite = new Favorite
            {
                MovieId = model.Id,
                UserId = userId,
            };
            var addedStatus = await _userRepository.AddFavorite(createdFavorite);
            if (addedStatus != null) return true;
            return false;
        }
        public async Task<bool> DeleteFavoriteMovie(MovieCardResponseModel model, int userId)
        {
            var allUserFavrites = await _userRepository.GetUserFavoriteMovies(userId);
            foreach (var favorite in allUserFavrites)
            {
                if (favorite.MovieId == model.Id)
                {
                    var deleteStatus = await _userRepository.DeleteFavorite(favorite);
                    return true;
                }
            }
            return false;
        }


        public async Task<bool> PurchaseMovie(MovieDetailsResponseModel model, int userId)
        { 
            var allUserPurchased  =await _userRepository.GetUserPurchasedMovies(userId);
            foreach (var purchasedMovie in allUserPurchased)
            {
                if (purchasedMovie.MovieId == model.Id)
                {
                    return false;
                }
            }
            var createdPurchase = new Purchase
            {
                UserId = userId,
                PurchaseNumber = System.Guid.NewGuid(),
                TotalPrice = (decimal) model.Price,
                PurchaseDateTime = DateTime.Today,
                MovieId = model.Id,
            };
            var addedStatus = await _userRepository.PurchaseMovie(createdPurchase);
            if (addedStatus != null) return true;
            return false;
        }

        public async Task<bool> AddReview(ReviewResponseModel model, int userId)
        {
            var allUserReviews = await _userRepository.GetAllReviewsByUserId(userId);
            foreach (var review in allUserReviews)
            {
                if (review.MovieId == model.MovieId)
                {
                    return false;
                }
            }
            var createdReview = new Review
            {
                MovieId = model.MovieId,
                UserId = userId,
                Rating = model.Rating,
                ReviewText = model.ReviewText,
            };
            var addedStatus = await _userRepository.AddReview(createdReview);
            if (addedStatus != null) return true;
            return false;
        }

    }
}
