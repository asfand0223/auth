using Auth.Configuration;
using Auth.Entities;
using Auth.Entities.Results;
using Auth.Interfaces.Services;
using Auth.Models;
using Auth.Results;
using Microsoft.Extensions.Options;
using AR = Auth.Results;

namespace Auth.Services
{
    public class AuthorisationService : IAuthorisationService
    {
        private readonly IOptions<Config> _config;
        private readonly IAccessTokenService _accessTokenService;
        private readonly ISelfService _selfService;
        private readonly IRefreshTokenService _refreshTokenService;

        public AuthorisationService(
            IOptions<Config> config,
            IAccessTokenService accessTokenService,
            ISelfService selfService,
            IRefreshTokenService refreshTokenService
        )
        {
            _config = config;
            _accessTokenService = accessTokenService;
            _selfService = selfService;
            _refreshTokenService = refreshTokenService;
        }

        public AuthoriseResult Authorise(string access_token)
        {
            // Prepare result object to return
            AR.AuthoriseResult result = new AuthoriseResult { };
            string authorisedAccessToken = access_token;
            // Validate access token
            AR.TokenValidationResult tokenValidationResult = _accessTokenService.Validate(
                access_token
            );
            if (!tokenValidationResult.Valid)
            {
                result.Error = "Failed to validate access token";
                return result;
            }
            // Access token valid - Validate self
            SelfResult selfResult = _selfService.GetSelf(access_token);
            if (selfResult.Self == null)
            {
                result.Error = "Invalid self";
                return result;
            }
            Self self = selfResult.Self;
            //Refresh token if expired
            if (tokenValidationResult.Valid && tokenValidationResult.Expired)
            {
                string? refreshedAccessToken = _refreshTokenService.RefreshAccessToken(self);
                if (string.IsNullOrWhiteSpace(refreshedAccessToken))
                {
                    result.Error = "Failed to refresh access token";
                    return result;
                }
                authorisedAccessToken = refreshedAccessToken;
            }

            result.AccessToken = authorisedAccessToken;
            result.Self = selfResult.Self;
            return result;
        }
    }
}
