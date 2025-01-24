using System.Text.Json.Serialization;

namespace Auth.Entities
{
    public class APIError
    {
        [JsonPropertyName("error")]
        public required string Error { get; set; }
    }
}
