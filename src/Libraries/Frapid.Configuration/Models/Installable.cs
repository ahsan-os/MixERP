using System;
using System.Collections.Generic;
using System.Linq;
using Frapid.Mapper.Database;
using Newtonsoft.Json;

namespace Frapid.Configuration.Models
{
    public class Installable
    {
        public string DirectoryPath { get; set; }
        public string ApplicationName { get; set; }
        public bool AutoInstall { get; set; }
        public string Thumbnail { get; set; }
        public string Publisher { get; set; }
        public string Url { get; set; }
        public string DocumentationUrl { get; set; }
        public bool Hasi18N { get; set; }
        public string I18NTarget { get; set; }
        public string I18NSource { get; set; }
        public string I18NClassName { get; set; }
        public string AssemblyName { get; set; }
        public string Version { get; set; }
        public DateTime? RealeasedOn { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public string Bundle { get; set; }
        public bool IsMeta { get; set; }
        public string DbSchema { get; set; }
        public string BlankDbPath { get; set; }
        public string SampleDbPath { get; set; }
        public string PatchFilePath { get; set; }
        public bool InstallSample { get; set; }
        public string My { get; set; }
        public string OverrideTemplatePath { get; set; }
        public string OverrideDestination { get; set; }
        public string OverrideTenantProviderType { get; set; }//Empty value means all, Npgsql for PostgreSQL, System.Data.SqlClient for SQL Server

        [JsonIgnore]
        public List<Installable> Dependencies { get; private set; }

        public string[] DependsOn { get; set; }

        public void SetDependencies()
        {
            this.Dependencies = this.GetDependencies();
        }

        private List<Installable> GetDependencies()
        {
            var installables = new List<Installable>();

            if (this.DependsOn == null ||
                this.DependsOn.Length.Equals(0))
            {
                return installables;
            }

            var apps = AppResolver.Installables;

            foreach (var installable in apps.Where(installable => this.DependsOn.Contains(installable.ApplicationName)))
            {
                installable.SetDependencies();
                installables.Add(installable);
            }

            return installables;
        }
    }
}