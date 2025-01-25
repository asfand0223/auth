using System.Security.Claims;
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
            AR.AuthoriseResult result = new AuthoriseResult { Self = null, Error = null };

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
                return result;
            }

            List<Claim> claims = tokenValidationResult.Claims;
            string? userId = claims
                .Where(c => c.Type == "user_id")
                .Select(c => c.Value)
                .FirstOrDefault();
            string? username = claims
                .Where(c => c.Type == "username")
                .Select(c => c.Value)
                .FirstOrDefault();
            if (!Guid.TryParse(userId, out var userGuid))
            {
                result.Error = "Failed to resolve user_id claim";
                return result;
            }
            if (string.IsNullOrWhiteSpace(username))
            {
                result.Error = "Failed to resolve username claim";
                return result;
            }

            result.Self = new Self { Id = userGuid, Username = username };
            return result;
        }
    }
}
