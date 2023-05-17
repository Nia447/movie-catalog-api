using CatalogFilms.Data;
using CatalogFilms.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace CatalogFilms.Services
{
    public interface ITokenService
    {
        User? GetUserWithJwt(string jwt);
        bool IsValidToken(string jwt);
        void ClearInvalidTokens();
    }
    public class TokenService : ITokenService
    {
        private ApplicationDbContext _context;

        public TokenService(ApplicationDbContext context)
        {
            _context = context;
        }
        public User? GetUserWithJwt(string jwt)
        {
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(jwt);
            var tokenS = jsonToken as JwtSecurityToken;
            var userName = tokenS.Claims.First(claim => claim.Type == ClaimTypes.Name).Value;

            return _context.Users
                .Include(userEntity => userEntity.FavoriteMovies)
                .Include(reviewEntity => reviewEntity.Reviews)
                .FirstOrDefault(x => x.UserName == userName);
        }

        public bool IsValidToken(string jwt)
        {
            var token = _context.Tokens.FirstOrDefault(x => x.Jwt == jwt);
            if (token == null)
                return true;
            return false;
        }

        public void ClearInvalidTokens()
        {
            foreach(Token token in _context.Tokens)
            {
                try
                {
                    if (IsInvalidToken(token.Jwt))
                    {
                        _context.Tokens.Remove(token);
                    }
                }
                catch
                {
                    continue;
                }
            }
        }

        [Authorize]
        private bool IsInvalidToken(string jwt)
        {
            return true;
        }
    }
}
