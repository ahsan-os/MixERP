using System.Collections.Generic;
using System.Linq;
using Frapid.Configuration.Models;
using Frapid.Framework.Extensions;
using Frapid.SchemaUpdater.Helpers;

namespace Frapid.SchemaUpdater
{
    public sealed class UpdateCandidate
    {
        public UpdateCandidate(Installable app, string tenant)
        {
            this.AppInfo = app;
            this.AllVersions = VersionInvestigator.GetVersionsOnDisk(app, tenant);
            this.InstalledVersions = VersionManager.GetVersions(tenant, app.DbSchema);
            this.VersionToUpdate = this.AllVersions.OrderByDescending(x => x.VersionNumber.To<double>()).FirstOrDefault();
            this.PathToUpdateFile = VersionPathConvention.GetFilePath(app, tenant, this.VersionToUpdate);
        }

        public IEnumerable<SchemaVersion> InstalledVersions { get; }
        public IEnumerable<SchemaVersion> AllVersions { get; }
        public Installable AppInfo { get; }
        public SchemaVersion VersionToUpdate { get; }
        public string PathToUpdateFile { get; }
    }
}