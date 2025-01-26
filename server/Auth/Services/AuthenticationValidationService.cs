using Auth.DTOs;
using Auth.Interfaces.Repositories;
using Auth.Interfaces.Services;
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

        public ValidateRegisterResult ValidateRegister(RegisterDTO dto)
        {
            ValidateRegisterResult result = new ValidateRegisterResult { };

            User? existingU = _userRepository.GetByUsername(dto.Username);
            if (existingU != null)
            {
                result.Error = "Username taken";
                return result;
            }
            if (dto.Password != dto.ConfirmPassword)
            {
                result.Error = "Passwords do not match";
                return result;
            }

            return result;
        }

        public ValidateLoginResult ValidateLogin(LoginDTO dto)
        {
            ValidateLoginResult result = new ValidateLoginResult { };

            User? existingU = _userRepository.GetByUsername(dto.Username);
            if (existingU == null)
            {
                result.Error = "Account with this username does not exist";
                return result;
            }
            if (dto.Password != existingU.Password)
            {
                result.Error = "Incorrect password";
                return result;
            }

            return result;
        }
    }
}
