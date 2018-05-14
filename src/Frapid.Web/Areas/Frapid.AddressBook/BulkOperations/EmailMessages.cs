using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Frapid.AddressBook.Helpers;
using Frapid.AddressBook.ViewModels;
using Frapid.Messaging;
using Frapid.Messaging.DTO;

namespace Frapid.AddressBook.BulkOperations
{
    public static class EmailMessages
    {
        public static bool IsValidEmail(string source)
        {
            return new EmailAddressAttribute().IsValid(source);
        }

        public static async Task<bool> SendAsync(string tenant, EmailViewModel model)
        {
            var processor = EmailProcessor.GetDefault(tenant);
            if (processor == null)
            {
                return false;
            }

            foreach (var contactId in model.Contacts)
            {
                var contact = await DAL.Contacts.GetContactAsync(tenant, model.UserId, contactId).ConfigureAwait(false);
                if (string.IsNullOrWhiteSpace(contact?.EmailAddresses) || !contact.EmailAddresses.Split(',').Any())
                {
                    continue;
                }

                //Only select the first email address
                string emailAddress = contact.EmailAddresses.Split(',').Select(x => x.Trim()).FirstOrDefault(IsValidEmail);

                if (string.IsNullOrWhiteSpace(emailAddress))
                {
                    continue;
                }

                string message = model.Message;
                message = MessageParser.ParseMessage(message, contact);

                var email = new EmailQueue
                {
                    AddedOn = DateTimeOffset.UtcNow,
                    SendOn = DateTimeOffset.UtcNow,
                    SendTo = emailAddress,
                    FromName = processor.Config.FromName,
                    ReplyTo = processor.Config.FromEmail,
                    ReplyToName = processor.Config.FromName,
                    Subject = model.Subject,
                    Message = message
                };

                var manager = new MailQueueManager(tenant, email);
                await manager.AddAsync().ConfigureAwait(false);

                await manager.ProcessQueueAsync(processor).ConfigureAwait(false);
            }

            return true;
        }
    }
}