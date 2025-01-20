using System.Text.Json.Serialization;

namespace Auth.DTOs
{
    public class UpdateUserDTO
    {
        [JsonPropertyName("refresh_token")]
        public required string RefreshToken { get; set; }
    }
}
