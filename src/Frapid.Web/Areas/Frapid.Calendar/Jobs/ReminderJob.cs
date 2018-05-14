using System.Linq;
using System.Threading.Tasks;
using Frapid.AddressBook.Helpers;
using Frapid.Calendar.Contracts;
using Frapid.Calendar.DTO;
using Quartz;

namespace Frapid.Calendar.Jobs
{
    public sealed class ReminderJob : IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            var data = context.JobDetail.JobDataMap;

            string tenant = (string) data["Tenant"];
            var @event = (Event) data["Event"];


            if (string.IsNullOrWhiteSpace(tenant))
            {
                return;
            }

            var message = new ReminderMessage
            {
                Event = @event,
                Contact = Contacts.GetContactByUserIdAsync(tenant, @event.UserId).GetAwaiter().GetResult()
            };

            var providers = @event.ReminderTypes.Split(',');

            foreach (string providerId in providers)
            {
                var provider = this.GetProvider(providerId.Trim());
                if (provider == null)
                {
                    continue;
                }

                Task.Run(async () => { await provider.RemindAsync(tenant, message).ConfigureAwait(true); }).GetAwaiter().GetResult();
            }
        }

        private IReminderProvider GetProvider(string providerId)
        {
            var provider = ReminderProviders.GetEnabled().FirstOrDefault(x => x.ProviderId == providerId);
            return provider;
        }
    }
}