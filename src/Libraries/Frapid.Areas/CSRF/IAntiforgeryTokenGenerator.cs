using System.Web.Mvc;
using Frapid.Configuration.TenantServices.Contracts;

namespace Frapid.Areas.CSRF
{
    public interface IAntiforgeryTokenGenerator
    {
        string CurrentDomain { get; }
        IStaticDomainCheck StaticDomainCheck { get; }
        IAntiforgeryTokenizer Tokenizer { get; }

        MvcHtmlString GetAntiForgeryToken();
    }
}