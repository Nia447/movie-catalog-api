namespace CatalogFilms.Data.Models.DTO
{
    public class ReviewModifyDTO
    {
        public string ReviewText { get; set; } = "";

        public int Rating { get; set; }

        public bool IsAnonymous { get; set; } = true;
    }
}
