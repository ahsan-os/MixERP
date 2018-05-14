namespace Frapid.Framework
{
    public interface IExceptionLogger
    {
        string Tenant { get; set; }
        string OfficeName { get; set; }
        int UserId { get; set; }
        string UserName { get; set; }
        string Message { get; set; }

        void LogError();
    }
}