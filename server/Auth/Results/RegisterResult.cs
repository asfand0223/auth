using System.Text.Json.Serialization;

namespace Auth.Results
{
    public class RegisterResult
    {
        [JsonPropertyName("access_token")]
        [JsonRequired]
        public required string AccessToken { get; set; }
    }
}
