using System;

namespace Frapid.TokenManager.DTO
{
    public sealed class AccessToken
    {
        public Guid AccessTokenId { get; set; }
        public DateTimeOffset CreatedOn { get; set; }
        public DateTimeOffset? ExpiresOn { get; set; }
        public bool Revoked { get; set; }
        public string IpAddress { get; set; }
        public string UserAgent { get; set; }
        public string ClientToken { get; set; }
    }
}