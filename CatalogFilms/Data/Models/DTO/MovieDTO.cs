namespace CatalogFilms.Data.Models.DTO
{
    public class MovieDTO
    {
        public Guid Id { get; set; }

        public string? Name { get; set; }

        public string? Country { get; set; }

        public string? Poster { get; set; }

        public int? Year { get; set; }

        public List<ReviewShortDTO>? Reviews { get; set; }

        public List<GenreDTO>? Genres { get; set; }
    }
}
