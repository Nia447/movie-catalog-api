namespace CatalogFilms.Data.Models.DTO
{
    public class MoviesPagedListDTO
    {
        public List<MovieDTO> Movies { get; set; } = new List<MovieDTO>();

        public PageInfoDTO PageInfo { get; set; } = new PageInfoDTO();
    }
}
