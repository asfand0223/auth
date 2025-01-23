using System.Text.Json.Serialization;

namespace Auth.Results
{
    public class LoginResult
    {
        [JsonPropertyName("access_token")]
        [JsonRequired]
        public required string AccessToken { get; set; }
    }
}
