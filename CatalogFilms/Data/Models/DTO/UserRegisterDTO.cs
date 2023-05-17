using System.ComponentModel.DataAnnotations;

namespace CatalogFilms.Data.Models.DTO
{
    public class UserRegisterDTO
    {
        [Required]
        [StringLength(20, MinimumLength = 3)]
        public string Name { get; set; }

        [Range(typeof(DateTime), "1900-01-01T00:01:01.001Z", "2017-01-01T00:01:01.000Z",
            ErrorMessage = "Value for {0} must be between {1} and {2}")]
        public DateTime BirthDate { get; set; } = DateTime.Now;

        [Required]
        [StringLength(20, MinimumLength = 3)]
        public string UserName { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 3)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,15}$", ErrorMessage = "Password must has at least 8 symbols, 1 lowercase letter, 1 uppercase letter and 1 number")]
        public string Password { get; set; } = "Password123";

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public Gender? Gender { get; set; }
    }
}
