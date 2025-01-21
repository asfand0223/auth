using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Auth.DTOs
{
    public class RegisterDTO
    {
        [JsonPropertyName("username")]
        [JsonRequired]
        [Required]
        public required string Username { get; set; }

        [JsonRequired]
        [JsonPropertyName("password")]
        [Required]
        [MinLength(4)]
        public required string Password { get; set; }

        [JsonRequired]
        [JsonPropertyName("confirm_password")]
        [Required]
        [MinLength(4)]
        public required string ConfirmPassword { get; set; }
    }
}
