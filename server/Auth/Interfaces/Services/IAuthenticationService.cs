using Auth.DTOs;
using Auth.Results;

namespace Auth.Interfaces.Services
{
    public interface IAuthenticationService
    {
        public Task<RegisterResult> Register(RegisterDTO registerDTO);
        public LoginResult Login(LoginDTO loginDTO);
    }
}
