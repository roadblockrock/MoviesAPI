using MoviesAPI.DTOs;
using MoviesAPI.Exceptions;
using MoviesAPI.Models;

namespace MoviesAPI.Services
{
    public enum MovieVoteType { UP, DOWN, NONE };


    public interface IMovieRepo
    {
        Movie CreateMovie(CreateMovieRequestDTO request);

        void UpdateMovie(int id, UpdateMovieRequestDTO request);

        void DeleteMovie(int movieId);

        Movie GetMovieById(int id);

        List<Movie> SearchMovies(string? query);

        void SetUserVoteOnMovie(int movieId, Guid userId, MovieVoteType vote);
    }
    public class MovieRepo: IMovieRepo
    {
        private readonly MoviesContext _context;

        public MovieRepo(MoviesContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Creates a new movie
        /// </summary>
        /// <param name="request">The request</param>
        /// <returns>The movie that was created</returns>
        /// <exception cref="DuplicateMovieExistsException">Thrown if there is already a movie with the given title</exception>
        public Movie CreateMovie(CreateMovieRequestDTO request)
        {
            // double check no movies exist with this new one's title
            if (_context.Movies.Any(m => m.Title == request.Title))
            {
                throw new DuplicateMovieExistsException(request.Title);
            }

            var newMovie = new Movie
            {
                Title = request.Title,
                Description = request.Description,
                Duration = request.Duration,
                ReleaseYear = request.ReleaseYear,
                Rating = request.Rating
            };

            _context.Add(newMovie);
            _context.SaveChanges();

            return newMovie;
        }

        /// <summary>
        /// Updates the given movie. Only non-null fields in the request will be applied as changes
        /// </summary>
        /// <param name="request">The request</param>
        /// <exception cref="MovieNotFoundException">thrown if the given movie ID cannot be found</exception>
        /// <exception cref="DuplicateMovieExistsException">thrown if the update would cause duplicate movie titles</exception>
        public void UpdateMovie(int id,UpdateMovieRequestDTO request)
        {
            var thisMovie = GetMovieById(id);

            // double check no movies exist with this new one's title
            if (request.Title != null && _context.Movies.Any(m => m.Title == request.Title && m.Id != id))
            {
                throw new DuplicateMovieExistsException(request.Title);
            }

            thisMovie.Title = request.Title == null ? thisMovie.Title : request.Title;
            thisMovie.Description = request.Description == null ? thisMovie.Description : request.Description;
            thisMovie.Duration = request.Duration == null ? thisMovie.Duration : (int)request.Duration;
            thisMovie.ReleaseYear = request.ReleaseYear == null ? thisMovie.ReleaseYear : (int)request.ReleaseYear;
            thisMovie.Rating = request.Rating == null ? thisMovie.Rating : (int)request.Rating;

            _context.SaveChanges();
        }

        /// <summary>
        /// Deletes the movie with the given ID
        /// </summary>
        /// <param name="movieId">Movie to delete</param>
        /// <exception cref="MovieNotFoundException">Thrown if the movie with the given ID cannot be found</exception>
        public void DeleteMovie(int movieId)
        {
            var thisMovie = GetMovieById(movieId);
            _context.Movies.Remove(thisMovie);
            _context.SaveChanges();
        }

        public Movie GetMovieById(int id)
        {
            var thisMovie = _context.Movies.Find(id);

            if (thisMovie == null)
            {
                throw new MovieNotFoundException(id);
            }

            return thisMovie;
        }

        /// <summary>
        /// Searches movie titles and returns a list of matches
        /// </summary>
        /// <param name="query">The search query. Case sensitive</param>
        /// <returns>A list of matching movies</returns>
        public List<Movie> SearchMovies(string? query)
        {
            if(query == null)
            {
                return _context.Movies.ToList();
            }

            // cheap search, could do this with a full text index if better performance is needed
            return _context.Movies.Where(m => m.Title.Contains(query)).ToList();
        }

        /// <summary>
        /// Sets the given user's vote on a given movie
        /// </summary>
        /// <param name="movieId">Id of the movie in question</param>
        /// <param name="userId">Id of the user in question</param>
        /// <param name="vote"></param>
        public void SetUserVoteOnMovie(int movieId, Guid userId, MovieVoteType vote)
        {
            // make sure the movie exists
            if (!_context.Movies.Any(m => m.Id == movieId))
            {
                throw new MovieNotFoundException(movieId);
            }

            // make sure the user exists
            if (!_context.Users.Any(u => u.Id == userId))
            {
                throw new UserNotFoundException(userId);
            }

            // get existing rating if these is one
            var thisRating = _context.UserRatings.Where(r => r.UserId == userId && r.MovieId == movieId).FirstOrDefault();

            // if user vote is NONE, remove the vote if there is one
            if (vote == MovieVoteType.NONE)
            {
                
                if(thisRating != null)
                {
                    _context.UserRatings.Remove(thisRating);
                }
            }
            else
            {
                var isLike = vote == MovieVoteType.UP;

                if (thisRating != null)
                {
                    thisRating.isLike = isLike;
                }
                else
                {
                    _context.UserRatings.Add(new MovieUserRating
                    {
                        UserId = userId,
                        MovieId = movieId,
                        isLike = isLike
                    });
                }
            }
            _context.SaveChanges();
        }
    }
}
