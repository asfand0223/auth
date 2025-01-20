using Auth.Database;
using Auth.Interfaces;
using Auth.Models;

namespace Auth.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<User?> Get(Guid id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<bool> Create(User u)
        {
            try
            {
                await _context.AddAsync(u);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("UserRepository - Create: " + ex);
                return false;
            }
        }
    }
}
