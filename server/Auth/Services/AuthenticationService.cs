using Auth.DTOs;
using Auth.Interfaces.Services;
using Auth.Models;
using Auth.Results;

namespace Auth.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IAuthenticationValidationService _authenticationValidationService;
        private readonly IUserService _userService;
        private readonly IAccessTokenService _acessTokenService;

        public AuthenticationService(
            IAuthenticationValidationService authenticationValidationService,
            IUserService userService,
            IAccessTokenService accessTokenService
        )
        {
            _authenticationValidationService = authenticationValidationService;
            _userService = userService;
            _acessTokenService = accessTokenService;
        }

        public async Task<RegisterResult> Register(RegisterDTO dto)
        {
            RegisterResult result = new RegisterResult { AccessToken = null, Error = null };
            ValidateRegisterResult validateRegisterResult =
                _authenticationValidationService.ValidateRegister(dto);
            if (validateRegisterResult.Error != null)
            {
                result.Error = validateRegisterResult.Error;
                return result;
            }

            Guid? id = await _userService.Create(dto.Username, dto.Password);
            if (id == null)
            {
                result.Error = "Failed to create user";
                return result;
            }
            result.AccessToken = _acessTokenService.Generate(id.Value, dto.Username);
            return result;
        }

        public LoginResult Login(LoginDTO dto)
        {
            LoginResult result = new LoginResult { AccessToken = null, Error = null };
            ValidateLoginResult validateLoginResult =
                _authenticationValidationService.ValidateLogin(dto);
            if (validateLoginResult.Error != null)
            {
                result.Error = validateLoginResult.Error;
                return result;
            }

            User? user = _userService.GetByUsername(dto.Username);
            if (user == null)
            {
                result.Error = "User not found";
                return result;
            }
            result.AccessToken = _acessTokenService.Generate(user.Id, dto.Username);
            return result;
        }
    }
}
