using System.Text.Json.Serialization;

namespace Auth.Types
{
    public class AuthResult
    {
        [JsonPropertyName("data")]
        [JsonRequired]
        public string? Data { get; set; }

        [JsonPropertyName("error")]
        [JsonRequired]
        public string? Error { get; set; }
    }
}
