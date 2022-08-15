using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace MoviesAPI.Models
{
    public class Movie
    {
        [Key]
        public int Id { get; set; }
        /// <summary>
        /// Title of the movie
        /// </summary>
        [Required]
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
        public int ReleaseYear { get; set; }

        /// <summary>
        /// Duration in seconds
        /// </summary>
        [Required]
        public int Duration { get; set; }

        /// <summary>
        /// Rating from 0-5 stars
        /// </summary>
        [Required]
        public int Rating { get; set; }
    }
}
