using Auth.Configuration;
using Auth.DTOs;
using Auth.Entities;
using Auth.Entities.Results;
using Auth.Interfaces.Services;
using Auth.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Auth.Controllers
{
    [Route("auth")]
    public class AuthController : ControllerBase
    {
        private readonly IOptions<Config> _c;
        private readonly IAuthenticationService _authenticationService;
        private readonly IAuthorisationService _authorisationService;
        private readonly ISelfService _selfService;

        public AuthController(
            IOptions<Config> c,
            IAuthenticationService authenticationService,
            IAuthorisationService authorisationService,
            ISelfService selfService
        )
        {
            _c = c;
            _authenticationService = authenticationService;
            _authorisationService = authorisationService;
            _selfService = selfService;
        }

        [HttpGet("self")]
        public async Task<IActionResult> Self()
        {
            try
            {
                // read access_token from HttpOnly cookie
                string? access_token = Request.Cookies["access_token"];
                Response.Cookies.Delete("access_token");
                if (string.IsNullOrWhiteSpace(access_token))
                {
                    return Unauthorized(new APIError { Error = "No access token found" });
                }
                // Check if user is authorised
                AuthoriseResult authoriseResult = await _authorisationService.Authorise(
                    access_token
                );
                if (!string.IsNullOrWhiteSpace(authoriseResult.Error))
                {
                    return Unauthorized(new APIError { Error = authoriseResult.Error });
                }
                if (string.IsNullOrWhiteSpace(authoriseResult.AccessToken))
                {
                    throw new Exception();
                }
                if (authoriseResult.Self == null)
                {
                    throw new Exception();
                }
                /* User is authorised, so create new HttpOnly cookie with existing
                (or potentially refreshed) access_token */
                var cookieOptions = new CookieOptions
                {
                    HttpOnly = true,
                    Expires = DateTime.UtcNow.AddSeconds(
                        _c.Value.Jwt.HttpOnlyCookieExpiresIn.TotalSeconds
                    ),
                    Secure = true,
                    SameSite = SameSiteMode.Strict,
                };
                Response.Cookies.Append("access_token", authoriseResult.AccessToken, cookieOptions);
                return Ok(authoriseResult.Self);
            }
            catch (Exception ex)
            {
                Console.WriteLine("AuthController - Self: " + ex);
                return StatusCode(500, new APIError { Error = "Failed to authorise" });
            }
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDTO registerDTO)
        {
            try
            {
                // Check if DTO attribute constraints violated
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                // Register user
                RegisterResult result = await _authenticationService.Register(registerDTO);
                if (!string.IsNullOrWhiteSpace(result.Error))
                {
                    return Unauthorized(new APIError { Error = result.Error });
                }
                if (result.AccessToken == null)
                {
                    throw new Exception("Authentication service failed to return an access token");
                }
                // Registration successful - create HttpOnly cookie to store access_token
                var cookieOptions = new CookieOptions
                {
                    HttpOnly = true,
                    Expires = DateTime.UtcNow.AddSeconds(
                        _c.Value.Jwt.HttpOnlyCookieExpiresIn.TotalSeconds
                    ),
                    Secure = true,
                    SameSite = SameSiteMode.Strict,
                };
                Response.Cookies.Append("access_token", result.AccessToken, cookieOptions);
                return Ok();
            }
            catch (Exception ex)
            {
                Console.WriteLine("AuthController - Register: " + ex);
                return StatusCode(500, new APIError { Error = "Failed to create account" });
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO loginDTO)
        {
            try
            {
                // Check if DTO attribute constraints violated
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                // Log user in
                LoginResult result = await _authenticationService.Login(loginDTO);
                if (!string.IsNullOrWhiteSpace(result.Error))
                {
                    return BadRequest(new APIError { Error = result.Error });
                }
                if (result.AccessToken == null)
                {
                    throw new Exception("Authentication service failed to return an access token");
                }
                // Login successful - create HttpOnly cookie to store access_token
                var cookieOptions = new CookieOptions
                {
                    HttpOnly = true,
                    Expires = DateTime.UtcNow.AddMinutes(
                        _c.Value.Jwt.HttpOnlyCookieExpiresIn.TotalMinutes
                    ),
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

        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            try
            {
                string? access_token = Request.Cookies["access_token"];
                if (string.IsNullOrWhiteSpace(access_token))
                {
                    return BadRequest("Invalid access token");
                }

                SelfResult selfResult = _selfService.GetSelf(access_token);
                if (!string.IsNullOrWhiteSpace(selfResult.Error))
                {
                    throw new Exception(selfResult.Error);
                }
                if (selfResult.Self == null)
                {
                    throw new Exception("Invalid self");
                }

                bool isLoggedOut = await _authenticationService.Logout(selfResult.Self.UserId);
                if (!isLoggedOut)
                {
                    throw new Exception(
                        "Authentication service failed to complete logout operation"
                    );
                }
                Response.Cookies.Delete("access_token");
                return Ok();
            }
            catch (Exception ex)
            {
                Console.WriteLine("AuthController - Logout: " + ex);
                return StatusCode(500, new APIError { Error = "Failed to logout" });
            }
        }
    }
}
