using System.Text.Json.Serialization;

namespace Auth.Result
{
    public class CreateUserResult
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }
    }
}
