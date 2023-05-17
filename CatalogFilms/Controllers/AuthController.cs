using CatalogFilms.Data;
using CatalogFilms.Data.Models;
using CatalogFilms.Data.Models.DTO;
using CatalogFilms.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CatalogFilms.Controllers
{
    [ApiController]
    [Route("api/account")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }
        
        [HttpPost("register")]
        public async Task<IActionResult> Post([FromBody] UserRegisterDTO userRegisterDTO)
        {
            if (ModelState.IsValid)
            {
                if (_authService.CheckEmailIdentity(userRegisterDTO))
                    return StatusCode(409, "User with this Email already exists.");
                if (_authService.CheckUserNameIdentity(userRegisterDTO))
                    return StatusCode(409, "User with this UserName already exists.");

                try
                {
                    await _authService.Register(userRegisterDTO);
                    return Post(new LoginCredentialsDTO { Username = userRegisterDTO.UserName, Password = userRegisterDTO.Password });
                }
                catch (ArgumentException exception)
                {
                    ModelState.AddModelError("RegistrationErrors: ", exception.Message);
                    return BadRequest();
                }
            }
            return BadRequest("Registration Model is invalid.");
        }

        [HttpPost("login")]
        public IActionResult Post([FromBody] LoginCredentialsDTO loginCredentialsDTO)
        {
            if (ModelState.IsValid && loginCredentialsDTO.Username != null && loginCredentialsDTO.Password != null)
            {
                var jwt = _authService.GetJwtSecurityToken(loginCredentialsDTO);
                
                if (jwt != null)
                {
                    var response = new
                    {
                        token = jwt
                    };

                    return new JsonResult(response);
                }
                else
                {
                    return Unauthorized("Login or password is Incorrect.");
                }
            }
            else
            {
                return BadRequest("Login Credentials is invalid.");
            }
        }

        [HttpPost("logout")]
        [Authorize]
        public IActionResult Post()
        {
            try
            {
                string jwt = Request.Headers["Authorization"].ToString().Split(" ")[1];
                _authService.Logout(jwt);
                return Ok();
            }
            catch (ArgumentException exception)
            {
                ModelState.AddModelError("LogoutErrors: ", exception.Message);
                return BadRequest();
            }
        }
    }
}
