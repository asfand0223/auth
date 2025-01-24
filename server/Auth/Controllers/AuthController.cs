using Auth.Configuration;
using Auth.DTOs;
using Auth.Entities;
using Auth.Entities.Results;
using Auth.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Auth.Controllers
{
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IOptions<Config> _c;
        private readonly IAuthenticationService _authenticationService;

        public AuthController(IOptions<Config> c, IAuthenticationService authenticationService)
        {
            _c = c;
            _authenticationService = authenticationService;
        }

        [HttpPost("/register")]
        public async Task<IActionResult> Register([FromBody] RegisterDTO registerDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                RegisterResult result = await _authenticationService.Register(registerDTO);

                if (!string.IsNullOrWhiteSpace(result.Error))
                {
                    return Unauthorized(new APIError { Error = result.Error });
                }
                if (result.AccessToken == null)
                {
                    throw new Exception("Failed to generate access token");
                }

                var cookieOptions = new CookieOptions
                {
                    HttpOnly = true,
                    Expires = DateTime.Now.AddMinutes(_c.Value.Jwt.Expires.TotalMinutes),
                    Secure = true,
                    SameSite = SameSiteMode.Strict,
                };
                Response.Cookies.Append("access_token", result.AccessToken, cookieOptions);
                return Ok();
            }
            catch (Exception ex)
            {
                Console.WriteLine("UserController - Register: " + ex);
                return StatusCode(500, new APIError { Error = "Failed to create account" });
            }
        }

        [HttpPost("/login")]
        public IActionResult Login([FromBody] LoginDTO loginDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                LoginResult result = _authenticationService.Login(loginDTO);

                if (!string.IsNullOrWhiteSpace(result.Error))
                {
                    return Unauthorized(new APIError { Error = result.Error });
                }
                if (result.AccessToken == null)
                {
                    throw new Exception("Failed to generate access token");
                }

                var cookieOptions = new CookieOptions
                {
                    HttpOnly = true,
                    Expires = DateTime.Now.AddMinutes(_c.Value.Jwt.Expires.TotalMinutes),
                    Secure = true,
                    SameSite = SameSiteMode.Strict,
                };
                Response.Cookies.Append("access_token", result.AccessToken, cookieOptions);
                return Ok();
            }
            catch (Exception ex)
            {
                Console.WriteLine("AuthController - Login: " + ex);
                return StatusCode(500, new APIError { Error = "Failed to login" });
            }
        }
    }
}
