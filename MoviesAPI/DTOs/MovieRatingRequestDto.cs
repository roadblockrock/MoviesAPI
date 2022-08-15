using MoviesAPI.Services;
using System.ComponentModel.DataAnnotations;

namespace MoviesAPI.DTOs
{
    public class MovieRatingRequestDto
    {
        [Required]
        public int MovieId { get; set; }
        [Required]
        public Guid UserId { get; set; }
        [Required]
        public MovieVoteType Vote { get; set; }
    }
}
