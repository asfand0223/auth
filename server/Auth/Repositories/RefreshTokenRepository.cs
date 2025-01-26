using Auth.Database;
using Auth.Interfaces.Repositories;
using Auth.Models;

namespace Auth.Repositories
{
    public class RefreshTokenRepository : IRefreshTokenRepository
    {
        private readonly ApplicationDbContext _context;

        public RefreshTokenRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public RefreshToken? GetByUserId(Guid userId)
        {
            try
            {
                return _context.RefreshTokens.FirstOrDefault(
                    (RefreshToken rt) => rt.UserId == userId
                );
            }
            catch (Exception ex)
            {
                Console.WriteLine("RefreshTokenRepository - GetByUsername: " + ex);
                return null;
            }
        }

        public async Task<Guid?> Create(Guid userId)
        {
            try
            {
                RefreshToken rt = new RefreshToken
                {
                    Id = Guid.NewGuid(),
                    UserId = userId,
                    ExpiresAt = DateTime.UtcNow,
                };
                await _context.AddAsync(rt);
                await _context.SaveChangesAsync();
                return rt.Id;
            }
            catch (Exception ex)
            {
                Console.WriteLine("RefreshTokenRepository - Create: " + ex);
                return null;
            }
        }
    }
}
