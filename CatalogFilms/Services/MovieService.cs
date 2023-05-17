using CatalogFilms.Data;
using CatalogFilms.Data.Models;
using CatalogFilms.Data.Models.DTO;
using Microsoft.EntityFrameworkCore;

namespace CatalogFilms.Services
{
    public interface IMovieService
    {
        bool IsExitingPage(int page);
        bool IsExitingMovie(Guid id);
        MoviesPagedListDTO CreateMoviesPagedListDTO(int page);
        MovieDetailsDTO? GetMovieDetailsDTO(Guid id);
    }

    public class MovieService : IMovieService
    {
        private ApplicationDbContext _context;
        private IConverterService _converterService;
        private const int pageSize = 6;

        public MovieService(ApplicationDbContext context, IConverterService converterService)
        {
            _context = context;
            _converterService = converterService;
        }

        public bool IsExitingPage(int page)
        {
            if ((page <= 0 || page > GetPageCount()) && page != 1)
                return false;
            return true;
        }

        public MoviesPagedListDTO CreateMoviesPagedListDTO(int page)
        {
            var pageInfo = new PageInfoDTO { CurrentPage = page, PageSize = pageSize, PageCount = GetPageCount() };
            var movies = new List<Movie>();

            if (page != pageInfo.PageCount && pageInfo.PageCount != 0)
            {
                movies = _context.Movies.Include(x => x.Reviews).Include(x => x.Genres).Include(x => x.Users).Skip((page - 1) * pageSize).Take(pageSize).ToList();
            }
            else
            {
                movies = _context.Movies.Include(x => x.Reviews).Include(x => x.Genres).Include(x => x.Users).Skip((page - 1) * pageSize).Take(_context.Movies.Count() - (pageInfo.PageCount - 1) * pageSize).ToList();
            }
            var moviesDTO = new List<MovieDTO>();
            foreach (var movie in movies)
            {
                moviesDTO.Add(_converterService.ConvertMovieToMovieDTO(movie));
            }
            return new MoviesPagedListDTO { PageInfo = pageInfo, Movies = moviesDTO };        
        }

        private int GetPageCount()
        {
            return Convert.ToInt32(Math.Ceiling(Convert.ToDouble(_context.Movies.Count()) / Convert.ToDouble(pageSize)));
        }

        public bool IsExitingMovie(Guid id)
        {
            var movie = _context.Movies.FirstOrDefault(x => x.Id == id);

            if (movie == null)
                return false;
            return true;
        }

        public MovieDetailsDTO? GetMovieDetailsDTO(Guid id)
        {
            var movie = _context.Movies.Include(x => x.Reviews).Include(x => x.Genres).Include(x => x.Users).FirstOrDefault(x => x.Id == id);

            if (movie == null)
                return null;

            return new MovieDetailsDTO
            {
                Id = movie.Id,
                Name = movie.Name,
                Poster = movie.Poster,
                AgeLimit = movie.AgeLimit,
                Budget = movie.Budget,
                Description = movie.Description,
                Director = movie.Director,
                Country = movie.Country,
                Fees = movie.Fees,
                Tagline = movie.Tagline,
                Time = movie.Time,
                Year = movie.Year,
                Genres = _converterService.ConvertListGenreToGenreDTO(movie.Genres),
                Reviews = _converterService.ConvertListReviewToReviewDTO(movie.Reviews)
            };
        }
    }
}
