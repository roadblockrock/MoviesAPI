using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MoviesAPI.Models
{
    /// <summary>
    /// Represents a user's like or dislike of a given movie
    /// </summary>
    public class MovieUserRating
    {
        /// <summary>
        /// Note- Id column could be removed and a Composite key of MovieId and UserUniqueId could be used instead.
        /// Id column was used as a personal preference
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Unique Id of the user who performed this rating
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// Unique Id of the movie this rating is for
        /// </summary>
        public int MovieId { get; set; }

        /// <summary>
        /// If true, this is a like. If false, this is a dislike
        /// </summary>
        [Required]
        public bool isLike { get; set; }

        [ForeignKey(nameof(UserId))]
        public User User { get; set; }

        [ForeignKey(nameof(MovieId))]
        public Movie Movie { get; set; }
    }
}
