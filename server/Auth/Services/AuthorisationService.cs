using System.Security.Claims;
using System.Text.Json;
using Auth.Entities;
using Auth.Interfaces.Services;
using Auth.Results;
using AR = Auth.Results;

namespace Auth.Services
{
    public class AuthorisationServcie : IAuthorisationService
    {
        private readonly IAccessTokenService _accessTokenService;

        public AuthorisationServcie(IAccessTokenService accessTokenService)
        {
            _accessTokenService = accessTokenService;
        }

        public AuthoriseResult Authorise(string access_token)
        {
            AR.AuthoriseResult result = new AuthoriseResult { };

            AR.TokenValidationResult tokenValidationResult = _accessTokenService.Validate(
                access_token
            );
            if (!tokenValidationResult.Valid && !tokenValidationResult.Expired)
            {
                result.Error = "Failed to validate access token";
                return result;
            }
            if (!tokenValidationResult.Valid && tokenValidationResult.Expired)
            {
                result.Error = "Access token expired";
                return result;
            }
            if (tokenValidationResult.Claims == null)
            {
                result.Error = "No claims found";
                return result;
            }

            List<Claim> claims = tokenValidationResult.Claims;
            string? selfJson = claims
                .Where(c => c.Type == "self")
                .Select(c => c.Value)
                .FirstOrDefault();
            if (selfJson == null)
            {
                result.Error = "Failed to find self claim";
                return result;
            }
            Self? self = JsonSerializer.Deserialize<Self>(selfJson);
            if (self == null)
            {
                result.Error = "Failed to deserialise self";
                return result;
            }
            result.AccessToken = tokenValidationResult.AccessToken;
            result.Self = self;
            return result;
        }
    }
}
