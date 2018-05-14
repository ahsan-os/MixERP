using System.Threading.Tasks;
using Frapid.WebsiteBuilder.Contracts;
using Frapid.WebsiteBuilder.Models;
using HtmlAgilityPack;

namespace Frapid.WebsiteBuilder.Plugins
{
    public class ArticleExtension : IContentExtension
    {
        public async Task<string> ParseHtmlAsync(string tenant, string html)
        {
            var doc = new HtmlDocument();
            doc.LoadHtml(html);

            var nodes = doc.DocumentNode.SelectNodes("//include[@article-alias and @category-alias]");
            if (nodes == null)
            {
                return html;
            }

            foreach (var node in nodes)
            {
                string alias = node.Attributes["article-alias"].Value;
                string categoryAlias = node.Attributes["category-alias"].Value;

                var model =  await ContentModel.GetContentAsync(tenant, categoryAlias, alias).ConfigureAwait(false);
                if (model != null)
                {
                    string contents = model.Contents;

                    var newNode = HtmlNode.CreateNode(contents);
                    node.ParentNode.ReplaceChild(newNode, node);
                }
            }

            return doc.DocumentNode.OuterHtml;
        }
    }
}