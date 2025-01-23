namespace Auth.Configuration
{
    public class Config
    {
        public required ConnectionStrings ConnectionStrings { get; set; }
        public required Jwt Jwt { get; set; }
        public required KestrelSettings Kestrel { get; set; }
    }

    public class ConnectionStrings
    {
        public required string DefaultConnection { get; set; }
    }

    public class Jwt
    {
        public required string Key { get; set; }
        public required string Issuer { get; set; }
        public required string Audience { get; set; }
        public TimeSpan Expires { get; set; }
    }

    public class KestrelSettings
    {
        public required Endpoints Endpoints { get; set; }
    }

    public class Endpoints
    {
        public required HttpEndpoint Http { get; set; }
    }

    public class HttpEndpoint
    {
        public required string Url { get; set; }
    }
}
