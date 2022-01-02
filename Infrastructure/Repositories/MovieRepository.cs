using ApplicationCore.Entities;
using ApplicationCore.Models;
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

        public async Task<IEnumerable<Movie>> Get30HighestRatedMovies()
        {
            var movies = await _dbContext.Movies.OrderByDescending(m => m.Rating).Take(30).ToListAsync();
            return movies;
        }

        public async Task<IEnumerable<Movie>> Get100NewReleaseMovies()
        {
            var movies = await _dbContext.Movies.OrderByDescending(m => m.ReleaseDate).Take(100).ToListAsync();
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

        public async Task<IEnumerable<MovieGenre>> GetMoviesByGenreId(int genreId)
        {
            var movies = await _dbContext.MovieGenres.Include(mg => mg.Movie).Where(mg => mg.GenreId == genreId).ToListAsync();
            if (movies == null) return null;
            return movies;
        }

        public async Task<IEnumerable<Review>> GetAllReviewsByMovieId(int id)
        {
            var reviews = await _dbContext.Reviews.Include(r => r.Movie).Where(r => r.MovieId == id).ToListAsync();
            if (reviews == null) return null;
            return reviews;
        }

        public async Task<Movie> AddNewMovie(Movie movie)
        { 
            var createdMovie = _dbContext.Movies.Add(movie);
            await _dbContext.SaveChangesAsync();
            return createdMovie.Entity;
        }

        public async Task<Movie> UpdateMovie(MovieDetailsResponseModel model, string admin)
        {
            var targetMovie = await _dbContext.Movies.SingleOrDefaultAsync(m => m.Id == model.Id);
            if (targetMovie != null)
            {
                targetMovie.Title = model.Title;
                targetMovie.Overview = model.Overview;
                targetMovie.Tagline = model.Tagline;
                targetMovie.Budget = model.Budget;
                targetMovie.Revenue = model.Revenue;
                targetMovie.ImdbUrl = model.ImdbUrl;
                targetMovie.TmdbUrl = model.TmdbUrl;
                targetMovie.PosterUrl = model.PosterUrl;
                targetMovie.BackdropUrl = model.BackdropUrl;
                targetMovie.OriginalLanguage = model.OriginalLanguage;
                targetMovie.ReleaseDate = model.ReleaseDate;
                targetMovie.RunTime = model.RunTime;
                targetMovie.Price = model.Price;
                targetMovie.UpdatedDate = DateTime.Today;
                targetMovie.UpdatedBy = admin;
            }
            await _dbContext.SaveChangesAsync();
            return targetMovie;
        }
    }
}
