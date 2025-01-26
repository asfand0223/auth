using System.Security.Claims;
using System.Text.Json;
using Auth.Configuration;
using Auth.Entities;
using Auth.Interfaces.Services;
using Microsoft.Extensions.Options;
using AR = Auth.Results;
using U = Auth.Utils;

namespace Auth.Services
{
    public class AccessTokenService : IAccessTokenService
    {
        private readonly IOptions<Config> _c;
        private readonly ISelfService _selfService;

        public AccessTokenService(IOptions<Config> c, ISelfService selfService)
        {
            _c = c;
            _selfService = selfService;
        }

        public string? Generate(Guid userId, string username)
        {
            Self? self = _selfService.CreateSelf(userId, username);
            if (self == null)
            {
                return null;
            }
            string selfJson = JsonSerializer.Serialize<Self>(self);
            return U.Jwt.Generate(
                _c.Value.Jwt.Key,
                _c.Value.Jwt.Issuer,
                _c.Value.Jwt.Audience,
                new List<Claim> { new Claim("self", $"{selfJson}") },
                _c.Value.Jwt.ExpiresIn.TotalSeconds
            );
        }

        public AR.TokenValidationResult Validate(string access_token)
        {
            return U.Jwt.Validate(
                access_token,
                _c.Value.Jwt.Key,
                _c.Value.Jwt.Issuer,
                _c.Value.Jwt.Audience
            );
        }
    }
}
