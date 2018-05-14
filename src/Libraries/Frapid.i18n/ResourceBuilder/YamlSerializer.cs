using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Frapid.i18n.ResourceBuilder
{
    internal static class YamlSerializer
    {
        private static string Escape(string token)
        {
            string escaped = token.Replace(@"""", @"\""").Replace("\r\n", "\\r\\n").Replace("\n", "\\n");
            return escaped;
        }

        public static void SaveToDisk(string path, Dictionary<string, string> resources)
        {
            var builer = new StringBuilder();

            foreach (var resource in resources.OrderBy(x => x.Key))
            {
                builer.Append(resource.Key);
                builer.Append(": ");
                builer.Append("\"");
                builer.Append(Escape(resource.Value));
                builer.Append("\"");
                builer.Append(Environment.NewLine);
            }

            if (resources.Any())
            {
                File.WriteAllText(path, builer.ToString(), new UTF8Encoding(false));
            }
        }
    }
}