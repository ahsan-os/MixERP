using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Frapid.i18n.Command
{
    public static class ResourceSerializer
    {
        public static string GetSerializedResources(Dictionary<string, string> resources)
        {
            var builer = new StringBuilder();

            foreach (var resource in resources.OrderBy(x => x.Key))
            {
                builer.Append(resource.Key);
                builer.Append(": ");
                builer.Append("\"");
                builer.Append(resource.Value.Replace(@"""", @"\"""));
                builer.Append("\"");
                builer.Append(Environment.NewLine);
            }

            return builer.ToString();
        }
    }
}