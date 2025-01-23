using Auth.DTOs;
using Auth.Interfaces;
using Auth.Models;
using Auth.Results;

namespace Auth.Services
{
    public class AuthenticationValidationService : IAuthenticationValidationService
    {
        private readonly IUserRepository _userRepository;

        public AuthenticationValidationService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public AuthenticationValidationResult ValidateRegister(RegisterDTO dto)
        {
            User? existingU = _userRepository.GetByUsername(dto.Username);
            if (existingU != null)
            {
                return new AuthenticationValidationResult { Error = "Username taken" };
            }

            if (dto.Password != dto.ConfirmPassword)
            {
                return new AuthenticationValidationResult { Error = "Passwords do not match" };
            }

            return new AuthenticationValidationResult { Error = null };
        }

        public (AuthenticationValidationResult avr, User? u) ValidateLogin(LoginDTO dto)
        {
            User? existingU = _userRepository.GetByUsername(dto.Username);
            if (existingU == null)
            {
                return (
                    new AuthenticationValidationResult
                    {
                        Error = "Account with this username does not exist",
                    },
                    null
                );
            }
            if (dto.Password != existingU.Password)
            {
                return (new AuthenticationValidationResult { Error = "Incorrect password" }, null);
            }
            return (new AuthenticationValidationResult { Error = null }, existingU);
        }
    }
}
