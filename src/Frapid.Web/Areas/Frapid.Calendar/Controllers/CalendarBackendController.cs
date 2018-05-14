using Frapid.Dashboard.Controllers;

namespace Frapid.Calendar.Controllers
{
    public class CalendarBackendController : DashboardController
    {
        public CalendarBackendController()
        {
            this.ViewBag.LayoutPath = this.GetLayoutPath();
        }


        private string GetLayoutPath()
        {
            return this.GetRazorView<AreaRegistration>("Layout.cshtml", this.Tenant);
        }
    }
}