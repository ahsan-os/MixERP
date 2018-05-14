using WebMarkupMin.Core;

namespace Frapid.Areas
{
    internal static class MinificationHelper
    {
        internal static string Minify(string html)
        {
            var htmlMinifier = new HtmlMinifier();
            var result = htmlMinifier.Minify(html, false);

            if (result.Errors.Count == 0)
            {
                return result.MinifiedContent;
            }

            return html;
        }
    }
}