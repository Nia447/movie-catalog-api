namespace CatalogFilms.Data.Models
{
    public class Token
    {
        public Guid Id { get;set; }

        public string Jwt { get; set; }

        public User User { get; set; }
    }
}
