using System;
using System.Net;
using System.Web.Helpers;
using System.Web.Mvc;
using Frapid.Framework.Extensions;
using Serilog;

namespace Frapid.Areas.CSRF
{
    [AttributeUsage(AttributeTargets.Class)]
    public class AntiForgeryAttribute : AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            var request = filterContext.HttpContext.Request;

            if (request.HttpMethod != WebRequestMethods.Http.Post)
            {
                return;
            }

            try
            {
                if (request.IsAjaxRequest())
                {
                    var antiForgeryCookie = request.Cookies[AntiForgeryConfig.CookieName];
                    string cookieValue = antiForgeryCookie?.Value;
                    AntiForgery.Validate(cookieValue, request.Headers["RequestVerificationToken"]);
                }
                else
                {
                    new ValidateAntiForgeryTokenAttribute().OnAuthorization(filterContext);
                }
            }
            catch (HttpAntiForgeryException ex)
            {
                //Log and swallow
                Log.Error
                    (
                        "Invalid antiforgery cookie data.\nIP: {IP}\nBrowser: {Browser}\nUser Agener: {UserAgent}. Stack: {Exception}",
                        filterContext.HttpContext.GetClientIpAddress(),
                        filterContext.HttpContext.Request.Browser.Browser,
                        filterContext.HttpContext.Request.UserAgent,
                        ex);
            }
        }
    }
}