using System;

namespace AbiokaApi.Infrastructure.Common.Authentication
{
    public class TokenPayload
    {
        public string iss { get; set; }

        public int exp { get; set; }

        public int iat { get; set; }

        public string email { get; set; }

        public Guid id { get; set; }

        public string provider { get; set; }

        public string refresh_token { get; set; }

        public string language { get; set; }

        public string[] roles { get; set; }
    }
}