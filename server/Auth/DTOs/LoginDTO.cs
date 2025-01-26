using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Auth.DTOs
{
    public class LoginDTO
    {
        [JsonPropertyName("username")]
        [JsonRequired]
        [Required(ErrorMessage = "Username is required")]
        public required string Username { get; set; }

        [JsonPropertyName("password")]
        [JsonRequired]
        [Required(ErrorMessage = "Password is required")]
        public required string Password { get; set; }
    }
}
