using ApplicationCore.Entities;
using ApplicationCore.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class MovieRepository : Repository<Movie>, IMovieRepository
    {
        public IEnumerable<Movie> Get30HighestGrossingMovies()
        {
            // we need to go to database and get the movies using ADO.NET Dapper or EF Core
            // 
            
            
            var movies = new List<Movie> {
             new Movie { Id=1, Title = "Inception", Budget= 160000000, OriginalLanguage="en", PosterUrl="https://image.tmdb.org/t/p/w342//9gk7adHYeDvHkCSEqAvQNLV5Uge.jpg"},
             new Movie { Id=2, Title = "Interstellar", Budget= 160000000, OriginalLanguage="en", PosterUrl="https://image.tmdb.org/t/p/w342//gEU2QniE6E77NI6lCU6MxlNBvIx.jpg"},
             new Movie { Id=3, Title = "The Dark Knight", Budget= 160000000, OriginalLanguage="en", PosterUrl="https://image.tmdb.org/t/p/w342//qJ2tW6WMUDux911r6m7haRef0WH.jpg"},
             new Movie { Id=4, Title = "Deadpool", Budget= 160000000, OriginalLanguage="en", PosterUrl="https://image.tmdb.org/t/p/w342//yGSxMiF0cYuAiyuve5DA6bnWEOI.jpg"},
            new Movie { Id=5, Title = "The Avengers", Budget= 160000000, OriginalLanguage="en", PosterUrl="https://image.tmdb.org/t/p/w342//RYMX2wcKCBAr24UyPD7xwmjaTn.jpg"}
                        };
            return movies;
        }
    }
}
