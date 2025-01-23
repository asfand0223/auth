using Auth.Interfaces;
using Auth.Models;

namespace Auth.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _ur;

        public UserService(IUserRepository ur)
        {
            _ur = ur;
        }

        public User? GetByUsername(string username)
        {
            return _ur.GetByUsername(username);
        }

        public async Task<Guid?> Create(string username, string password)
        {
            return await _ur.Create(username, password);
        }
    }
}
