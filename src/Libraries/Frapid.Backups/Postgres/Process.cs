using System;
using System.Diagnostics;
using System.IO;

namespace Frapid.Backups.Postgres
{
    public sealed class Process
    {
        public Process(DbServer server, string fileName, string tenant)
        {
            this.Server = server;
            this.PgDumpPath = Path.Combine(this.Server.BinDirectory, "pg_dump.exe");
            this.FileName = fileName;
            this.Tenant = tenant;
        }

        public string Tenant { get; set; }
        public string BatchFileName { get; set; }
        public string PgDumpPath { get; set; }
        public DbServer Server { get; set; }
        public string FileName { get; set; }
        public event Progressing Progress;
        public event Complete BackupComplete;

        public bool Execute()
        {
            var batchFile = new BatchFile(this.FileName, this.Server, this.PgDumpPath, this.Tenant);
            this.BatchFileName = batchFile.Create();

            using (var process = new System.Diagnostics.Process())
            {
                process.StartInfo.FileName = this.BatchFileName;

                process.StartInfo.CreateNoWindow = true;
                process.StartInfo.ErrorDialog = false;
                process.StartInfo.RedirectStandardInput = true;
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.RedirectStandardError = true;

                process.ErrorDataReceived += this.Data_Received;
                process.OutputDataReceived += this.Data_Received;
                process.Disposed += this.Completed;

                process.Start();

                process.BeginErrorReadLine();
                process.BeginOutputReadLine();

                process.WaitForExit();


                return true;
            }
        }

        private void Data_Received(object sender, DataReceivedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(e.Data))
            {
                var progress = this.Progress;

                progress?.Invoke(new ProgressInfo(e.Data));
            }
        }

        private void Completed(object sender, EventArgs e)
        {
            var complete = this.BackupComplete;

            if (complete != null)
            {
                var batchFile = new BatchFile(this.BatchFileName);
                batchFile.Delete();

                complete(this, e);
            }
        }
    }
}