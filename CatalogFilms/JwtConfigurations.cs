using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace CatalogFilms
{
    public class JwtConfigurations
    {
        public const string Issuer = "JwtIssuer";
        public const string Audience = "JwtClient";
        private const string Key = "ShlepaLovesDeliciousPelmens!ThereIsManyMeat!ThereIsLittleDough!*322";
        public const int Lifetime = 60;
        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Key));
        }
    }
}
