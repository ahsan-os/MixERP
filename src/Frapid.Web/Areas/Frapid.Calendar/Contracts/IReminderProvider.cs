using System.Threading.Tasks;

namespace Frapid.Calendar.Contracts
{
    public interface IReminderProvider
    {
        string ProviderId { get; set; }
        string LocalizedName { get; set; }
        bool Enabled { get; set; }

        Task<bool> RemindAsync(string tenant, ReminderMessage message);
    }
}