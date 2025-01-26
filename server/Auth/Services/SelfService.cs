using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text.Json;
using Auth.Entities;
using Auth.Entities.Results;
using Auth.Interfaces.Services;

namespace Auth.Services
{
    public class SelfService : ISelfService
    {
        public Self? CreateSelf(Guid userId, string username)
        {
            return new Self { UserId = userId, Username = username };
        }

        public SelfResult GetSelf(string accessToken)
        {
            SelfResult result = new SelfResult { };
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            JwtSecurityToken token = tokenHandler.ReadJwtToken(accessToken);
            List<Claim> claims = token.Claims.ToList();
            string? selfJson = claims
                .Where(c => c.Type == "self")
                .Select(c => c.Value)
                .FirstOrDefault();
            if (selfJson == null)
            {
                result.Error = "Self claim not found";
                return result;
            }
            Self? self = JsonSerializer.Deserialize<Self>(selfJson);
            if (self == null)
            {
                result.Error = "Failed to deserialise self claim";
                return result;
            }
            result.Self = self;
            return result;
        }
    }
}
