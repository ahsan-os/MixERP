using System;
using System.IO;
using System.Web;
using SendGrid.Helpers.Mail;

namespace SendGridMail.Helpers
{
    internal static class AttachmentHelper
    {
        internal static Mail AddAttachments(Mail message, string[] attachments)
        {
            if (attachments == null)
            {
                return message;
            }

            foreach (string file in attachments)
            {
                if (string.IsNullOrWhiteSpace(file) || !File.Exists(file))
                {
                    continue;
                }

                var bytes = File.ReadAllBytes(file);
                string content = Convert.ToBase64String(bytes);

                string fileName = new FileInfo(file).Name;

                var attachment = new Attachment
                {
                    Filename = fileName,
                    Content = content,
                    Type = MimeMapping.GetMimeMapping(file),
                    Disposition = "attachment",
                    ContentId = fileName
                };

                message.AddAttachment(attachment);
            }

            return message;
        }
    }
}