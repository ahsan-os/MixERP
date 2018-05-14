using System.Web.Mvc;

namespace Frapid.Areas.CSRF
{
    public sealed class AntiforgeryTokenizer : IAntiforgeryTokenizer
    {
        public AntiforgeryTokenizer(HtmlHelper helper)
        {
            this.Helper = helper;
        }

        public HtmlHelper Helper { get; set; }

        public MvcHtmlString Get()
        {
            return this.Helper.AntiForgeryToken();
        }
    }
}