using System;

namespace API.OAuth
{
    public class JwtConfig
    {
        public string Issuer { get; set; }
        public string SecretKey { get; set; }
        public string AudienceId { get; set; }
        public DateTime Lifetime { get; set; }
    }
}
