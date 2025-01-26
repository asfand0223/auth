using Auth.Entities;
using Auth.Entities.Results;

namespace Auth.Interfaces.Services
{
    public interface ISelfService
    {
        public Self? CreateSelf(Guid userId, string username);
        public SelfResult GetSelf(string accessToken);
    }
}
