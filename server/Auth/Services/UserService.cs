using Auth.Interfaces.Repositories;
using Auth.Interfaces.Services;
using Auth.Models;

namespace Auth.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public User? GetByUsername(string username)
        {
            return _userRepository.GetByUsername(username);
        }

        public async Task<Guid?> Create(string username, string password)
        {
            return await _userRepository.Create(username, password);
        }
    }
}
