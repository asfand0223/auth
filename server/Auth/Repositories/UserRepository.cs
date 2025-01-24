using Auth.Database;
using Auth.Interfaces.Repositories;
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

        public User? GetByUsername(string username)
        {
            return _context.Users.FirstOrDefault((User u) => u.Username == username);
        }

        public async Task<Guid?> Create(string username, string password)
        {
            try
            {
                User u = new User
                {
                    Id = Guid.NewGuid(),
                    Username = username,
                    Password = password,
                    CreatedOn = DateTime.UtcNow,
                };
                await _context.AddAsync(u);
                await _context.SaveChangesAsync();
                return u.Id;
            }
            catch (Exception ex)
            {
                Console.WriteLine("UserRepository - Create: " + ex);
                return null;
            }
        }
    }
}
