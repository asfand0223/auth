using Auth.Entities;
using Auth.Models;

namespace Auth.Interfaces.Services
{
    public interface IRefreshTokenService
    {
        public Task<RefreshToken?> GetByUserId(Guid userId);
        public Task<Guid?> Create(Guid userId);
        public Task<string?> RefreshAccessToken(Self self);
    }
}
