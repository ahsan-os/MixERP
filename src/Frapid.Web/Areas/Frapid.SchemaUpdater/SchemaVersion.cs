using System;

namespace Frapid.SchemaUpdater
{
    public sealed class SchemaVersion
    {
        public string SchemaName { get; set; }
        public string VersionNumber { get; set; }
        public DateTimeOffset? LastInstalledOn { get; set; }
    }
}