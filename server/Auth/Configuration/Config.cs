namespace Auth.Configuration
{
    public class Config
    {
        public required ConnectionStrings ConnectionStrings { get; set; }
        public KestrelSettings? Kestrel { get; set; }
    }

    public class ConnectionStrings
    {
        public required string DefaultConnection { get; set; }
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
