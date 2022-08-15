using System.ComponentModel.DataAnnotations;

namespace MoviesAPI.Models
{
    /// <summary>
    /// Represents a user. Because users are anonymous, the only purpose of this table is tracking a user's previous likes and dislikes. 
    /// </summary>
    public class User
    {
        /// <summary>
        /// Unique Id of the user. Uses a GUID to make it hard to guess one
        /// </summary>
        [Key]
        public Guid Id { get; set; }

        /// <summary>
        /// Date this user's entry was added to the db
        /// </summary>
        [Required]
        public DateTime DateCreated { get; set; }
    }
}
