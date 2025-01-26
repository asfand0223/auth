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
        private readonly IRefreshTokenService _refreshTokenService;

        public AuthenticationService(
            IAuthenticationValidationService authenticationValidationService,
            IUserService userService,
            IAccessTokenService accessTokenService,
            IRefreshTokenService refreshTokenService
        )
        {
            _authenticationValidationService = authenticationValidationService;
            _userService = userService;
            _acessTokenService = accessTokenService;
            _refreshTokenService = refreshTokenService;
        }

        public async Task<RegisterResult> Register(RegisterDTO dto)
        {
            // Prepare result object to return
            RegisterResult result = new RegisterResult { };
            // Validate registration details
            ValidateRegisterResult validateRegisterResult =
                _authenticationValidationService.ValidateRegister(dto);
            if (validateRegisterResult.Error != null)
            {
                result.Error = validateRegisterResult.Error;
                return result;
            }
            // Details validated - create user in db
            Guid? userId = await _userService.Create(dto.Username, dto.Password);
            if (userId == null)
            {
                result.Error = "Failed to create user";
                return result;
            }
            // User created - create refresh token in db
            Guid? refreshTokenId = await _refreshTokenService.Create(userId.Value);
            if (refreshTokenId == null)
            {
                result.Error = "Failed to create refresh token";
                return result;
            }

            result.AccessToken = _acessTokenService.Generate(userId.Value, dto.Username);
            return result;
        }

        public async Task<LoginResult> Login(LoginDTO dto)
        {
            // Prepare result object to return
            LoginResult result = new LoginResult { };
            // Validate login details
            ValidateLoginResult validateLoginResult =
                _authenticationValidationService.ValidateLogin(dto);
            if (validateLoginResult.Error != null)
            {
                result.Error = validateLoginResult.Error;
                return result;
            }
            // Details validated - get user for access token claims construction
            User? user = _userService.GetByUsername(dto.Username);
            if (user == null)
            {
                result.Error = "User not found";
                return result;
            }
            // User exists and is logged in - create refresh token in db
            Guid? refreshTokenId = await _refreshTokenService.Create(user.Id);
            if (refreshTokenId == null)
            {
                result.Error = "Failed to create refresh token";
                return result;
            }

            result.AccessToken = _acessTokenService.Generate(user.Id, dto.Username);
            return result;
        }
    }
}
