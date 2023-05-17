using CatalogFilms.Data;
using CatalogFilms.Data.Models;
using CatalogFilms.Data.Models.DTO;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace CatalogFilms.Services
{
    public interface IAuthService
    {
        Task Register(UserRegisterDTO userRegisterDTO);
        bool CheckEmailIdentity(UserRegisterDTO userRegisterDTO);
        bool CheckUserNameIdentity(UserRegisterDTO userRegisterDTO);
        string GetJwtSecurityToken(LoginCredentialsDTO loginCredentialsDTO);
        void Logout(string jwt);
    }

    public class AuthService : IAuthService
    {
        private ApplicationDbContext _context;
        private ITokenService _tokenService;

        public AuthService(ApplicationDbContext context, ITokenService tokenService)
        {
            _context = context;
            _tokenService = tokenService;
        }

        public async Task Register(UserRegisterDTO userRegisterDTO)
        {
            var user = new User
            {
                Name = userRegisterDTO.Name,
                UserName = userRegisterDTO.UserName,
                BirthDate = userRegisterDTO.BirthDate,
                Password = userRegisterDTO.Password,
                Email = userRegisterDTO.Email,
                Gender = userRegisterDTO.Gender
            };
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public bool CheckUserNameIdentity(UserRegisterDTO userRegisterDTO)
        {
            var user = _context.Users.FirstOrDefault(x => x.UserName == userRegisterDTO.UserName);
            if (user != null)
                return true;
            return false;
        }

        public bool CheckEmailIdentity(UserRegisterDTO userRegisterDTO)
        {
            var user = _context.Users.FirstOrDefault(x => x.Email == userRegisterDTO.Email);
            if (user != null)
                return true;
            return false;
        }

        public string GetJwtSecurityToken(LoginCredentialsDTO loginCredentialsDTO)
        {
            var identity = GetIdentity(loginCredentialsDTO);
            if (identity == null)
            {
                return null;
            }

            var now = DateTime.UtcNow;

            var jwt = new JwtSecurityToken(
                issuer: JwtConfigurations.Issuer,
                audience: JwtConfigurations.Audience,
                notBefore: now,
                claims: identity.Claims,
                expires: now.Add(new TimeSpan(0, JwtConfigurations.Lifetime, 0)),
                signingCredentials: new SigningCredentials(JwtConfigurations.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            return encodedJwt;
        }

        public void Logout(string jwt)
        {
            var user = _tokenService.GetUserWithJwt(jwt);

            _context.Tokens.Add(new Token { User = user, Jwt = jwt }); ;
            _context.SaveChanges();
        }

        private ClaimsIdentity GetIdentity(LoginCredentialsDTO loginCredentialsDTO)
        {
            var user = _context.Users.FirstOrDefault(x => x.UserName == loginCredentialsDTO.Username && x.Password == loginCredentialsDTO.Password);

            if (user == null)
            {
                return null;
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, loginCredentialsDTO.Username),
                new Claim(ClaimTypes.Role, "User")
            };

            var claimsIdentity = new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);

            return claimsIdentity;
        }
    }
}
