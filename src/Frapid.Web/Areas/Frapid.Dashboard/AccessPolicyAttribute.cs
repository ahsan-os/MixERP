using System.Web.Mvc;
using Frapid.ApplicationState.Cache;
using Frapid.Configuration;
using Frapid.DataAccess.Models;
using Frapid.DbPolicy;

namespace Frapid.Dashboard
{
    public class AccessPolicyAttribute : ActionFilterAttribute
    {
        public AccessPolicyAttribute(string objectNamespace, string objectName, AccessTypeEnum type)
        {
            this.ObjectNamespace = objectNamespace;
            this.ObjectName = objectName;
            this.Type = type;
        }

        public string ObjectNamespace { get; }
        public string ObjectName { get; }
        public AccessTypeEnum Type { get; }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var meta = AppUsers.GetCurrentAsync().GetAwaiter().GetResult();
            string tenant = TenantConvention.GetTenant();

            var policy = new PolicyValidator
            {
                ObjectNamespace = this.ObjectNamespace,
                ObjectName = this.ObjectName,
                LoginId = meta.LoginId,
                Tenant = tenant,
                AccessType = this.Type
            };

            policy.ValidateAsync().GetAwaiter().GetResult();
            bool hasAccess = policy.HasAccess;

            if (hasAccess)
            {
                return;
            }

            filterContext.Result = new HttpUnauthorizedResult(Resources.AccessIsDenied);
        }
    }
}