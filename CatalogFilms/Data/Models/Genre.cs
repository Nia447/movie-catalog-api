using System.ComponentModel.DataAnnotations;

namespace CatalogFilms.Data.Models
{
    public class Genre
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        public ICollection<Movie> Movies { get; set; } = new List<Movie>();
    }
}
