using MoviesAPI.Validation;
using System.ComponentModel.DataAnnotations;

namespace MoviesAPI.DTOs
{
    public class CreateMovieRequestDTO
    {
        /// <summary>
        /// Title of the movie
        /// </summary>
        [Required]
        [MinLength(1)]
        public string Title { get; set; }

        /// <summary>
        /// Description of the movie
        /// </summary>
        [Required]
        public string Description { get; set; }

        /// <summary>
        /// Year released. Example: 2022
        /// </summary>
        [Required]
        [PositiveNumber]
        public int ReleaseYear { get; set; }

        /// <summary>
        /// Duration in seconds
        /// </summary>
        [Required]
        [PositiveNumber]
        public int Duration { get; set; }

        /// <summary>
        /// Rating from 0-5 stars
        /// </summary>
        [Required]
        [Rating]
        public int Rating { get; set; }
    }
}
