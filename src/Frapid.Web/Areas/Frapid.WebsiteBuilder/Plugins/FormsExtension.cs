using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using Frapid.WebsiteBuilder.Contracts;
using HtmlAgilityPack;

namespace Frapid.WebsiteBuilder.Plugins
{
    public class FormsExtension
    {
        public static IFormExtension GetForm(string identifier)
        {
            var iType = typeof (IFormExtension);
            var members = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(x => x.GetTypes())
                .Where(x => iType.IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract)
                .Select(Activator.CreateInstance);
            return members.Cast<IFormExtension>().FirstOrDefault(member => member.Identifier.Equals(identifier));
        }

        public static string GetForm(string tenant, string identifier, string path)
        {
            var form = GetForm(identifier);

            if (form == null)
            {
                return string.Empty;
            }

            form.Path = path;
            return form.GetForm(tenant);
        }

        public static async Task<string> PostFormAsync(string tenant, string identifier, string path, FormCollection model)
        {
            var form = GetForm(identifier);

            if (form == null)
            {
                return string.Empty;
            }

            form.Path = path;
            form.Form = model;

            return await form.PostFormAsync(tenant, model).ConfigureAwait(false);
        }

        public static async Task<string> ParseHtmlAsync(string tenant, string html, bool isPost = false, FormCollection form = null)
        {
            if (string.IsNullOrWhiteSpace(html))
            {
                return string.Empty;
            }

            var doc = new HtmlDocument();
            doc.LoadHtml(html);

            var nodes = doc.DocumentNode.SelectNodes("//include[@form-identifier and @path]");
            if (nodes == null)
            {
                return html;
            }

            foreach (var node in nodes)
            {
                string identifier = node.Attributes["form-identifier"].Value;
                string path = node.Attributes["path"].Value;


                string contents;

                if (isPost && form != null)
                {
                    identifier = form["IFormExtensionIdentifier"];
                    path = form["IFormExtensionPath"];
                    contents = await PostFormAsync(tenant, identifier, path, form).ConfigureAwait(false);
                }
                else
                {
                    contents = GetForm(tenant, identifier, path);
                }

                var newNode = HtmlNode.CreateNode("div");
                newNode.InnerHtml = contents;

                node.ParentNode.ReplaceChild(newNode, node);
            }

            return doc.DocumentNode.OuterHtml;
        }
    }
}