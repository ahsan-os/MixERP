namespace Frapid.Messaging.Smtp
{
    public sealed class SmtpCredentials : ICredentials
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}