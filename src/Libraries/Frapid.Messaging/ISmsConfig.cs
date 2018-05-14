namespace Frapid.Messaging
{
    public interface ISmsConfig
    {
        string FromName { get; set; }
        string FromNumber { get; set; }
        bool Enabled { get; set; }
    }
}