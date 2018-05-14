using System;
using System.Linq;
using System.Threading.Tasks;
using Frapid.AddressBook.Helpers;
using Frapid.AddressBook.ViewModels;
using Frapid.ApplicationState.Models;
using Frapid.Framework.Extensions;
using Frapid.Messaging;
using Frapid.Messaging.DTO;

namespace Frapid.AddressBook.BulkOperations
{
    public static class TextMessages
    {
        public static async Task<bool> SendAsync(string tenant, SmsViewModel model, LoginView meta)
        {
            var processor = SmsProcessor.GetDefault(tenant);
            if (processor == null)
            {
                return false;
            }

            foreach (var contactId in model.Contacts)
            {
                var contact = await DAL.Contacts.GetContactAsync(tenant, model.UserId, contactId).ConfigureAwait(false);
                if (string.IsNullOrWhiteSpace(contact?.MobileNumbers) || !contact.MobileNumbers.Split(',').Any())
                {
                    continue;
                }

                //Only select the first cell number
                string cellNumber = contact.MobileNumbers.Split(',').Select(x => x.Trim()).FirstOrDefault();

                if (string.IsNullOrWhiteSpace(cellNumber))
                {
                    continue;
                }

                string message = model.Message;
                message = MessageParser.ParseMessage(message, contact);

                var sms = new SmsQueue
                {
                    AddedOn = DateTimeOffset.UtcNow,
                    SendOn = DateTimeOffset.UtcNow,
                    SendTo = cellNumber,
                    FromName = processor.Config.FromName.Or(meta.OfficeName),
                    FromNumber = processor.Config.FromNumber.Or(meta.Phone),
                    Subject = model.Subject,
                    Message = message
                };

                await new SmsQueueManager(tenant, sms).AddAsync().ConfigureAwait(false);
            }

            var queue = new SmsQueueManager
            {
                Database = tenant
            };
            
            await queue.ProcessQueueAsync(processor).ConfigureAwait(false);

            return true;
        }
    }
}