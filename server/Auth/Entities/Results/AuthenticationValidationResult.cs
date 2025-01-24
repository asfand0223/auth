namespace Auth.Entities.Results
{
    public class AuthenticationValidationResult
    {
        public string? AccessToken { get; set; }

        public string? Error { get; set; }
    }
}
