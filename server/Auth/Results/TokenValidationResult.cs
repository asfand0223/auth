namespace Auth.Results
{
    public class TokenValidationResult
    {
        public bool Valid { get; set; }
        public bool Expired { get; set; }
    }
}
