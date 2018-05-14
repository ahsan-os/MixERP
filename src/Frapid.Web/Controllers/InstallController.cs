using System.Linq;
using System.Web.Mvc;
using Frapid.Areas;
using Frapid.Configuration;
using Frapid.i18n;
using Frapid.Installer;

namespace Frapid.Web.Controllers
{
    public class InstallController : FrapidController
    {
        [Route("install")]
        public ActionResult Index()
        {
            string domain = TenantConvention.GetDomain();

            var approved = new ApprovedDomainSerializer();
            var installed = new InstalledDomainSerializer();

            if (!approved.GetMemberSites().Any(x => x.Equals(domain)))
            {
                return this.HttpNotFound();
            }

            if (installed.GetMemberSites().Any(x => x.Equals(domain)))
            {
                return this.Redirect("/");
            }

            var setup = approved.Get().FirstOrDefault(x => x.GetSubtenants().Contains(domain.ToLowerInvariant()));
            InstallationFactory.Setup(setup); //Background job
            return this.Content(Resources.FrapidInstallationMessage);
        }
    }
}