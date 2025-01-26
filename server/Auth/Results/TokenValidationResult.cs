using System.Security.Claims;

namespace Auth.Results
{
    public class TokenValidationResult
    {
        public string? AccessToken { get; set; }
        public required List<Claim> Claims { get; set; }
        public bool Valid { get; set; }
        public bool Expired { get; set; }
    }
}
