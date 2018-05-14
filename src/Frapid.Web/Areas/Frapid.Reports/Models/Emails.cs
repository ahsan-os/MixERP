using System;
using System.Threading.Tasks;
using Frapid.Configuration;
using Frapid.Messaging;
using Frapid.Messaging.DTO;
using Frapid.Reports.ViewModels;

namespace Frapid.Reports.Models
{
    public static class Emails
    {
        public static async Task SendAsync(string tenant, Uri baseUri, EmailViewModel model)
        {
            var processor = EmailProcessor.GetDefault(tenant);

            if (processor == null)
            {
                throw new EmailException(I18N.NoEmailProcessorDefined);
            }

            string attachmentPath = ExportHelper.Export(tenant, baseUri, model.FileName, "pdf", model.Html);

            attachmentPath = PathMapper.MapPath(attachmentPath);

            var email = new EmailQueue
            {
                AddedOn = DateTimeOffset.UtcNow,
                SendOn = DateTimeOffset.UtcNow,
                SendTo = model.SendTo,
                FromName = processor.Config.FromName,
                ReplyTo = processor.Config.FromEmail,
                Subject = model.Subject,
                Message = model.Message,
                Attachments = attachmentPath
            };

            var manager = new MailQueueManager(tenant, email);

            await manager.AddAsync().ConfigureAwait(false);
            await manager.ProcessQueueAsync(processor, true).ConfigureAwait(false);
        }
    }
}