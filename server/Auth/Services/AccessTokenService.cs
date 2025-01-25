using System.Security.Claims;
using Auth.Configuration;
using Auth.Interfaces.Services;
using Microsoft.Extensions.Options;
using AR = Auth.Results;
using U = Auth.Utils;

namespace Auth.Services
{
    public class AccessTokenService : IAccessTokenService
    {
        private readonly IOptions<Config> _c;

        public AccessTokenService(IOptions<Config> c)
        {
            _c = c;
        }

        public string Generate(Guid userId, string username)
        {
            return U.Jwt.GenerateToken(
                _c.Value.Jwt.Key,
                _c.Value.Jwt.Issuer,
                _c.Value.Jwt.Audience,
                new List<Claim>
                {
                    new Claim("user_id", $"{userId}"),
                    new Claim("username", $"{username}"),
                },
                _c.Value.Jwt.ExpiresIn.TotalSeconds
            );
        }

        public AR.TokenValidationResult Validate(string access_token)
        {
            return U.Jwt.ValidateToken(
                access_token,
                _c.Value.Jwt.Key,
                _c.Value.Jwt.Issuer,
                _c.Value.Jwt.Audience
            );
        }
    }
}
