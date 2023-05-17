using CatalogFilms.Data;
using CatalogFilms.Data.Models.DTO;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace CatalogFilms.Services
{
    public interface IUserService
    {
        ProfileDTO? GetUser(string jwt);

        bool EditUser(string jwt, ProfileDTO profileDTO);
    }

    public class UserService : IUserService
    {
        private ApplicationDbContext _context;
        private ITokenService _tokenService;

        public UserService(ApplicationDbContext context, ITokenService tokenService)
        {
            _context = context;
            _tokenService = tokenService;
        }

        public ProfileDTO? GetUser(string jwt)
        {
            var user = _tokenService.GetUserWithJwt(jwt);

            if (user == null)
                return null;

            return new ProfileDTO
            {
                Id = user.Id,
                Name = user.Name,
                NickName = user.UserName,
                Email = user.Email,
                AvatarLink = user.AvatarLink,
                BirthDate = user.BirthDate,
                Gender = user.Gender
            };
        }

        public bool EditUser(string jwt, ProfileDTO profileDTO)
        {
            var user = _tokenService.GetUserWithJwt(jwt);
            
            if (user == null)
                return false;

            try
            {
                user.Name = profileDTO.Name;
                user.Email = profileDTO.Email;
                user.BirthDate = profileDTO.BirthDate;
                user.Gender = profileDTO.Gender;
                user.AvatarLink = profileDTO.AvatarLink;
                if (profileDTO.NickName != null)
                {
                    user.UserName = profileDTO.NickName;
                }

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
