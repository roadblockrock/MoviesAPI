using MoviesAPI.Models;

namespace MoviesAPI.Services
{
    public interface IMovieInitializer
    {
        void Seed();
    }

    public class MovieInitializer: IMovieInitializer
    {
        private readonly MoviesContext _context;

        public MovieInitializer(MoviesContext context)
        {
            _context = context;
        }

        public void Seed()
        {
            if (_context.Movies.Any())
            {
                return;
            }

            // Disclaimer- this data does not reflect my personal tastes ;)
            var movies = new List<Movie>
            {
                new Movie{Title="Inception", Description="A dream within a dream", Duration=148, Rating=5, ReleaseYear=2010 },
                new Movie{Title="The Dark Knight", Description="Wanna know where I got these scars?", Duration=152, Rating=4, ReleaseYear=2008 },
                new Movie{Title="The Dark Knight Rises", Description="You're a big guy", Duration=165, Rating=3, ReleaseYear=2012 },
                new Movie{Title="Avengers: Infinity War", Description="Perfectly balanced, as all things should be", Duration=149, Rating=2, ReleaseYear=2018 },
                new Movie{Title="Avengers: Endgame", Description="I see this as an absolute win!", Duration=181, Rating=1, ReleaseYear=2019 },
            };

            movies.ForEach(m => _context.Movies.Add(m));
            _context.SaveChanges();
        }
    }
}
