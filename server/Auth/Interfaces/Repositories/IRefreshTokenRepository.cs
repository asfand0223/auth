using Auth.Models;

namespace Auth.Interfaces.Repositories
{
    public interface IRefreshTokenRepository
    {
        public RefreshToken? GetByUserId(Guid userId);
        public Task<Guid?> Create(Guid userId);
    }
}
