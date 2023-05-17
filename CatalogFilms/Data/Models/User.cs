using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CatalogFilms.Data.Models
{
    public class User
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 3)]
        public string Name { get; set; }

        [Required]
        [Range(typeof(DateTime), "1900-01-01T00:01:01.001Z", "2017-01-01T00:01:01.000Z",
            ErrorMessage = "Value for {0} must be between {1} and {2}")]
        public DateTime BirthDate { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 3)]
        public string UserName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,15}$", ErrorMessage = "Password must has at least 8 symbols, 1 lowercase letter, 1 uppercase letter and 1 number")]
        public string Password { get; set; }

        public string? AvatarLink { get; set; }

        public bool IsAdmin { get; set; } = false;

        public Gender? Gender { get; set; }

        public ICollection<Movie> FavoriteMovies { get; set; } = new List<Movie>();

        public ICollection<Review> Reviews { get; set; } = new List<Review>();

        public ICollection<Token> Tokens { get; set; } = new List<Token>();
    }
}
