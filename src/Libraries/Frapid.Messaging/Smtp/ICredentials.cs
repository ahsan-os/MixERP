namespace Frapid.Messaging.Smtp
{
    public interface ICredentials
    {
        string Username { get; set; }
        string Password { get; set; }
    }
}