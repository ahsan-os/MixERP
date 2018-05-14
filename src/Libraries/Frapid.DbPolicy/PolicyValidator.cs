using System;
using System.IO;
using System.Threading.Tasks;
using Frapid.ApplicationState.CacheFactory;
using Frapid.Configuration;
using Frapid.DataAccess;
using Frapid.DataAccess.Models;
using Frapid.Framework.Extensions;

namespace Frapid.DbPolicy
{
    public class PolicyValidator : IPolicy
    {
        public AccessTypeEnum AccessType { get; set; }
        public string ObjectNamespace { get; set; }
        public string ObjectName { get; set; }
        public bool HasAccess { get; private set; }
        public long LoginId { get; set; }
        public string Tenant { get; set; }

        public async Task ValidateAsync()
        {
            string key = $"access_policy_{this.Tenant}_{this.ObjectNamespace}_{this.ObjectName}_{this.LoginId}";

            var factory = new DefaultCacheFactory();
            var policy = factory.Get<dynamic>(key);

            if (policy != null)
            {
                this.HasAccess = policy.HasAccess;
                return;
            }

            this.HasAccess = await FromStore(this).ConfigureAwait(false);
            factory.Add(key, new {this.HasAccess}, DateTimeOffset.UtcNow.AddSeconds(GetTotalCacheDuration(this.Tenant)));
        }

        private static int GetTotalCacheDuration(string tenant)
        {
            string configFile = PathMapper.MapPath($"~/Tenants/{tenant}/Configs/Frapid.config");
            string config = !File.Exists(configFile) ? string.Empty : ConfigurationManager.ReadConfigurationValue(configFile, "AccessPolicyCacheDurationInSeconds");

            return string.IsNullOrWhiteSpace(config) ? 60 : config.To<int>();
        }

        private static async Task<bool> FromStore(IPolicy policy)
        {
            if (policy.LoginId == 0)
            {
                return false;
            }

            string sql = FrapidDbServer.GetProcedureCommand(policy.Tenant, "auth.has_access", new[] {"@0", "@1", "@2"});

            string entity = policy.ObjectNamespace + "." + policy.ObjectName;
            int type = (int) policy.AccessType;

            bool result = await Factory.ScalarAsync<bool>(policy.Tenant, sql, policy.LoginId, entity, type).ConfigureAwait(false);
            return result;
        }
    }
}