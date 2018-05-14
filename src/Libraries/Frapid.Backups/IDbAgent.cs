using System;
using System.Threading.Tasks;

namespace Frapid.Backups
{
    public interface IDbAgent
    {
        string FileName { get; set; }
        DbServer Server { get; set; }
        string BackupFileLocation { get; set; }
        event Progressing Progress;
        event Complete Complete;
        event Fail Fail;
        Task<bool> BackupAsync(Action<string> success, Action<string> fail);
    }
}