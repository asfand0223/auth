using System.Text.Json.Serialization;

namespace Auth.Results
{
    public class AuthenticationResult
    {
        [JsonPropertyName("data")]
        [JsonRequired]
        public string? Data { get; set; }

        [JsonPropertyName("error")]
        [JsonRequired]
        public string? Error { get; set; }
    }
}
