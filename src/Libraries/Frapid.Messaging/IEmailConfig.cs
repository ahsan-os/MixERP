namespace Frapid.Messaging
{
    public interface IEmailConfig
    {
        string FromName { get; set; }
        string FromEmail { get; set; }
        bool Enabled { get; set; }
    }
}