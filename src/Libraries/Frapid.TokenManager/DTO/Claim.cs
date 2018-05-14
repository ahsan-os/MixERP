using System.Collections.Generic;
using System.Security.Claims;

namespace Frapid.TokenManager.DTO
{
    public sealed class Claim
    {
        public string Issuer { get; set; }
        public string OriginalIssuer { get; set; }
        public IDictionary<string, string> Properties { get; set; }
        public ClaimsIdentity Subject { get; set; }
        public string Type { get; set; }
        public string Value { get; set; }
        public string ValueType { get; set; }
    }
}