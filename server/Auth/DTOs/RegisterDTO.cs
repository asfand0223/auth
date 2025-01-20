using System.Text.Json.Serialization;

namespace Auth.DTOs
{
    public class RegisterDTO
    {
        [JsonPropertyName("username")]
        [JsonRequired]
        public required string Username { get; set; }

        [JsonRequired]
        [JsonPropertyName("password")]
        public required string Password { get; set; }

        [JsonRequired]
        [JsonPropertyName("confirm_password")]
        public required string ConfirmPassword { get; set; }
    }
}
