using Auth.Entities;
using Auth.Models;

namespace Auth.Interfaces.Services
{
    public interface IRefreshTokenService
    {
        public Task<RefreshToken?> GetByUserId(Guid userId);
        public Task<Guid?> Create(Guid userId);
        public Task<string?> RefreshAccessToken(Self self);
        public Task<bool> Delete(Guid id);
        public Task<bool> DeleteByUserId(Guid userId);
    }
}
