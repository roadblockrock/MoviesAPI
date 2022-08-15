namespace MoviesAPI.Exceptions
{
    public class DuplicateMovieExistsException: Exception
    {
        public DuplicateMovieExistsException(string movieName) : base($"Movie with Name {movieName} already exists")
        {
            MovieName = movieName;
        }

        public string MovieName { get; set; }
    }
}
