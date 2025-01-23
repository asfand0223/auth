using Auth.DTOs;
using Auth.Results;

namespace Auth.Interfaces
{
    public interface IAuthenticationService
    {
        public Task<AuthenticationResult> Register(RegisterDTO registerDTO);
        public Task<AuthenticationResult> Login(LoginDTO loginDTO);
    }
}
