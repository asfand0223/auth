using Auth.Models;

namespace Auth.Interfaces
{
    public interface IUserRepository
    {
        public Task<User?> Get(Guid id);
        public Task<bool> Create(User u);
    }
}
