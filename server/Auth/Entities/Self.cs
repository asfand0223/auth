using System.Text.Json.Serialization;

namespace Auth.Entities
{
    public class Self
    {
        [JsonPropertyName("user_id")]
        public Guid UserId { get; set; }

        [JsonPropertyName("username")]
        public required string Username { get; set; }
    }
}
