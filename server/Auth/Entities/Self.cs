using System.Text.Json.Serialization;

namespace Auth.Entities
{
    public class Self
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }

        [JsonPropertyName("username")]
        public required string Username { get; set; }
    }
}
