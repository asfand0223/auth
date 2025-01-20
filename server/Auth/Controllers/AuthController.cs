using Auth.DTOs;
using Auth.Interfaces;
using Auth.Models;
using Auth.Result;
using Microsoft.AspNetCore.Mvc;

namespace Auth.Controllers
{
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public AuthController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpPost("/login")]
        public IActionResult Login([FromBody] LoginDTO loginDTO)
        {
            try
            {
                if (
                    string.IsNullOrWhiteSpace(loginDTO.Username)
                    || string.IsNullOrWhiteSpace(loginDTO.Password)
                )
                {
                    return BadRequest("Invalid username or password");
                }
                User? existingUser = _userRepository.GetByUsername(loginDTO.Username);
                if (existingUser == null)
                {
                    return Unauthorized("Invalid username or password");
                }
                if (loginDTO.Password != existingUser.Password)
                {
                    return Unauthorized("Invalid username or password");
                }
                return Ok(
                    new LoginResult { Id = existingUser.Id, Username = existingUser.Username }
                );
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
                if (
                    string.IsNullOrWhiteSpace(registerDTO.Username)
                    || string.IsNullOrWhiteSpace(registerDTO.Password)
                    || string.IsNullOrWhiteSpace(registerDTO.ConfirmPassword)
                )
                {
                    return BadRequest("Missing fields");
                }
                if (registerDTO.Password != registerDTO.ConfirmPassword)
                {
                    return BadRequest("Passwords do not match");
                }

                Guid id = Guid.NewGuid();
                User user = new User
                {
                    Id = id,
                    Username = registerDTO.Username,
                    Password = registerDTO.Password,
                    CreatedOn = DateTime.UtcNow,
                };
                bool created = await _userRepository.Create(user);
                if (!created)
                {
                    return StatusCode(500, "Registration unsuccessful");
                }
                return Ok(new RegisterResult { Id = id, Username = registerDTO.Username });
            }
            catch (Exception ex)
            {
                Console.WriteLine("UserController - Register: " + ex);
                return StatusCode(500, "Failed to register user");
            }
        }
    }
}
