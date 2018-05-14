using System;
using System.Globalization;
using System.IO;
using System.Text;

namespace Frapid.Backups.Postgres
{
    internal class BatchFile
    {
        public string Tenant;

        public BatchFile(string fileName)
        {
            this.FileName = fileName;
        }

        public BatchFile(string fileName, DbServer server, string pgDumpPath, string tenant)
        {
            this.FileName = fileName;
            this.Server = server;
            this.PgDumpPath = pgDumpPath;
            this.Tenant = tenant;
        }

        public DbServer Server { get; }
        public string PgDumpPath { get; }
        public string FileName { get; }

        public string Create()
        {
            var commands = new StringBuilder();

            commands.Append("@echo off");
            commands.Append(Environment.NewLine);
            commands.Append("SET PGPASSWORD=" + this.Server.Password);
            commands.Append(Environment.NewLine);

            string command = @"""{0}"" --host ""{1}"" --port {2} --username ""{3}"" --format custom --blobs --verbose --file ""{4}"" ""{5}""";

            command = string.Format(CultureInfo.InvariantCulture, command, this.PgDumpPath, this.Server.HostName, this.Server.PortNumber, this.Server.UserId, this.FileName, this.Tenant);
            commands.Append(command);
            commands.Append(Environment.NewLine);
            commands.Append("exit");

            string file = this.FileName + ".bat";

            File.WriteAllText(file, string.Join(Environment.NewLine, commands.ToString()), new UTF8Encoding(false));

            return file;
        }

        public void Delete()
        {
            if (File.Exists(this.FileName))
            {
                File.Delete(this.FileName);
            }
        }
    }
}