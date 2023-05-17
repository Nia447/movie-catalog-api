using CatalogFilms.Data.Models.DTO;
using CatalogFilms.Services;
using Microsoft.AspNetCore.Mvc;

namespace CatalogFilms.Controllers
{
    [Route("api/movies")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private readonly IMovieService _movieService;

        public MovieController(IMovieService movieService)
        {
            _movieService = movieService;
        }

        [HttpGet("{page}")]
        public ActionResult<MoviesPagedListDTO> Get(int page = 1)
        {
            if (!_movieService.IsExitingPage(page))
                return StatusCode(404);

            return new JsonResult(_movieService.CreateMoviesPagedListDTO(page));
        }

        [HttpGet("details/{id}")]
        public ActionResult<MovieDetailsDTO> Get(Guid id)
        {
            if (!_movieService.IsExitingMovie(id))
                return StatusCode(404);

            return new JsonResult(_movieService.GetMovieDetailsDTO(id));
        }
    }
}
