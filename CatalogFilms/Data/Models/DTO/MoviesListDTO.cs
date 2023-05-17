namespace CatalogFilms.Data.Models.DTO
{
    public class MoviesListDTO
    {
        public ICollection<MovieDTO?> Movies { get; set; } = new List<MovieDTO>();
    }
}
