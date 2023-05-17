using System.ComponentModel.DataAnnotations;

namespace CatalogFilms.Data.Models
{
    public class Review
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        public User Author { get; set; }

        [Required]
        public Movie MovieToReview { get; set; }
        
        [Required(AllowEmptyStrings = true)]
        public string ReviewText { get; set; } = "";

        [Range(1, 10)]
        public int Rating { get; set; }

        public bool IsAnonymous { get; set; } = true;

        public DateTime CreateDateTime { get; set; }
    }
}
