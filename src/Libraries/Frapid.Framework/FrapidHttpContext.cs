using System;
using System.Web;

namespace Frapid.Framework
{
    public class FrapidHttpContext
    {
        public static IServiceProvider ServiceProvider { get; set; }

        public static HttpContext GetCurrent()
        {
            return HttpContext.Current;
        }
    }
}