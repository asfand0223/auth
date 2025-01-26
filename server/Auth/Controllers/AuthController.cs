using Auth.Configuration;
using Auth.DTOs;
using Auth.Entities;
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
        private readonly IAuthorisationService __authorisationService;

        public AuthController(
            IOptions<Config> c,
            IAuthenticationService authenticationService,
            IAuthorisationService authorisationService
        )
        {
            _c = c;
            _authenticationService = authenticationService;
            __authorisationService = authorisationService;
        }

        [HttpGet("self")]
        public IActionResult Self()
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
                AuthoriseResult authoriseResult = __authorisationService.Authorise(access_token);
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
                    Expires = DateTime.UtcNow.AddSeconds(_c.Value.Jwt.ExpiresIn.TotalSeconds),
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
                    throw new Exception("Failed to generate access token");
                }
                // Registration successful - create HttpOnly cookie to store access_token
                var cookieOptions = new CookieOptions
                {
                    HttpOnly = true,
                    Expires = DateTime.UtcNow.AddSeconds(_c.Value.Jwt.ExpiresIn.TotalSeconds),
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
                    throw new Exception("Failed to generate access token");
                }
                // Login successful - create HttpOnly cookie to store access_token
                var cookieOptions = new CookieOptions
                {
                    HttpOnly = true,
                    Expires = DateTime.UtcNow.AddMinutes(_c.Value.Jwt.ExpiresIn.TotalMinutes),
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
