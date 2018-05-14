using System;
using System.IO;
using System.Linq;
using System.Text;
using Frapid.Configuration;

namespace Frapid.Messaging
{
    public static class DisposableEmailValidator
    {
        /// <summary>
        ///     Checks if an email address belongs to a disposable email domain
        /// </summary>
        public static bool IsDisposableEmail(string tenant, string email)
        {
            string configPath;

            //The file should be a plain text file which contains the list of dispoable email domains separated by new line.
            //The file is expected to be present on either of these two paths:
            string tenantConfigFile = PathMapper.MapPath($"/Tenants/{tenant}/Configs/DisposableEmailDomains.config");
            string rootConfigFile = PathMapper.MapPath("/Resources/Configs/DisposableEmailDomains.config");


            if (File.Exists(tenantConfigFile))
            {
                configPath = tenantConfigFile;
            }
            else
            {
                configPath = File.Exists(rootConfigFile) ? rootConfigFile : string.Empty;
            }

            if (string.IsNullOrWhiteSpace(configPath))
            {
                //Cannot determine if the email address is disposable because no configuration file was found.
                return false;
            }

            string contents = File.ReadAllText(configPath, Encoding.UTF8);
            var domains = contents.Split
                (
                    new[]
                    {
                        "\r\n",
                        "\n"
                    },
                    StringSplitOptions.None).Select(x => x.ToUpperInvariant().Trim());
            return domains.Any(domain => email.Trim().ToUpperInvariant().EndsWith(domain.ToUpperInvariant()));
        }
    }
}