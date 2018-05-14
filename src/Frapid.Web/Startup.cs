using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using Frapid.Framework;
using Frapid.Web;
using Frapid.Web.Application;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Owin;

[assembly: OwinStartup(typeof(Startup))]
namespace Frapid.Web
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            this.ConfigureAsync(app).GetAwaiter().GetResult();
        }

        public async Task ConfigureAsync(IAppBuilder app)
        {
            GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings = JsonHelper.GetJsonSerializerSettings();

            app.Map
                (
                    "/signalr",
                    map =>
                    {
                        map.UseCors(CorsOptions.AllowAll);

                        var configuration = new HubConfiguration
                        {
                            EnableJavaScriptProxies = true
                        };

                        map.RunSignalR(configuration);
                        var module = new AuthorizeModule(null, null);
                        GlobalHost.HubPipeline.AddModule(module);
                    });


            ViewEngines.Engines.Clear();
            ViewEngines.Engines.Add(new FrapidRazorViewEngine());
            LogManager.InternalizeLogger();
            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            AssetConfig.Register();
            await StartupRegistration.RegisterAsync().ConfigureAwait(false);
            BackupRegistration.Register();
            EodTaskRegistration.Register();
            AccountConfig.Register(app);
        }
    }
}