using System;
using System.Collections.Generic;
using Frapid.Backups.SqlServer;
using Frapid.Configuration;
using Quartz;
using Serilog;

namespace Frapid.Backups
{
    public class BackupJob : IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            string fileName = DateTimeOffset.UtcNow.Ticks.ToString();
            var domains = TenantConvention.GetDomains();

            foreach (var domain in domains)
            {
                string tenant = TenantConvention.GetDbNameByConvention(domain.DomainName);
                var directories = this.GetBackupDirectory(domain, tenant);


                var server = new DbServer(tenant);

                foreach (string directory in directories)
                {
                    var agent = this.GetAgent(server, fileName, tenant, directory);

                    try
                    {
                        agent.BackupAsync
                            (
                                done =>
                                {
                                    var backup = new Resources(tenant, directory, fileName);

                                    backup.AddTenantDataToBackup();
                                    backup.Compress();
                                    backup.Clean();
                                },
                                error => { Log.Error($"Could not backup because an error occurred. \n\n{error}"); })
                            .GetAwaiter()
                            .GetResult();
                    }
                    catch (Exception ex)
                    {
                        Log.Error("Exception occurred executing the backup job. {Exception}.", ex);
                    }
                }
            }
        }

        public List<string> GetBackupDirectory(ApprovedDomain domain, string tenant)
        {
            var directories = new List<string>();

            if (domain.BackupDirectoryIsFixedPath && !string.IsNullOrWhiteSpace(domain.BackupDirectory))
            {
                directories.Add(domain.BackupDirectory);
            }

            if (!string.IsNullOrWhiteSpace(domain.BackupDirectory))
            {
                directories.Add(PathMapper.MapPath(domain.BackupDirectory));
            }
            else
            {
                string path = $"/Backups/{tenant}/backup";
                directories.Add(PathMapper.MapPath(path));
            }

            foreach (var backup in domain.AlternativeBackups)
            {
                if (!string.IsNullOrWhiteSpace(backup.Path))
                {
                    if (backup.IsFixedPath)
                    {
                        directories.Add(backup.Path);
                    }
                    else
                    {
                        directories.Add(PathMapper.MapPath(backup.Path));
                    }
                }
            }


            return directories;
        }


        public IDbAgent GetAgent(DbServer server, string backupFileName, string tenant, string backupPath)
        {
            if (server.ProviderName.ToUpperInvariant().Equals("SQL SERVER"))
            {
                return new Agent
                {
                    Server = server,
                    FileName = backupFileName,
                    Tenant = tenant,
                    BackupFileLocation = backupPath
                };
            }


            return new Postgres.Agent
            {
                Server = server,
                FileName = backupFileName,
                Tenant = tenant,
                BackupFileLocation = backupPath
            };
        }
    }
}