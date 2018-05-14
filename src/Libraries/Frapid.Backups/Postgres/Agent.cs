using System;
using System.IO;
using System.Threading.Tasks;

namespace Frapid.Backups.Postgres
{
    public sealed class Agent : IDbAgent
    {
        public string Tenant { get; set; }
        public event Progressing Progress;
        public event Complete Complete;
        public event Fail Fail;
        public string FileName { get; set; }
        public DbServer Server { get; set; }
        public string BackupFileLocation { get; set; }

        public async Task<bool> BackupAsync(Action<string> successCallback, Action<string> failCallback)
        {
            await Task.Delay(1).ConfigureAwait(false);

            if (string.IsNullOrWhiteSpace(this.BackupFileLocation))
            {
                this.OnOnBackupFail(new ProgressInfo(i18n.Resources.CannotFindPostgreSQLBackupDirectory));
                failCallback(i18n.Resources.CannotFindPostgreSQLBackupDirectory);
                return false;
            }

            string backupDirectory = Path.Combine(this.BackupFileLocation, this.FileName);
            string path = Path.Combine(backupDirectory, "db.backup");

            Directory.CreateDirectory(backupDirectory);


            var process = new Process(this.Server, path, this.Tenant);

            process.Progress += delegate(ProgressInfo info)
            {
                var progress = this.Progress;
                progress?.Invoke(new ProgressInfo(info.Message));
            };

            process.BackupComplete += delegate(object sender, EventArgs args)
            {
                this.OnOnBackupComplete(sender, args);
                successCallback(this.FileName);
            };

            bool result = process.Execute();

            if (!result)
            {
                this.OnOnBackupFail(new ProgressInfo(i18n.Resources.CouldNotCreateBackup));
                failCallback(i18n.Resources.CouldNotCreateBackup);
                return false;
            }

            return true;
        }

        private void OnOnBackupComplete(object sender, EventArgs e)
        {
            var complete = this.Complete;
            complete?.Invoke(sender, e);
        }

        private void OnOnBackupFail(ProgressInfo progressinfo)
        {
            var fail = this.Fail;
            fail?.Invoke(progressinfo);
        }
    }
}