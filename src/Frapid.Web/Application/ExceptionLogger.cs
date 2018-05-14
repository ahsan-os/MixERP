using Frapid.Framework;
using Serilog;

namespace Frapid.Web.Application
{
    public sealed class ExceptionLogger : IExceptionLogger
    {
        public string Tenant { get; set; }
        public string OfficeName { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Message { get; set; }

        public void LogError()
        {
            string message = this.GetExceptionMessage();

            Log.Error(message);
        }

        private string GetExceptionMessage()
        {
            return $"Exception occurred in {this.Tenant} on {this.OfficeName}. Application user: {this.UserId} ({this.UserName}).\r\n\r\n{this.Message}";
        }
    }
}