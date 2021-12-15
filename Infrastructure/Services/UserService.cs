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

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<bool> EditUserProfile(UserDetailsModel userDetailsModel, int id)
        {
            bool editStatus = await _userRepository.EditUserProfile(userDetailsModel, id);
            return editStatus;
        }

        public async Task<List<MovieCardResponseModel>> GetUserFavoriteMovies(int id)
        {
            var favoriteMovies = await _userRepository.GetUserFavoriteMovies(id);
            var favoriteMovieCards = new List<MovieCardResponseModel>();
            foreach (var movie in favoriteMovieCards)
            {
                favoriteMovieCards.Add(
                    new MovieCardResponseModel { Id = movie.Id, PosterUrl = movie.PosterUrl, Title = movie.Title }
                );
            }

            return favoriteMovieCards;
        }
  
        public async Task<List<MovieCardResponseModel>> GetUserPurchasedMovies(int id)
        {
            var purchasedMovies = await _userRepository.GetUserPurchasedMovies(id);
            var purchasedMovieCards = new List<MovieCardResponseModel>();
            foreach (var movie in purchasedMovies)
            {
                purchasedMovieCards.Add(
                    new MovieCardResponseModel { Id = movie.Id, PosterUrl = movie.PosterUrl, Title = movie.Title }
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
    }
}
