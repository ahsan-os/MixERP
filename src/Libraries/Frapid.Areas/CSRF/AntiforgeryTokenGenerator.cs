using System.Web.Mvc;
using Frapid.Configuration.TenantServices.Contracts;

namespace Frapid.Areas.CSRF
{
    public sealed class AntiforgeryTokenGenerator : IAntiforgeryTokenGenerator
    {
        public AntiforgeryTokenGenerator(IStaticDomainCheck staticDomainCheck, IAntiforgeryTokenizer tokenizer, string currentDomain)
        {
            this.StaticDomainCheck = staticDomainCheck;
            this.Tokenizer = tokenizer;
            this.CurrentDomain = currentDomain;
        }

        public IStaticDomainCheck StaticDomainCheck { get; }
        public IAntiforgeryTokenizer Tokenizer { get; }
        public string CurrentDomain { get; }

        public MvcHtmlString GetAntiForgeryToken()
        {
            bool isStatic = this.StaticDomainCheck.IsStaticDomain(this.CurrentDomain);

            if (isStatic)
            {
                return new MvcHtmlString(string.Empty);
            }

            return this.Tokenizer.Get();
        }
    }
}