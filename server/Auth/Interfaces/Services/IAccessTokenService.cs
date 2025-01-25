using AR = Auth.Results;

namespace Auth.Interfaces.Services
{
    public interface IAccessTokenService
    {
        public string Generate(Guid userId, string username);
        public AR.TokenValidationResult Validate(string access_token);
    }
}
