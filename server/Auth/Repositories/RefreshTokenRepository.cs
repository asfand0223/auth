using Auth.Database;
using Auth.Interfaces.Repositories;
using Auth.Models;
using Microsoft.EntityFrameworkCore;

namespace Auth.Repositories
{
    public class RefreshTokenRepository : IRefreshTokenRepository
    {
        private readonly ApplicationDbContext _context;

        public RefreshTokenRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<RefreshToken?> GetByUserId(Guid userId)
        {
            try
            {
                return await _context.RefreshTokens.FirstOrDefaultAsync(
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
                    ExpiresAt = DateTime.UtcNow.AddDays(1),
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

        public async Task<bool> Delete(Guid id)
        {
            try
            {
                await _context
                    .RefreshTokens.Where((RefreshToken rt) => rt.Id == id)
                    .ExecuteDeleteAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("RefreshTokenRepository - Delete: " + ex);
                return false;
            }
        }

        public async Task<bool> DeleteByUserId(Guid userId)
        {
            try
            {
                await _context
                    .RefreshTokens.Where((RefreshToken rt) => rt.UserId == userId)
                    .ExecuteDeleteAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("RefreshTokenRepository - DeleteByUserId: " + ex);
                return false;
            }
        }
    }
}
