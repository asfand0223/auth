using System.Text.Json;
using Auth.DTOs;
using Auth.Interfaces;
using Auth.Types;
using Microsoft.AspNetCore.Mvc;

namespace Auth.Controllers
{
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
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
                AuthResult result = await _authService.Login(loginDTO);
                await using MemoryStream stream = new MemoryStream();
                using var reader = new StreamReader(stream);
                await JsonSerializer.SerializeAsync(stream, result);
                stream.Position = 0;
                string json = await reader.ReadToEndAsync();
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
                AuthResult result = await _authService.Register(registerDTO);
                await using MemoryStream stream = new MemoryStream();
                using var reader = new StreamReader(stream);
                await JsonSerializer.SerializeAsync(stream, result);
                stream.Position = 0;
                string json = await reader.ReadToEndAsync();
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
