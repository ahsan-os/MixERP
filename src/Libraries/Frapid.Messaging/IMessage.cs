namespace Frapid.Messaging
{
    internal interface IMessage
    {
        string FromName { get; set; }
        string FromIdentifier { get; set; }
        string SendTo { get; set; }
        string Message { get; set; }
        //bool IsBodyHtml { get; set; }
    }
}