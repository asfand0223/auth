using Auth.DTOs;
using Auth.Models;
using Auth.Results;

namespace Auth.Interfaces
{
    public interface IAuthenticationValidationService
    {
        public AuthenticationValidationResult ValidateRegister(RegisterDTO dto);
        public (AuthenticationValidationResult avr, User? u) ValidateLogin(LoginDTO dto);
    }
}
