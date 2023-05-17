using System.ComponentModel.DataAnnotations;

namespace CatalogFilms.Data.Models
{
    public class Movie
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string? Country { get; set; }

        public string? Poster { get; set; }

        public string? Description { get; set; }
        
        [Range(1895, 2050)]
        public int Year { get; set; }

        [Range(1, 1000)]
        public int Time { get; set; }
        
        public string? Tagline { get; set; }
        
        public string? Director { get; set; }
        
        [Range(0, int.MaxValue)]
        public int Budget { get; set; }

        [Range(0, int.MaxValue)]
        public int Fees { get; set; }
        
        [Range(0, 18)]
        public int AgeLimit { get; set; }

        public ICollection<User> Users { get; set; } = new List<User>();

        public ICollection<Review> Reviews { get; set; } = new List<Review>();

        public ICollection<Genre> Genres { get; set; } = new List<Genre>();
    }
}
