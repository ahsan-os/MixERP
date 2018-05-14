using System.Linq;
using Frapid.Configuration.TenantServices.Contracts;
using Serilog;

namespace Frapid.Configuration.TenantServices
{
    public class DomainNameExtractor : IDomainNameExtractor
    {
        public DomainNameExtractor(ILogger logger)
        {
            this.Logger = logger;
        }

        public ILogger Logger { get; }

        public string GetDomain(string url)
        {
            if (string.IsNullOrWhiteSpace(url))
            {
                this.Logger.Information("Cannot extract the domain name because the url was was null.");
                return string.Empty;
            }

            if (url.StartsWith("http"))
            {
                url = url.Replace("http://", "").Replace("https://", "");
            }

            if (url.StartsWith("www."))
            {
                url = url.Replace("www.", "");
            }

            url = url.Split('/').FirstOrDefault() ?? url;

            return url;
        }
    }
}