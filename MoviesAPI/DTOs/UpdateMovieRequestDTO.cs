using MoviesAPI.Validation;
using System.ComponentModel.DataAnnotations;

namespace MoviesAPI.DTOs
{
    public class UpdateMovieRequestDTO
    {
        /// <summary>
        /// Title of the movie
        /// </summary>
        [MinLength(1)]
        public string? Title { get; set; }

        /// <summary>
        /// Description of the movie
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// Year released. Example: 2022
        /// </summary>
        [PositiveNumber]
        public int? ReleaseYear { get; set; }

        /// <summary>
        /// Duration in seconds
        /// </summary>
        [PositiveNumber]
        public int? Duration { get; set; }

        /// <summary>
        /// Rating from 0-5 stars
        /// </summary>
        [Rating]
        public int? Rating { get; set; }
    }
}
