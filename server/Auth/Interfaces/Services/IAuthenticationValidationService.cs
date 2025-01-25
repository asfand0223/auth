using Auth.DTOs;
using Auth.Results;

namespace Auth.Interfaces.Services
{
    public interface IAuthenticationValidationService
    {
        public ValidateRegisterResult ValidateRegister(RegisterDTO dto);
        public ValidateLoginResult ValidateLogin(LoginDTO dto);
    }
}
