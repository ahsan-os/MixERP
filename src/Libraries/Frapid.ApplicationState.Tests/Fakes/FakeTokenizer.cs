using System.Web.Mvc;
using Frapid.Areas.CSRF;

namespace Frapid.ApplicationState.Tests.Fakes
{
    public sealed class FakeTokenizer:IAntiforgeryTokenizer
    {
        public MvcHtmlString Get()
        {
            return new MvcHtmlString("Token");
        }
    }
}