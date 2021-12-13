using ApplicationCore.Entities;
using ApplicationCore.RepositoryInterfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class MovieRepository : Repository<Movie>, IMovieRepository
    {
        public MovieRepository(MovieShopDbContext dbContext) : base(dbContext)
        {

        }
        public async Task<IEnumerable<Movie>> Get30HighestGrossingMovies()
        {
            // we need to go to database and get the movies using ADO.NET Dapper or EF Core
            // access the dbcontext object and dbset of movies object to query the movies table
       
            var movies = await _dbContext.Movies.OrderByDescending(m => m.Revenue).Take(30).ToListAsync();
            return movies;
        }

        public async override Task<Movie> GetById(int id)
        {
            //call the movie dbset and join other info from genres, trailers, cast sbsets.
            //use navigation properties
            //use Include methos in EF to help us 
            var movieDetails = await _dbContext.Movies.Include(m => m.CastsOfMovie).ThenInclude(m => m.Cast)
                .Include(m => m.GenresOfMovie).ThenInclude(m => m.Genre).Include(m => m.Trailers)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (movieDetails == null) return null;

            var rating = await _dbContext.Reviews.Where(r => r.MovieId == id).DefaultIfEmpty()
                .AverageAsync(r => r == null ? 0 : r.Rating);
            movieDetails.Rating = rating;
            return movieDetails;
        }
    }
}
