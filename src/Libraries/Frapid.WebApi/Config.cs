using System.IO;
using Frapid.Configuration;
using Frapid.Framework.Extensions;

namespace Frapid.WebApi
{
    public static class Config
    {
        public static int GetPageSize(string tenant)
        {
            string configFile = PathMapper.MapPath($"~/Tenants/{tenant}/Configs/Frapid.config");
            int pageSize = !File.Exists(configFile) ? 0 : ConfigurationManager.ReadConfigurationValue(configFile, "WebApiPageSize").To<int>();

            pageSize = pageSize == 0 ? 10 : pageSize;
            pageSize = pageSize > 100 ? 100 : pageSize;

            return pageSize;
        }
    }
}