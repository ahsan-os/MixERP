using System;
using System.Web;

namespace Frapid.Framework
{
    public static class UrlHelper
    {
        private static readonly UrlCombiner Combiner;

        static UrlHelper()
        {
            Combiner = new UrlCombiner();
        }

        public static string CombineUrl(string domain, string path)
        {
            return Combiner.Combine(domain, path);
        }

        public static string ResolveAbsoluteUrl(string relativeUrl)
        {
            if (HttpContext.Current != null)
            {
                return HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority) + VirtualPathUtility.ToAbsolute(relativeUrl);
            }
            return relativeUrl;
        }
    }
}