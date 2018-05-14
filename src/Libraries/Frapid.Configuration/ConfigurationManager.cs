using System.Configuration;
using Serilog;

namespace Frapid.Configuration
{
    public static class ConfigurationManager
    {
        /// <summary>
        ///     Gets the configuration value of the requested key.
        /// </summary>
        /// <param name="configFileName">The name of the configuration file.</param>
        /// <param name="key">The configuration key to find.</param>
        /// <returns>Returns the configuration value of the requested key.</returns>
        public static string GetConfigurationValue(string configFileName, string key)
        {
            Log.Verbose($"Getting configuration key \"{key}\" on config file \"{configFileName}\".");

            if (configFileName == null)
            {
                Log.Verbose($"Empty string returned for the key \"{key}\" because no config file name was provided.");
                return string.Empty;
            }

            string fileName = System.Configuration.ConfigurationManager.AppSettings[configFileName];
            Log.Verbose($"Configuration file for \"{configFileName}\" is {fileName}.");

            string path = PathMapper.MapPath(fileName);

            return ReadConfigurationValue(path, key);
        }

        /// <summary>
        ///     Opens a configuration files and gets the configuration value of the requested key.
        /// </summary>
        /// <param name="path">The physical path to the configuration file.</param>
        /// <param name="key">The configuration key to find.</param>
        /// <returns>Returns the configuration value of the requested key.</returns>
        public static string ReadConfigurationValue(string path, string key)
        {
            var configFileMap = new ExeConfigurationFileMap
            {
                ExeConfigFilename = path
            };

            var config = System.Configuration.ConfigurationManager.OpenMappedExeConfiguration(configFileMap, ConfigurationUserLevel.None);

            var section = config.GetSection("appSettings") as AppSettingsSection;

            return section?.Settings[key] != null ? section.Settings[key].Value : string.Empty;
        }

        /// <summary>
        ///     Saves appSettings key configuration on the requested file.
        /// </summary>
        /// <param name="path">Path to configuration file.</param>
        /// <param name="key">The key to edit.</param>
        /// <param name="value">The value to edit the key with.</param>
        public static void SetConfigurationValue(string path, string key, string value)
        {
            var configFileMap = new ExeConfigurationFileMap
            {
                ExeConfigFilename = path
            };
            var config = System.Configuration.ConfigurationManager.OpenMappedExeConfiguration(configFileMap, ConfigurationUserLevel.None);
            var section = config.GetSection("appSettings") as AppSettingsSection;

            if (section?.Settings[key] != null)
            {
                section.Settings[key].Value = value;
            }

            config.Save(ConfigurationSaveMode.Modified);
        }
    }
}