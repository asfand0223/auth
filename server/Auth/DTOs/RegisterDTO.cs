using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Auth.DTOs
{
    public class RegisterDTO
    {
        [JsonPropertyName("username")]
        [JsonRequired]
        [Required(ErrorMessage = "Username is required")]
        public required string Username { get; set; }

        [JsonRequired]
        [JsonPropertyName("password")]
        [Required(ErrorMessage = "Password is required")]
        [MinLength(8, ErrorMessage = "Password must be at least 8 characters")]
        public required string Password { get; set; }

        [JsonRequired]
        [JsonPropertyName("confirm_password")]
        [Required(ErrorMessage = "Confirm Password is required")]
        [MinLength(8, ErrorMessage = "Confirm Password must be at least 8 characters")]
        public required string ConfirmPassword { get; set; }
    }
}
