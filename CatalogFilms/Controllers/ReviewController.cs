using CatalogFilms.Data.Models.DTO;
using CatalogFilms.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CatalogFilms.Controllers
{
    [Route("api/movie/{movieId}/review")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewService _reviewService;
        private readonly ITokenService _tokenService;

        public ReviewController(IReviewService reviewService, ITokenService tokenService)
        {
            _reviewService = reviewService;
            _tokenService = tokenService;
        }

        [HttpPost("add")]
        [Authorize]
        public IActionResult Post(Guid movieId, [FromBody] ReviewModifyDTO reviewModifyDTO)
        {
            string jwt = Request.Headers["Authorization"].ToString().Split(" ")[1];
            if (!_tokenService.IsValidToken(jwt))
            {
                return Unauthorized("This token is invalid.");
            }

            if (ModelState.IsValid)
            {
                if (_reviewService.DoesUserHaveReview(jwt, movieId))
                {
                    return StatusCode(409, "There is User's review for this movie.");
                }

                try
                {
                    bool result = _reviewService.AddReview(jwt, movieId, reviewModifyDTO);

                    if (result)
                    {
                        return Ok();
                    }
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError("reviewShortDTOError: ", exception.Message);
                }

                return StatusCode(409, "This review can't add.");
            }

            return BadRequest("Review Modify is invalid.");
        }

        [HttpPut("{id}/edit")]
        [Authorize]
        public IActionResult Put(Guid movieId, Guid id, [FromBody] ReviewModifyDTO reviewModifyDTO)
        {
            string jwt = Request.Headers["Authorization"].ToString().Split(" ")[1];
            if (!_tokenService.IsValidToken(jwt))
            {
                return Unauthorized("This token is invalid.");
            }

            if (ModelState.IsValid)
            {

                if (!_reviewService.DoesUserHaveReview(jwt, movieId))
                {
                    return StatusCode(409, "There isn't User's review for this movie.");
                }

                if (!_reviewService.IsReviewUsers(jwt, id))
                {
                    return StatusCode(403, "The user can't edit the comment because it isn't his comment.");
                }

                try
                {
                    bool result = _reviewService.EditReview(id, reviewModifyDTO);

                    if (result)
                    {
                        return Ok();
                    }
                }
                catch (ArgumentException exception)
                {
                    ModelState.AddModelError("reviewShortDTOError: ", exception.Message);
                }

                return StatusCode(409, "This review can't edit.");
            }

            return BadRequest("Review Modify is invalid.");
        }

        [HttpDelete("{id}/delete")]
        [Authorize]
        public IActionResult Delete(Guid movieId, Guid id)
        {
            string jwt = Request.Headers["Authorization"].ToString().Split(" ")[1];
            if (!_tokenService.IsValidToken(jwt))
            {
                return Unauthorized("This token is invalid.");
            }

            if (!_reviewService.IsReviewUsers(jwt, id))
            {
                return StatusCode(403, "The user can't delete the comment because it isn't his comment.");
            }

            try
            {
                bool result = _reviewService.DeleteReview(id, movieId);

                if (result)
                {
                    return Ok();
                }
            }
            catch (ArgumentException exception)
            {
                ModelState.AddModelError("deleteError: ", exception.Message);
            }

            return StatusCode(409, "This review can't delete.");
        }
    }
}
