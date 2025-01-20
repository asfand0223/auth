using Auth.DTOs;
using Auth.Interfaces;
using Auth.Models;
using Auth.Result;
using Microsoft.AspNetCore.Mvc;

namespace Auth.Controllers
{
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            try
            {
                Console.WriteLine("Id: " + id.ToString());

                User? user = await _userRepository.Get(id);
                if (user == null)
                {
                    return NotFound("User not found");
                }
                return Ok(user);
            }
            catch (Exception ex)
            {
                Console.WriteLine("UserController - Get: " + ex);
                return StatusCode(500, "Failed to get user");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateUserDTO createUserDTO)
        {
            try
            {
                Guid id = Guid.NewGuid();
                User user = new User
                {
                    Id = id,
                    Username = createUserDTO.Username,
                    CreatedOn = DateTime.UtcNow,
                };
                bool created = await _userRepository.Create(user);
                if (!created)
                    return StatusCode(500, "Failed to create user");
                return Ok(new CreateUserResult { Id = id });
            }
            catch (Exception ex)
            {
                Console.WriteLine("UserController - Create: " + ex);
                return StatusCode(500, "Failed to create user");
            }
        }
    }
}
