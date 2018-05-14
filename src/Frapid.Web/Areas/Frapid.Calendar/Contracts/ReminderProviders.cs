using System.Collections.Generic;
using System.Linq;
using Frapid.Framework.Extensions;

namespace Frapid.Calendar.Contracts
{
    public static class ReminderProviders
    {
        public static IEnumerable<IReminderProvider> GetEnabled()
        {
            var type = typeof(IReminderProvider);
            return type.GetTypeMembersNotAbstract<IReminderProvider>().Where(x => x.Enabled);
        }
    }
}