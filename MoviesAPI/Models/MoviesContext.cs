using Microsoft.EntityFrameworkCore;

namespace MoviesAPI.Models
{
    public class MoviesContext: DbContext
    {
        public MoviesContext(DbContextOptions options): base(options)
        {
        }

        public DbSet<Movie> Movies { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<MovieUserRating> UserRatings { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Movie>()
                .HasIndex(m => m.Title)
                .IsUnique();
        }

    }
}
