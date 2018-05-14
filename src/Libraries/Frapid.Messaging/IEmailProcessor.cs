using System.Threading.Tasks;
using Frapid.Messaging.DTO;

namespace Frapid.Messaging
{
    public interface IEmailProcessor
    {
        bool IsEnabled { get; set; }

        IEmailConfig Config { get; set; }
        void InitializeConfig(string database);
        Task<bool> SendAsync(EmailMessage email);
        Task<bool> SendAsync(EmailMessage email, bool deleteAttachmentes, params string[] attachments);
    }
}