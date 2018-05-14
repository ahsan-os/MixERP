using System;

namespace Frapid.Backups
{
    public delegate void Complete(object sender, EventArgs args);

    public delegate void Fail(ProgressInfo progressInfo);

    public delegate void Progressing(ProgressInfo progressInfo);

    public sealed class ProgressInfo
    {
        public ProgressInfo(string message)
        {
            this.Timestamp = DateTimeOffset.UtcNow;
            this.Message = message;
        }

        public DateTimeOffset Timestamp { get; private set; }
        public string Message { get; set; }
    }
}