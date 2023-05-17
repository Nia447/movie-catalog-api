using CatalogFilms.Data.Models.DTO;
using CatalogFilms.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CatalogFilms.Controllers
{
    [Route("api/favorites")]
    [ApiController]
    public class FavoriteMoviesController : ControllerBase
    {
        IFavoriteMoviesService _favoriteMoviesService;
        ITokenService _tokenService;

        public FavoriteMoviesController(IFavoriteMoviesService favoriteMoviesService, ITokenService tokenService)
        {
            _favoriteMoviesService = favoriteMoviesService;
            _tokenService = tokenService;
        }

        [HttpGet]
        [Authorize]
        public ActionResult<MoviesListDTO> Get()
        {
            string jwt = Request.Headers["Authorization"].ToString().Split(" ")[1];
            if (!_tokenService.IsValidToken(jwt))
            {
                return Unauthorized("This token is invalid.");
            }

            return new JsonResult(_favoriteMoviesService.GetFavoriteMovies(jwt));
        }

        [HttpPost("{id}/add")]
        [Authorize]
        public IActionResult Post(Guid id)
        {
            string jwt = Request.Headers["Authorization"].ToString().Split(" ")[1];
            if (!_tokenService.IsValidToken(jwt))
            {
                return Unauthorized("This token is invalid.");
            }

            try
            {
                bool result = _favoriteMoviesService.AddFavoriteMovie(jwt, id);

                if (result)
                {
                    return Ok();
                }
            }
            catch (ArgumentException exception)
            {
                ModelState.AddModelError("AddError: ", exception.Message);
            }

            return StatusCode(409, "This favorite movie can't add.");
        }

        [HttpDelete("{id}/delete")]
        [Authorize]
        public IActionResult Delete(Guid id)
        {
            string jwt = Request.Headers["Authorization"].ToString().Split(" ")[1];
            if (!_tokenService.IsValidToken(jwt))
            {
                return Unauthorized("This token is invalid.");
            }

            try
            {
                bool result = _favoriteMoviesService.DeleteFavoriteMovie(jwt, id);

                if (result)
                {
                    return Ok();
                }
            }
            catch (ArgumentException exception)
            {
                ModelState.AddModelError("DeleteError: ", exception.Message);
            }

            return BadRequest("This favorite movie can't delete.");
        }
    }
}
