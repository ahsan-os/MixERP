using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using Frapid.ApplicationState.Cache;
using Frapid.Framework.Extensions;
using Frapid.i18n;

namespace Frapid.Dashboard.Controllers
{
    public class DefaultController : DashboardController
    {
        [Route("dashboard")]
        public ActionResult Index()
        {
            return this.View(this.GetRazorView<AreaRegistration>("Default/Index.cshtml", this.Tenant));
        }

        [Route("dashboard/meta")]
        public ActionResult GetMeta()
        {
            return this.Ok
            (
                new ViewModels.Dashboard
                {
                    Culture = CultureManager.GetCurrent().Name,
                    Tenant = this.Tenant,
                    Language = CultureManager.GetCurrent().TwoLetterISOLanguageName,
                    JqueryUIi18NPath = "/scripts/jquery-ui/i18n/",
                    Today = DateTime.Today,
                    Now = DateTimeOffset.UtcNow,
                    UserId = this.AppUser.UserId,
                    User = this.AppUser.Email,
                    Office = this.AppUser.OfficeName,
                    MetaView = AppUsers.GetCurrent(),
                    ShortDateFormat = CultureManager.GetShortDateFormat(),
                    LongDateFormat = CultureManager.GetCurrent().DateTimeFormat.LongDatePattern,
                    ThousandSeparator = CultureManager.GetThousandSeparator(),
                    DecimalSeparator = CultureManager.GetDecimalSeparator(),
                    CurrencyDecimalPlaces = CultureManager.GetCurrencyDecimalPlaces(),
                    CurrencySymbol = CultureManager.GetCurrencySymbol(),
                    DatepickerShowWeekNumber = true,
                    DatepickerWeekStartDay = (int) CultureManager.GetCurrent().DateTimeFormat.FirstDayOfWeek,
                    DatepickerNumberOfMonths = "[1, 3]"
                });
        }


        [Route("dashboard/custom-variables")]
        public async Task<ActionResult> GetCustomVariablesAsync()
        {
            var meta = await AppUsers.GetCurrentAsync().ConfigureAwait(true);

            var model = new Dictionary<string, string>();
            var iType = typeof(ICustomJavascriptVariable);
            var members = iType.GetTypeMembers<ICustomJavascriptVariable>();

            foreach (var member in members)
            {
                var items = await member.GetAsync(this.Tenant, meta.OfficeId).ConfigureAwait(true);
                model = model.Union(items).ToDictionary(k => k.Key, v => v.Value);
            }
        
            return this.Ok(model);
        }
    }
}