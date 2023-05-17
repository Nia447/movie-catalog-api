using CatalogFilms.Data.Models.DTO;
using CatalogFilms.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CatalogFilms.Controllers
{
    [Route("api/account/profile")]
    [ApiController]
    public class UserController : Controller
    {
        IUserService _userService;
        ITokenService _tokenService;

        public UserController(IUserService userService, ITokenService tokenService)
        {
            _userService = userService;
            _tokenService = tokenService;
        }

        [HttpGet]
        [Authorize]
        public ActionResult<ProfileDTO> Get()
        {
            string jwt = Request.Headers["Authorization"].ToString().Split(" ")[1];
            if (!_tokenService.IsValidToken(jwt))
            {
                return Unauthorized("This token is invalid.");
            }

            var user = _userService.GetUser(jwt);
            if (user == null)
            {
                return StatusCode(404, "User not found.");
            }

            return new JsonResult(user);
        }

        [HttpPut]
        [Authorize]
        public IActionResult Put(ProfileDTO profileDTO)
        {
            string jwt = Request.Headers["Authorization"].ToString().Split(" ")[1];
            if (!_tokenService.IsValidToken(jwt))
            {
                return Unauthorized("This token is invalid.");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    bool result = _userService.EditUser(jwt, profileDTO);

                    if (result)
                    {
                        return Ok();
                    }
                }
                catch (ArgumentException exception)
                {
                    ModelState.AddModelError("ProfileDTOError: ", exception.Message);
                }

                return StatusCode(409, "This user can't edit.");
            }

            return BadRequest("Profile is invalid.");
        }
    }
}
