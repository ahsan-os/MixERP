using System.IO;
using System.Text;
using Frapid.ApplicationState.Cache;
using Frapid.Configuration;

namespace Frapid.Calendar
{
    public static class Configs
    {
        public static string GoogleMapsJavascriptApiKey => GetGoogleMapsJavascriptApi();

        public static string GetGoogleMapsJavascriptApi()
        {
            string tenant = AppUsers.GetTenant();
            var google = Google.ConfigurationManager.Get(tenant);
            return google.MapsJavascriptApiKey;
        }


        public static string Get(string key)
        {
            string tenant = AppUsers.GetTenant();
            return Get(tenant, key);
        }

        public static string GetNotificationEmailTemplate(string tenant)
        {
            string file = $"/Tenants/{tenant}/Areas/Frapid.Calendar/Templates/Email.html";
            file = PathMapper.MapPath(file);

            if (!File.Exists(file))
            {
                return string.Empty;
            }

            return File.ReadAllText(file, Encoding.UTF8);
        }

        public static string Get(string tenant, string key)
        {
            string configurationFile = $"/Tenants/{tenant}/Configs/Calendar.config";
            configurationFile = PathMapper.MapPath(configurationFile);

            if (!File.Exists(configurationFile))
            {
                return string.Empty;
            }

            return ConfigurationManager.ReadConfigurationValue(configurationFile, key);
        }
    }
}