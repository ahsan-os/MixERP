using System.Web.Mvc;

namespace Frapid.Areas.CSRF
{
    public interface IAntiforgeryTokenizer
    {
        MvcHtmlString Get();
    }
}