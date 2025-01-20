using System.Text.Json.Serialization;

namespace Auth.Result
{
    public class LoginResult
    {
        [JsonPropertyName("id")]
        [JsonRequired]
        public Guid Id { get; set; }

        [JsonPropertyName("username")]
        [JsonRequired]
        public required string Username { get; set; }
    }
}
