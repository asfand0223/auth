using Auth.Models;

namespace Auth.Interfaces.Repositories
{
    public interface IRefreshTokenRepository
    {
        public Task<RefreshToken?> GetByUserId(Guid userId);
        public Task<Guid?> Create(Guid userId);
        public Task<bool> Delete(Guid id);
        public Task<bool> DeleteByUserId(Guid userId);
    }
}
