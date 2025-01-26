using Auth.Results;

namespace Auth.Interfaces.Services
{
    public interface IAuthorisationService
    {
        public Task<AuthoriseResult> Authorise(string access_token);
    }
}
