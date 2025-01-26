using Auth.Entities;
using Auth.Interfaces.Services;

namespace Auth.Services
{
    public class SelfService : ISelfService
    {
        public Self Generate(Guid userId, string username)
        {
            return new Self { UserId = userId, Username = username };
        }
    }
}
