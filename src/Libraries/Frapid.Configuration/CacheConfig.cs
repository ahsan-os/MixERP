namespace Frapid.Configuration
{
    public static class CacheConfig
    {
        public static string GetDefaultCacheType()
        {
            return ConfigurationManager.GetConfigurationValue("ParameterConfigFileLocation", "DefaultCacheType");
        }
    }
}