using Auth.Entities;

namespace Auth.Results
{
    public class LoginResult
    {
        public string? AccessToken { get; set; }
        public Self? Self { get; set; }
        public string? Error { get; set; }
    }
}
