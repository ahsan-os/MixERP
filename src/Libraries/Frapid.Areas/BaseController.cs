using System;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mime;
using System.Text;
using System.Web.Mvc;
using Frapid.Configuration;
using Frapid.Framework;
using Frapid.i18n;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using Newtonsoft.Json;

namespace Frapid.Areas
{
    public abstract class BaseController : Controller
    {
        public RemoteUser RemoteUser { get; private set; }
        public string CurrentDomain { get; set; }
        public string Tenant { get; set; }
        public string CurrentPageUrl { get; set; }

        protected Uri GetBaseUri()
        {
            string baseUrl = this.Request?.Url?.Scheme + "://" + this.Request?.Url?.Authority + this.Request?.ApplicationPath?.TrimEnd('/') + "/";
            return new Uri(baseUrl);
        }

        protected override void OnActionExecuting(ActionExecutingContext context)
        {
            if (this.Request?.Url != null)
            {
                this.CurrentDomain = this.Request.Url.DnsSafeHost;
                this.CurrentPageUrl = this.Request.Url.AbsoluteUri;
                this.Tenant = TenantConvention.GetTenant(this.CurrentDomain);
            }

            this.RemoteUser = RemoteUser.Get(this.HttpContext);
            this.Initialize(context.RequestContext);
        }

        protected new virtual ContentResult View(string viewName, object model = null)
        {
            var controllerContext = this.ControllerContext;
            var result = ViewEngines.Engines.FindView(controllerContext, viewName, null);

            StringWriter output;
            using (output = new StringWriter())
            {
                var dictionary = new ViewDataDictionary(model);

                var dynamic = this.ViewBag as DynamicObject;

                if (dynamic != null)
                {
                    var members = dynamic.GetDynamicMemberNames().ToList();

                    foreach (string member in members)
                    {
                        var value = Versioned.CallByName(dynamic, member, CallType.Get);
                        dictionary.Add(member, value);
                    }
                }


                var viewContext = new ViewContext(controllerContext, result.View, dictionary, controllerContext.Controller.TempData, output);

                result.View.Render(viewContext, output);
                result.ViewEngine.ReleaseView(controllerContext, result.View);
            }

            string html = CdnHelper.UseCdn(output.ToString());
            html = CustomJavascriptInjector.Inject(viewName, html);
            html = MinificationHelper.Minify(html);
            return this.Content(html, "text/html");
        }

        protected ActionResult Ok(object model = null)
        {
            if (model == null)
            {
                model = "OK";
            }

            this.Response.StatusCode = 200;

            string json = JsonConvert.SerializeObject(model, JsonHelper.GetJsonSerializerSettings());
            return this.Content(json, "application/json");
        }

        protected ActionResult Failed(string message, HttpStatusCode statusCode)
        {
            this.Response.StatusCode = (int) statusCode;
            return this.Content(message, MediaTypeNames.Text.Plain, Encoding.UTF8);
        }

        protected ActionResult InvalidModelState(ModelStateDictionary modelState)
        {
            string errors = string.Join("\n", this.ModelState.SelectMany(x => x.Value.Errors.Select(z => z.ErrorMessage)));
            return this.Failed(errors, HttpStatusCode.BadRequest);
        }

        protected ActionResult AccessDenied()
        {
            return this.Failed(Resources.AccessIsDenied, HttpStatusCode.Forbidden);
        }
    }
}