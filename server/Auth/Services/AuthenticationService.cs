using Auth.DTOs;
using Auth.Interfaces;
using Auth.Models;
using Auth.Results;
using Auth.Utils;

namespace Auth.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IAuthenticationValidationService _authenticationValidationService;
        private readonly IUserRepository _userRepository;

        public AuthenticationService(
            IAuthenticationValidationService authValidationService,
            IUserRepository userRepository
        )
        {
            _authenticationValidationService = authValidationService;
            _userRepository = userRepository;
        }

        public async Task<AuthenticationResult> Register(RegisterDTO dto)
        {
            AuthenticationValidationResult atvr = _authenticationValidationService.ValidateRegister(
                dto
            );
            if (atvr.Error != null)
            {
                return new AuthenticationResult { Error = atvr.Error };
            }

            Guid id = Guid.NewGuid();
            User u = new User
            {
                Id = id,
                Username = dto.Username,
                Password = dto.Password,
                CreatedOn = DateTime.UtcNow,
            };
            bool c = await _userRepository.Create(u);
            if (!c)
            {
                return new AuthenticationResult { Error = "Failed to create user" };
            }

            string json = await Json.Write<RegisterResult>(
                new RegisterResult
                {
                    Id = id,
                    Username = dto.Username,
                    AccessToken = "",
                }
            );
            return new AuthenticationResult { Data = json };
        }

        public async Task<AuthenticationResult> Login(LoginDTO dto)
        {
            (AuthenticationValidationResult avr, User? u) result =
                _authenticationValidationService.ValidateLogin(dto);
            if (result.avr.Error != null || result.u == null)
            {
                return new AuthenticationResult { Error = result.avr.Error };
            }

            string json = await Json.Write<LoginResult>(
                new LoginResult
                {
                    Id = result.u.Id,
                    Username = dto.Username,
                    AccessToken = "",
                }
            );
            return new AuthenticationResult { Data = json };
        }
    }
}
