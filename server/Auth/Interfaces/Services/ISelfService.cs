using Auth.Entities;

namespace Auth.Interfaces.Services
{
    public interface ISelfService
    {
        public Self Generate(Guid userId, string username);
    }
}
