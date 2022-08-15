namespace MoviesAPI.Exceptions
{
    public class MovieNotFoundException: Exception
    {
        public MovieNotFoundException(int movieId) : base($"Movie with Id {movieId} was not found")
        {
            MovieId = movieId;
        }

        public int MovieId { get; set; }
    }
}
