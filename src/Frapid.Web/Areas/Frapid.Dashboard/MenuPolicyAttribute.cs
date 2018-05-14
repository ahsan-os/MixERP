using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Frapid.ApplicationState.Cache;
using Frapid.ApplicationState.CacheFactory;
using Frapid.Configuration;
using Frapid.Dashboard.DAL;
using Frapid.Dashboard.DTO;
using Frapid.Framework.Extensions;

namespace Frapid.Dashboard
{
    public class MenuPolicyAttribute : ActionFilterAttribute
    {
        public string OverridePath { get; set; }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            string path = this.OverridePath.Or(filterContext.HttpContext.Request.FilePath);

            var meta = AppUsers.GetCurrentAsync().GetAwaiter().GetResult();
            int userId = meta.UserId;
            int officeId = meta.OfficeId;

            string tenant = TenantConvention.GetTenant();

            var policy = this.GetAllMenus(tenant, userId, officeId);

            if (policy.Any(x => x.Url.Equals(path)))
            {
                return;
            }

            filterContext.Result = new HttpUnauthorizedResult(Resources.AccessIsDenied);
        }

        private IEnumerable<Menu> GetAllMenus(string tenant, int userId, int officeId)
        {
            string key = $"menu_policy_{tenant}_{officeId}_{userId}";

            var factory = new DefaultCacheFactory();
            var policy = factory.Get<List<Menu>>(key);

            if (policy != null && policy.Any())
            {
                return policy;
            }

            policy = this.FromStore(tenant, userId, officeId).ToList();
            factory.Add(key, policy, DateTimeOffset.UtcNow.AddSeconds(GetTotalCacheDuration(tenant)));

            return policy;
        }

        private IEnumerable<Menu> FromStore(string tenant, int userId, int officeId)
        {
            var policy = Menus.GetAsync(tenant, userId, officeId).GetAwaiter().GetResult();
            return policy;
        }

        private static int GetTotalCacheDuration(string tenant)
        {
            string configFile = PathMapper.MapPath($"~/Tenants/{tenant}/Configs/Frapid.config");
            string config = !File.Exists(configFile) ? string.Empty : ConfigurationManager.ReadConfigurationValue(configFile, "MenuPolicyCacheDurationInSeconds");
            return string.IsNullOrWhiteSpace(config) ? 60 : config.To<int>();
        }
    }
}