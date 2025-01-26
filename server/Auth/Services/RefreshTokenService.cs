using Auth.Interfaces.Repositories;
using Auth.Interfaces.Services;
using Auth.Models;

namespace Auth.Services
{
    public class RefreshTokenService : IRefreshTokenService
    {
        private readonly IRefreshTokenRepository _refreshTokenRepository;

        public RefreshTokenService(IRefreshTokenRepository refreshTokenRepository)
        {
            _refreshTokenRepository = refreshTokenRepository;
        }

        public RefreshToken? GetByUserId(Guid userId)
        {
            return _refreshTokenRepository.GetByUserId(userId);
        }

        public async Task<Guid?> Create(Guid userId)
        {
            return await _refreshTokenRepository.Create(userId);
        }
    }
}
