using System.IO;
using System.Text;
using System.Web.Mvc;
using Frapid.Account.ViewModels;
using Frapid.Areas.CSRF;
using Frapid.Dashboard;
using Frapid.Dashboard.Controllers;

namespace Frapid.Account.Controllers.Backend
{
    [AntiForgery]
    public class EmailTemplateController : DashboardController
    {
        [Route("dashboard/account/email-templates/{file}")]
        [MenuPolicy]
        public ActionResult Index(string file)
        {
            string contents = this.GetContents(file);

            if (string.IsNullOrWhiteSpace(contents))
            {
                throw new FileNotFoundException();
            }

            var model = new Template
            {
                Contents = contents,
                Title = file + ".html"
            };
            return this.FrapidView(this.GetRazorView<AreaRegistration>("EmailTemplate/Index.cshtml", this.Tenant), model);
        }

        [Route("dashboard/account/email-templates")]
        [HttpPost]
        public ActionResult Save(Template model)
        {
            this.SetContents(model.Title, model.Contents);
            return this.Ok();
        }

        private string GetContents(string file)
        {
            string path = Configuration.GetOverridePath(this.Tenant) + "/EmailTemplates/" + file + ".html";
            return System.IO.File.Exists(path) ? System.IO.File.ReadAllText(path, Encoding.UTF8) : string.Empty;
        }

        private void SetContents(string file, string contents)
        {
            string path = Configuration.GetOverridePath(this.Tenant) + "/EmailTemplates/" + file;
            if (System.IO.File.Exists(path))
            {
                System.IO.File.WriteAllText(path, contents, new UTF8Encoding(false));
            }
        }
    }
}