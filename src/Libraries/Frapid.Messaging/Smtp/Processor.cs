using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using Frapid.Messaging.DTO;
using Serilog;

namespace Frapid.Messaging.Smtp
{
    public sealed class Processor : IEmailProcessor
    {
        public SmtpHost Host { get; private set; }
        public ICredentials Credentials { get; private set; }
        public bool IsEnabled { get; set; }

        public void InitializeConfig(string database)
        {
            var config = new EmailConfig(database, null);
            var host = EmailHelper.GetSmtpHost(config);
            var credentials = EmailHelper.GetCredentials(config);

            this.Config = config;
            this.Host = host;
            this.Credentials = credentials;
            this.IsEnabled = config.Enabled;
        }

        public IEmailConfig Config { get; set; }

        public Task<bool> SendAsync(EmailMessage email)
        {
            return this.SendAsync(email, false, null);
        }

        public async Task<bool> SendAsync(EmailMessage email, bool deleteAttachmentes, params string[] attachments)
        {
            if (string.IsNullOrWhiteSpace(email.SendTo))
            {
                throw new ArgumentNullException(email.SendTo);
            }

            if (string.IsNullOrWhiteSpace(email.Message))
            {
                throw new ArgumentNullException(email.Message);
            }

            var addresses = email.SendTo.Split(',');

            foreach (var validator in addresses.Select(address => new Validator(address)))
            {
                validator.Validate();

                if (!validator.IsValid)
                {
                    return false;
                }
            }

            addresses = addresses.Distinct().ToArray();
            email.SendTo = string.Join(",", addresses);
            email.Status = Status.Executing;


            using (var mail = new MailMessage(email.FromIdentifier, email.SendTo))
            {
                if (attachments != null)
                {
                    foreach (string file in attachments)
                    {
                        if (!string.IsNullOrWhiteSpace(file))
                        {
                            if (File.Exists(file))
                            {
                                var attachment = new Attachment(file, MediaTypeNames.Application.Octet);

                                var disposition = attachment.ContentDisposition;
                                disposition.CreationDate = File.GetCreationTime(file);
                                disposition.ModificationDate = File.GetLastWriteTime(file);
                                disposition.ReadDate = File.GetLastAccessTime(file);

                                disposition.FileName = Path.GetFileName(file);
                                disposition.Size = new FileInfo(file).Length;
                                disposition.DispositionType = DispositionTypeNames.Attachment;

                                mail.Attachments.Add(attachment);
                            }
                        }
                    }
                }

                using (var smtp = new SmtpClient(this.Host.Address, this.Host.Port))
                {
                    smtp.DeliveryMethod = this.Host.DeliveryMethod;
                    smtp.PickupDirectoryLocation = this.Host.PickupDirectory;

                    smtp.EnableSsl = this.Host.EnableSsl;
                    smtp.UseDefaultCredentials = false;
                    smtp.Credentials = new NetworkCredential(this.Credentials.Username, this.Credentials.Password);
                    try
                    {
                        mail.Subject = email.Subject;
                        mail.Body = email.Message;
                        mail.IsBodyHtml = email.IsBodyHtml;

                        mail.SubjectEncoding = Encoding.UTF8;

                        mail.ReplyToList.Add(new MailAddress(email.FromIdentifier, email.FromName));

                        await smtp.SendMailAsync(mail).ConfigureAwait(false);

                        email.Status = Status.Completed;
                        return true;
                    }
                    catch (SmtpException ex)
                    {
                        email.Status = Status.Failed;
                        Log.Warning(@"Could not send email to {To}. {Ex}. ", email.SendTo, ex);
                    }
                    finally
                    {
                        foreach (IDisposable item in mail.Attachments)
                        {
                            item?.Dispose();
                        }

                        if (deleteAttachmentes && email.Status == Status.Completed)
                        {
                            this.DeleteFiles(attachments);
                        }
                    }
                }
            }

            return false;
        }

        private void DeleteFiles(params string[] files)
        {
            foreach (string file in files.Where(file => !string.IsNullOrWhiteSpace(file)).Where(File.Exists))
            {
                File.Delete(file);
            }
        }
    }
}