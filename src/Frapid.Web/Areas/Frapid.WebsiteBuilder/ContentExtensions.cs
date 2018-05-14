using System;
using System.Linq;
using System.Threading.Tasks;
using Frapid.WebsiteBuilder.Contracts;

namespace Frapid.WebsiteBuilder
{
    public static class ContentExtensions
    {
        public static async Task<string> ParseHtmlAsync(string tenant, string html)
        {
            var iType = typeof(IContentExtension);
            var members = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(x => x.GetTypes())
                .Where(x => iType.IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract)
                .Select(Activator.CreateInstance);

            foreach(IContentExtension member in members)
            {
                html = await member.ParseHtmlAsync(tenant, html).ConfigureAwait(false);
            }

            return html;
        }
    }
}