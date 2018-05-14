namespace Frapid.Messaging.Smtp
{
    public class SmsConfig : ISmsConfig
    {
        public SmsConfig(string tenant, ISmsProcessor processor)
        {
            if (processor != null)
            {
                this.Tenant = tenant;
                this.Enabled = processor.IsEnabled;
                this.FromName = processor.Config.FromName;
                this.FromNumber = processor.Config.FromNumber;
            }
        }

        public string Tenant { get; set; }
        public bool Enabled { get; set; }
        public string FromName { get; set; }
        public string FromNumber { get; set; }
    }
}