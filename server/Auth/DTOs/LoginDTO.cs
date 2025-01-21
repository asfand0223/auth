using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Auth.DTOs
{
    public class LoginDTO
    {
        [JsonPropertyName("username")]
        [JsonRequired]
        [Required]
        public required string Username { get; set; }

        [JsonPropertyName("password")]
        [JsonRequired]
        [Required]
        public required string Password { get; set; }
    }
}
