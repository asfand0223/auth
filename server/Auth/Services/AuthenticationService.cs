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

        public AuthenticationService(IAuthenticationValidationService avs, IUserService us)
        {
            _avs = avs;
            _us = us;
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
                new RegisterResult
                {
                    Id = id.Value,
                    Username = dto.Username,
                    AccessToken = "",
                }
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
