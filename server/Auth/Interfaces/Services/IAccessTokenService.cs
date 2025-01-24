namespace Auth.Interfaces.Services
{
    public interface IAccessTokenService
    {
        public string Generate(Guid userId, string username);
    }
}
