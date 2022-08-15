using MoviesAPI.Models;

namespace MoviesAPI.Services
{
    public interface IUserRepo
    {
        Guid RegisterNewUser();
    }

    public class UserRepo: IUserRepo
    {
        private readonly MoviesContext _context;

        public UserRepo(MoviesContext context)
        {
            _context = context;
        }

        public Guid RegisterNewUser()
        {
            var newUser = new User
            {
                DateCreated = DateTime.Now
            };

            _context.Users.Add(newUser);
            _context.SaveChanges();

            return newUser.Id;
        }
    }
}
