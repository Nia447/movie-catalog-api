using System.ComponentModel.DataAnnotations;

namespace CatalogFilms.Data.Models.DTO
{
    public class ProfileDTO
    {
        public Guid Id { get; set; }

        [Required]
        [StringLength(30, MinimumLength = 2)]
        public string Name { get; set; }

        [Range(typeof(DateTime), "1900-01-01T00:01:01.001Z", "2017-01-01T00:01:01.000Z",
            ErrorMessage = "Value for {0} must be between {1} and {2}")]
        public DateTime BirthDate { get; set; }

        [Required]
        [StringLength(30, MinimumLength = 3)]
        public string? NickName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public string? AvatarLink { get; set; }

        public Gender? Gender { get; set; }
    }
}
