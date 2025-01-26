using Auth.DTOs;
using Auth.Results;

namespace Auth.Interfaces.Services
{
    public interface IAuthenticationValidationService
    {
        public Task<ValidateRegisterResult> ValidateRegister(RegisterDTO dto);
        public Task<ValidateLoginResult> ValidateLogin(LoginDTO dto);
    }
}
