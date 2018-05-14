using System.Collections.Generic;

namespace Frapid.Configuration
{
    public class ApprovedDomain
    {
        public string DbProvider { get; set; }
        public string DomainName { get; set; }
        public string BackupDirectory { get; set; }
        public bool BackupDirectoryIsFixedPath { get; set; }
        public bool EnforceSsl { get; set; }
        public string CdnDomain { get; set; }
        public string AdminEmail { get; set; }
        /// <summary>
        /// Bcrypted password of the admin user. 
        /// If both AdminEmail and this property is set,
        /// frapid will automatically create user account
        /// during setup.
        /// </summary>
        public string BcryptedAdminPassword { get; set; }
        public string[] Synonyms { get; set; }

        public object Do { get; internal set; }
        public List<AlternativeBackup> AlternativeBackups { get; set; }

        public List<string> GetSubtenants()
        {
            var subtenants = new List<string>
            {
                this.DomainName,
                this.CdnDomain
            };

            if (this.Synonyms != null)
            {
                subtenants.AddRange(this.Synonyms);
            }

            return subtenants;
        }
    }
}