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

        Task<IEnumerable<Movie>> GetUserPurchasedMovies(int id);

        Task<IEnumerable<Movie>> GetUserFavoriteMovies(int id);

        Task<User> GetUserDetails(int id);

        Task<bool> EditUserProfile(UserDetailsModel userDetailsModel, int id);
    }
}
