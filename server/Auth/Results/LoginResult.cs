using System.Text.Json.Serialization;

namespace Auth.Results
{
    public class LoginResult
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
