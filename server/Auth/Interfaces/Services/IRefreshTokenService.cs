using Auth.Entities;
using Auth.Models;

namespace Auth.Interfaces.Services
{
    public interface IRefreshTokenService
    {
        public RefreshToken? GetByUserId(Guid userId);
        public Task<Guid?> Create(Guid userId);
        public string? RefreshAccessToken(Self self);
    }
}
