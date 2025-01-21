using Auth.Models;

namespace Auth.Interfaces
{
    public interface IUserRepository
    {
        public User? GetByUsername(string username);
        public Task<bool> Create(User u);
    }
}
