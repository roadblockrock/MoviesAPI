namespace MoviesAPI.Exceptions
{
    public class UserNotFoundException : Exception
    {
        public UserNotFoundException(Guid userId) : base($"User with Id {userId} was not found")
        {
            UserId = userId;
        }

        public Guid UserId { get; set; }
    }
}
