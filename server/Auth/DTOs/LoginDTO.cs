using System.Text.Json.Serialization;

namespace Auth.DTOs
{
    public class LoginDTO
    {
        [JsonPropertyName("username")]
        [JsonRequired]
        public required string Username { get; set; }

        [JsonPropertyName("password")]
        [JsonRequired]
        public required string Password { get; set; }
    }
}
