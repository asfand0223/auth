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

        public async Task<RefreshToken?> GetByUserId(Guid userId)
        {
            return await _refreshTokenRepository.GetByUserId(userId);
        }

        public async Task<Guid?> Create(Guid userId)
        {
            return await _refreshTokenRepository.Create(userId);
        }

        public async Task<string?> RefreshAccessToken(Self self)
        {
            // Get refresh token from db and validate
            RefreshToken? refreshToken = await _refreshTokenRepository.GetByUserId(self.UserId);
            if (refreshToken == null)
            {
                return null;
            }
            if (DateTime.UtcNow > refreshToken.ExpiresAt)
            {
                return null;
            }
            // Refresh token(single-use) rotation
            bool refreshTokenDeleted = await _refreshTokenRepository.Delete(refreshToken.Id);
            if (!refreshTokenDeleted)
            {
                return null;
            }
            await _refreshTokenRepository.Create(self.UserId);

            return _accessTokenService.Generate(self.UserId, self.Username);
        }
    }
}
