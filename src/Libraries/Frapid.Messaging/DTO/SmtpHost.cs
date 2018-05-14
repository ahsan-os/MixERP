using System.Net.Mail;

namespace Frapid.Messaging.DTO
{
    public sealed class SmtpHost
    {
        public string Address { get; set; }
        public int Port { get; set; }
        public bool EnableSsl { get; set; }
        public SmtpDeliveryMethod DeliveryMethod { get; set; }
        public string PickupDirectory { get; set; }
    }
}