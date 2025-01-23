using Auth.DTOs;
using Auth.Interfaces;
using Auth.Models;
using Auth.Results;
using Auth.Utils;

namespace Auth.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IAuthenticationValidationService _avs;
        private readonly IUserService _us;
        private readonly IAccessTokenService _ats;

        public AuthenticationService(
            IAuthenticationValidationService avs,
            IUserService us,
            IAccessTokenService ats
        )
        {
            _avs = avs;
            _us = us;
            _ats = ats;
        }

        public async Task<AuthenticationResult> Register(RegisterDTO dto)
        {
            AuthenticationValidationResult atvr = _avs.ValidateRegister(dto);
            if (atvr.Error != null)
            {
                return new AuthenticationResult { Error = atvr.Error };
            }

            Guid? id = await _us.Create(dto.Username, dto.Password);
            if (id == null)
            {
                return new AuthenticationResult { Error = "Failed to create user" };
            }

            string json = await Json.Write<RegisterResult>(
                new RegisterResult { AccessToken = _ats.Generate(id.Value, dto.Username) }
            );
            return new AuthenticationResult { Data = json };
        }

        public async Task<AuthenticationResult> Login(LoginDTO dto)
        {
            (AuthenticationValidationResult avr, User? u) result = _avs.ValidateLogin(dto);
            if (result.avr.Error != null || result.u == null)
            {
                return new AuthenticationResult { Error = result.avr.Error };
            }

            string json = await Json.Write<LoginResult>(
                new LoginResult { AccessToken = _ats.Generate(result.u.Id, dto.Username) }
            );
            return new AuthenticationResult { Data = json };
        }
    }
}
