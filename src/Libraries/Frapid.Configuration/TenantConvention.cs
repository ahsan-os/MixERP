using System.Collections.Generic;
using System.Linq;
using System.Web;
using Frapid.Configuration.TenantServices;
using Frapid.Configuration.TenantServices.Contracts;
using Frapid.Framework;
using Serilog;

namespace Frapid.Configuration
{
    public class TenantConvention
    {
        public static string GetBaseDomain(HttpContextBase context, bool includeScheme)
        {
            var approved = GetSerializer();
            string domain = context.Request.Url?.DnsSafeHost;
            bool isHttps = context.Request.IsSecureConnection;

            var locator = new BaseDomainLocator(approved);
            return locator.Get(domain, isHttps, includeScheme);
        }

        public static ApprovedDomain FindDomainByTenant(string tenant)
        {
            var logger = Log.Logger;
            var serializer = GetSerializer();
            var convention = new ByConvention(logger, serializer);

            string defaultTenant = GetDefaultTenantName();

            var finder = new DomainFinder(serializer, convention);
            return finder.FindByTenant(tenant, defaultTenant);
        }

        public static string GetDomain()
        {
            if (FrapidHttpContext.GetCurrent() == null)
            {
                return string.Empty;
            }

            string url = FrapidHttpContext.GetCurrent().Request.Url.Authority;
            var extractor = new DomainNameExtractor(Log.Logger);
            return extractor.GetDomain(url);
        }

        public static bool IsStaticDomain(string domain = "")
        {
            if (string.IsNullOrWhiteSpace(domain))
            {
                domain = GetDomain();
            }

            var logger = Log.Logger;
            var serializer = GetSerializer();

            var check = new StaticDomainCheck(logger, serializer);

            return check.IsStaticDomain(domain);
        }

        public static bool EnforceSsl(string domain = "")
        {
            if (string.IsNullOrWhiteSpace(domain))
            {
                domain = GetDomain();
            }

            var logger = Log.Logger;
            var serializer = GetSerializer();
            var check = new SslDomainCheck(logger, serializer);
            return check.EnforceSsl(domain);
        }

        public static string GetDbNameByConvention(string domain = "")
        {
            var approved = GetSerializer();
            var convention = new ByConvention(Log.Logger, approved);
            string or = GetDomain();
            return convention.GetTenantName(domain, or);
        }

        public static bool IsValidTenant(string tenant = "")
        {
            var logger = Log.Logger;
            var serializer = GetSerializer();
            var convention = new ByConvention(logger, serializer);

            var validator = new TenantValidator(logger, serializer, convention);
            return validator.IsValid(tenant);
        }

        public static bool IsValidDomain(string domain = "")
        {
            if (string.IsNullOrWhiteSpace(domain))
            {
                domain = GetDomain();
                Log.Verbose($"The empty domain was automatically resolved to \"{domain}\".");
            }

            var logger = Log.Logger;
            var serializer = GetSerializer();

            var validator = new DomainValidator(logger, serializer);
            return validator.IsValid(domain);
        }

        public static List<ApprovedDomain> GetDomains()
        {
            var serializer = GetSerializer();
            return serializer.Get();
        }

        public static List<string> GetTenants()
        {
            var serializer = GetSerializer();
            return serializer.Get().Select(member => GetDbNameByConvention(member.DomainName)).ToList();
        }

        public static ApprovedDomain GetSite(string tenant)
        {
            var serializer = GetSerializer();
            var members = serializer.Get();

            var site = members.FirstOrDefault(domain => GetDbNameByConvention(domain.DomainName) == tenant);
            return site;
        }


        public static string GetTenant(string url = "")
        {
            if (string.IsNullOrWhiteSpace(url))
            {
                url = FrapidHttpContext.GetCurrent().Request.Url.Authority;
            }

            var locator = GetTenantLocator();
            string defaultTenant = GetDefaultTenantName();

            return locator.FromUrl(url, defaultTenant);
        }

        #region Locator

        public static DomainSerializer GetSerializer()
        {
            return new ApprovedDomainSerializer();
        }

        public static ITenantLocator GetTenantLocator()
        {
            var logger = Log.Logger;
            var serializer = GetSerializer();
            var byConvention = new ByConvention(logger, serializer);
            var extractor = new DomainNameExtractor(logger);
            var validator = new TenantValidator(logger, serializer, byConvention);
            var investigator = new TenantLocator(logger, extractor, byConvention, validator, serializer);
            return investigator;
        }

        public static List<string> GetTenantMembers(string tenant)
        {
            var site = GetSite(tenant);
            return site.GetSubtenants();
        }

        public static string GetDefaultTenantName()
        {
            return System.Configuration.ConfigurationManager.AppSettings["DefaultTenant"];
        }

        #endregion
    }
}