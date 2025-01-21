using Auth.DTOs;
using Auth.Types;

namespace Auth.Interfaces
{
    public interface IAuthService
    {
        public Task<AuthResult> Register(RegisterDTO registerDTO);
        public Task<AuthResult> Login(LoginDTO loginDTO);
    }
}
