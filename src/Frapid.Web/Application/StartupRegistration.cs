using System;
using System.Linq;
using System.Threading.Tasks;
using Frapid.Framework;
using Frapid.Framework.Extensions;
using Serilog;

namespace Frapid.Web.Application
{
    public class StartupRegistration
    {
        public static async Task RegisterAsync()
        {
            var iType = typeof(IStartupRegistration);
            var members = iType.GetTypeMembersNotAbstract<IStartupRegistration>().ToList();

            foreach (var member in members)
            {
                try
                {
                    await member.RegisterAsync().ConfigureAwait(false);
                }
                catch (Exception ex)
                {
                    Log.Error("Could not register the startup job \"{Description}\" due to errors. Exception: {Exception}", member.Description, ex);
                    throw;
                }
            }
        }
    }
}