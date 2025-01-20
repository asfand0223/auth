using System.Text.Json.Serialization;

namespace Auth.DTOs
{
    public class CreateUserDTO
    {
        [JsonPropertyName("username")]
        public required string Username { get; set; }
    }
}
