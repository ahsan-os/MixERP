using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web;
using Frapid.ApplicationState.Cache;
using Frapid.Areas;
using Frapid.Configuration;
using Frapid.Framework;
using Frapid.Web.Application;
using Serilog;

namespace Frapid.Web
{
    public sealed class FrapidApplication : IHttpModule
    {
        public void Init(HttpApplication app)
        {
            app.BeginRequest += this.App_BeginRequest;
            app.EndRequest += this.App_EndRequest;
            app.Error += this.App_Error;
        }

        public void Dispose()
        {
        }

        public void InitializeCulture(HttpContext context)
        {
            string cultureCode = "en-US";
            var cultureCookie = context.Request.Cookies["culture"];
            if (cultureCookie != null)
            {
                cultureCode = cultureCookie.Value;
            }

            var culture = new CultureInfo(cultureCode);

            Thread.CurrentThread.CurrentCulture = culture;
            Thread.CurrentThread.CurrentUICulture = culture;
            CultureInfo.DefaultThreadCurrentCulture = culture;
        }

        private bool IsFont(string url)
        {
            var candidates = new[] {".woff", ".woff2", ".ttf", ".font"};
            string file = Path.GetFileName(url);
            return !string.IsNullOrWhiteSpace(file) && candidates.Any(file.EndsWith);
        }

        private void SetCorsHeaders()
        {
            var context = FrapidHttpContext.GetCurrent();

            if (context == null)
            {
                return;
            }

            bool isFont = this.IsFont(context.Request.PhysicalPath);

            if (!isFont)
            {
                return;
            }

            context.Response.Headers.Set("Access-Control-Allow-Origin", "*");
            context.Response.Headers.Set("Vary", "Origin");
            context.Response.Headers.Set("Access-Control-Allow-Headers", "Content-Type");
            context.Response.Headers.Set("Access-Control-Allow-Methods", "HEAD,GET");
            context.Response.Headers.Set("Access-Control-Allow-Credentials", "true");
        }

        private void App_Error(object sender, EventArgs e)
        {
            var context = FrapidHttpContext.GetCurrent();
            var exception = context.Server.GetLastError();

            if (exception == null)
            {
                return;
            }

            this.LogException(exception);
        }

        private void LogException(Exception ex)
        {
            string tenant = TenantConvention.GetTenant();
            var meta = AppUsers.GetCurrent();

            DefaultExceptionLogger.Log(tenant, meta.UserId, meta.Name, meta.OfficeName, ex.Message);
        }

        private void Handle404Error()
        {
            var context = FrapidHttpContext.GetCurrent();
            int statusCode = context.Response.StatusCode;

            if (statusCode != 404)
            {
                return;
            }

            context.Server.ClearError();
            context.Response.TrySkipIisCustomErrors = true;
            string path = context.Request.Url.AbsolutePath;

            var ignoredPaths = new[]
            {
                "/api",
                "/dashboard",
                "/content-not-found"
            };

            if (!ignoredPaths.Any(x => path.StartsWith(x)))
            {
                context.Server.TransferRequest("/content-not-found?path=" + path, true);
            }
        }

        public void App_EndRequest(object sender, EventArgs e)
        {
            this.SetCorsHeaders();
            this.Handle404Error();
        }

        public void App_BeginRequest(object sender, EventArgs e)
        {
            var context = FrapidHttpContext.GetCurrent();

            if (context == null)
            {
                return;
            }

            this.InitializeCulture(context);

            string domain = TenantConvention.GetDomain();
            Log.Verbose($"Got a {context.Request.HttpMethod} request {context.Request.AppRelativeCurrentExecutionFilePath} on domain {domain}.");

            bool enforceSsl = TenantConvention.EnforceSsl(domain);

            if (!enforceSsl)
            {
                Log.Verbose($"SSL was not enforced on domain {domain}.");
                return;
            }

            if (context.Request.Url.Scheme == "https")
            {
                context.Response.AddHeader("Strict-Transport-Security", "max-age=31536000");
            }
            else if (context.Request.Url.Scheme == "http")
            {
                string path = "https://" + context.Request.Url.Host + context.Request.Url.PathAndQuery;
                context.Response.Status = "301 Moved Permanently";
                context.Response.AddHeader("Location", path);
            }

            this.ServeRequestAsTenantResource();
        }

        /// <summary>
        /// This investigates and serves static resources present in the tenant's wwwroot folder.
        /// </summary>
        private void ServeRequestAsTenantResource()
        {
            string tenant = TenantConvention.GetTenant();
            string file = TenantStaticContentHelper.GetFile(tenant, FrapidHttpContext.GetCurrent());

            if (!string.IsNullOrWhiteSpace(file))
            {
                //We found the requested file on the tenant's "wwwroot" directory.
                FrapidHttpContext.GetCurrent().RewritePath(file);
            }
        }
    }
}