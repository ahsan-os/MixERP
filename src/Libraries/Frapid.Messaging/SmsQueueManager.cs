using System;
using System.Threading.Tasks;
using Frapid.Messaging.DAL;
using Frapid.Messaging.DTO;
using Frapid.Messaging.Smtp;

namespace Frapid.Messaging
{
    public sealed class SmsQueueManager
    {
        public SmsQueueManager()
        {
        }

        public SmsQueueManager(string database, SmsQueue sms)
        {
            this.Database = database;
            this.Sms = sms;
        }

        public SmsQueue Sms { get; set; }
        public string Database { get; set; }
        public ISmsProcessor Processor { get; set; }

        public async Task AddAsync()
        {
            this.Processor = SmsProcessor.GetDefault(this.Database);

            if (!this.IsEnabled())
            {
                return;
            }

            var config = new SmsConfig(this.Database, this.Processor);


            if (string.IsNullOrWhiteSpace(this.Sms.FromName))
            {
                this.Sms.FromName = config.FromName;
            }

            if (string.IsNullOrWhiteSpace(this.Sms.FromNumber))
            {
                this.Sms.FromNumber = config.FromNumber;
            }

            var sysConfig = MessagingConfig.Get(this.Database);

            if (sysConfig.TestMode)
            {
                this.Sms.IsTest = true;
            }

            await TextMessageQueue.AddToQueueAsync(this.Database, this.Sms).ConfigureAwait(false);
        }

        private bool IsEnabled()
        {
            return this.Processor != null && this.Processor.IsEnabled;
        }

        public async Task ProcessQueueAsync(ISmsProcessor processor)
        {
            var queue = await TextMessageQueue.GetMailInQueueAsync(this.Database).ConfigureAwait(false);
            var config = new SmsConfig(this.Database, this.Processor);
            this.Processor = processor;

            if (this.IsEnabled())
            {
                foreach (var mail in queue)
                {
                    var message = SmsHelper.GetMessage(config, mail);

                    await processor.SendAsync(message).ConfigureAwait(false);

                    if (message.Status == Status.Completed)
                    {
                        mail.Delivered = true;
                        mail.DeliveredOn = DateTimeOffset.UtcNow;

                        await TextMessageQueue.SetSuccessAsync(this.Database, mail.QueueId).ConfigureAwait(false);
                    }
                }
            }
        }
    }
}