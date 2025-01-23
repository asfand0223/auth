namespace Auth.Interfaces
{
    public interface IAccessTokenService
    {
        public string Generate(Guid userId, string username);
    }
}
