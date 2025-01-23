using Auth.Models;

namespace Auth.Interfaces
{
    public interface IUserService
    {
        public User? GetByUsername(string username);
        public Task<Guid?> Create(string username, string password);
    }
}
