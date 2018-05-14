using System;
using System.Web;
using System.Web.Http;
using System.Web.Http.Dispatcher;
using Frapid.WebApi;
using Serilog;

namespace Frapid.Web.Application
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            Log.Information("Registering Web API.");
            config.SuppressDefaultHostAuthentication();
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute
                (
                    "VersionedApi",
                    "api/v1.0/{schema}/{controller}/{action}/{id}",
                    new
                    {
                        id = RouteParameter.Optional
                    });

            config.Routes.MapHttpRoute
                (
                    "DefaultApi",
                    "api/{schema}/{controller}/{action}/{id}",
                    new
                    {
                        id = RouteParameter.Optional
                    });

            if (FrapidApiController.GetMembers() != null)
            {
                var version = HttpRuntime.IISVersion;

                if (version != null && version < new Version("8.0.0.0"))
                {
                    config.Services.Replace(typeof(IAssembliesResolver), new ClassicAssemblyResolver());
                }
                else
                {
                    config.Services.Replace(typeof(IAssembliesResolver), new DefaultAssemblyResolver());
                }
            }

            config.EnsureInitialized();
        }
    }
}