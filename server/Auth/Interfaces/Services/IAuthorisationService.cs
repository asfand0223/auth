using Auth.Results;

namespace Auth.Interfaces.Services
{
    public interface IAuthorisationService
    {
        public AuthoriseResult Authorise(string access_token);
    }
}
