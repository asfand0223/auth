using System.Text.Json;
using Auth.DTOs;
using Auth.Interfaces;
using Auth.Models;
using Auth.Types;

namespace Auth.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;

        public AuthService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<AuthResult> Register(RegisterDTO registerDTO)
        {
            User? existingUser = _userRepository.GetByUsername(registerDTO.Username);
            if (existingUser != null)
            {
                return new AuthResult { Error = "Username is already taken" };
            }
            if (registerDTO.Password != registerDTO.ConfirmPassword)
            {
                return new AuthResult { Error = "Passwords do not match" };
            }
            Guid id = Guid.NewGuid();
            User user = new User
            {
                Id = id,
                Username = registerDTO.Username,
                Password = registerDTO.Password,
                CreatedOn = DateTime.UtcNow,
            };
            bool created = await _userRepository.Create(user);
            if (!created)
            {
                return new AuthResult { Error = "Failed to create user" };
            }
            await using MemoryStream stream = new MemoryStream();
            using var reader = new StreamReader(stream);
            await JsonSerializer.SerializeAsync(
                stream,
                new RegisterResult
                {
                    Id = id,
                    Username = registerDTO.Username,
                    AccessToken = "",
                }
            );
            stream.Position = 0;
            string json = await reader.ReadToEndAsync();
            return new AuthResult { Data = json };
        }

        public async Task<AuthResult> Login(LoginDTO loginDTO)
        {
            User? existingUser = _userRepository.GetByUsername(loginDTO.Username);
            if (existingUser == null)
            {
                return new AuthResult { Error = "Account with this username does not exist" };
            }
            if (loginDTO.Password != existingUser.Password)
            {
                return new AuthResult { Error = "Incorrect password" };
            }
            await using MemoryStream stream = new MemoryStream();
            using var reader = new StreamReader(stream);
            await JsonSerializer.SerializeAsync(
                stream,
                new LoginResult
                {
                    Id = existingUser.Id,
                    Username = existingUser.Username,
                    AccessToken = "",
                }
            );
            stream.Position = 0;
            string json = await reader.ReadToEndAsync();
            return new AuthResult { Data = json };
        }
    }
}
