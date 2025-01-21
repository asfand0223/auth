using System.Text.Json.Serialization;

namespace Auth.Types
{
    public class RegisterResult
    {
        [JsonPropertyName("id")]
        [JsonRequired]
        public Guid Id { get; set; }

        [JsonPropertyName("username")]
        [JsonRequired]
        public required string Username { get; set; }

        [JsonPropertyName("access_token")]
        [JsonRequired]
        public required string AccessToken { get; set; }
    }
}
