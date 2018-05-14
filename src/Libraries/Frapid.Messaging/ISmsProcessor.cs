using System.Threading.Tasks;
using Frapid.Messaging.DTO;

namespace Frapid.Messaging
{
    public interface ISmsProcessor
    {
        bool IsEnabled { get; set; }

        ISmsConfig Config { get; set; }
        void InitializeConfig(string database);
        Task<bool> SendAsync(SmsMessage sms);
    }
}