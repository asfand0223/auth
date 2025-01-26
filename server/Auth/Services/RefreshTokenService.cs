using Auth.Configuration;
using Auth.Entities;
using Auth.Interfaces.Repositories;
using Auth.Interfaces.Services;
using Auth.Models;
using Microsoft.Extensions.Options;

namespace Auth.Services
{
    public class RefreshTokenService : IRefreshTokenService
    {
        private readonly IOptions<Config> _config;
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly IAccessTokenService _accessTokenService;

        public RefreshTokenService(
            IOptions<Config> config,
            IRefreshTokenRepository refreshTokenRepository,
            IAccessTokenService accessTokenService
        )
        {
            _config = config;
            _refreshTokenRepository = refreshTokenRepository;
            _accessTokenService = accessTokenService;
        }

        public RefreshToken? GetByUserId(Guid userId)
        {
            return _refreshTokenRepository.GetByUserId(userId);
        }

        public async Task<Guid?> Create(Guid userId)
        {
            return await _refreshTokenRepository.Create(userId);
        }

        public string? RefreshAccessToken(Self self)
        {
            RefreshToken? refreshToken = _refreshTokenRepository.GetByUserId(self.UserId);
            if (refreshToken == null)
            {
                return null;
            }
            bool refreshTokenIsExpired =
                DateTime.UtcNow.Subtract(refreshToken.ExpiresAt).TotalSeconds
                >= _config.Value.Jwt.ExpiresIn.TotalSeconds;
            if (refreshTokenIsExpired)
            {
                return null;
            }
            return _accessTokenService.Generate(self.UserId, self.Username);
        }
    }
}
