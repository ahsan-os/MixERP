using System;
using System.Web.Mvc;
using Frapid.Areas.CSRF;
using Frapid.Configuration;
using Frapid.Configuration.TenantServices;
using Serilog;

namespace Frapid.Areas
{
    public static class AntiforgeryHelper
    {
        private static string GetDomainName(Uri url)
        {
            var extractor = new DomainNameExtractor(Log.Logger);
            return extractor.GetDomain(url.Authority);
        }

        public static MvcHtmlString GetAntiForgeryToken(this HtmlHelper helper, Uri url)
        {
            var logger = Log.Logger;
            var serializer = new ApprovedDomainSerializer();
            var check = new StaticDomainCheck(logger, serializer);
            var tokenizer = new AntiforgeryTokenizer(helper);
            string currentDomain = GetDomainName(url);

            var generator = new AntiforgeryTokenGenerator(check, tokenizer, currentDomain);
            return generator.GetAntiForgeryToken();
        }
    }
}