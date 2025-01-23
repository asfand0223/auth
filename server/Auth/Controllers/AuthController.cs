using Auth.DTOs;
using Auth.Interfaces;
using Auth.Results;
using Auth.Utils;
using Microsoft.AspNetCore.Mvc;

namespace Auth.Controllers
{
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthenticationService _authService;

        public AuthController(IAuthenticationService authService)
        {
            _authService = authService;
        }

        [HttpPost("/login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO loginDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                AuthenticationResult result = await _authService.Login(loginDTO);
                string json = await Json.Write<AuthenticationResult>(result);
                if (!string.IsNullOrWhiteSpace(result.Error))
                {
                    return Unauthorized(json);
                }

                return Ok(json);
            }
            catch (Exception ex)
            {
                Console.WriteLine("AuthController - Login: " + ex);
                return StatusCode(500, "Failed to login");
            }
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

                AuthenticationResult result = await _authService.Register(registerDTO);
                string json = await Json.Write<AuthenticationResult>(result);
                if (!string.IsNullOrWhiteSpace(result.Error))
                {
                    return BadRequest(json);
                }

                return Ok(json);
            }
            catch (Exception ex)
            {
                Console.WriteLine("UserController - Register: " + ex);
                return StatusCode(500, "Failed to register user");
            }
        }
    }
}
