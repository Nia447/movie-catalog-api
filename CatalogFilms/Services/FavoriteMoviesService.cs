using CatalogFilms.Data;
using CatalogFilms.Data.Models.DTO;
using Microsoft.EntityFrameworkCore;

namespace CatalogFilms.Services
{
    public interface IFavoriteMoviesService
    {
        MoviesListDTO? GetFavoriteMovies(string jwt);
        bool AddFavoriteMovie(string jwt, Guid movieId);
        bool DeleteFavoriteMovie(string jwt, Guid movieId);
    }
    public class FavoriteMoviesService : IFavoriteMoviesService
    {
        private ApplicationDbContext _context;
        private IConverterService _converterService;
        private ITokenService _tokenService;

        public FavoriteMoviesService(ApplicationDbContext context, IConverterService converterService, ITokenService tokenService)
        {
            _context = context;
            _converterService = converterService;
            _tokenService = tokenService;
        }

        public MoviesListDTO? GetFavoriteMovies(string jwt)
        {
            var user = _tokenService.GetUserWithJwt(jwt);

            if (user == null)
                return null;

            var movies = new MoviesListDTO();

            foreach (var movie in _context.Movies.Include(x => x.Reviews).Include(x => x.Genres).Where(x => x.Users.FirstOrDefault(x => x.Id == user.Id) != null))
            {
                movies.Movies.Add(_converterService.ConvertMovieToMovieDTO(movie));
            }

            return movies;
        }

        public bool AddFavoriteMovie(string jwt, Guid movieId)
        {
            var user = _tokenService.GetUserWithJwt(jwt);
            var movie = _context.Movies.FirstOrDefault(x => x.Id == movieId);

            if (user == null || movie == null)
            {
                return false;
            }

            try
            {
                user.FavoriteMovies.Add(movie);
                movie.Users.Add(user);
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool DeleteFavoriteMovie(string jwt, Guid movieId)
        {
            var user = _tokenService.GetUserWithJwt(jwt);
            var movie = _context.Movies.Include(x => x.Users).FirstOrDefault(x => x.Id == movieId);

            if (user == null || movie == null)
            {
                return false;
            }

            try
            {
                user.FavoriteMovies.Remove(movie);
                movie.Users.Remove(user);
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
