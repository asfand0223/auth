using Auth.Models;

namespace Auth.Interfaces.Repositories
{
    public interface IUserRepository
    {
        public User? GetByUsername(string username);
        public Task<Guid?> Create(string username, string password);
    }
}
