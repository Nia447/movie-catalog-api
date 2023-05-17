namespace CatalogFilms.Data.Models.DTO
{
    public class ReviewDTO
    {
        public Guid Id { get; set; }

        public UserShortDTO Author { get; set; }

        public string? ReviewText { get; set; } = "";

        public int Rating { get; set; }

        public bool IsAnonymous { get; set; } = true;

        public DateTime CreateDateTime { get; set; }
    }
}
