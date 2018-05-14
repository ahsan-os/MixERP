using System;
using System.Collections.Generic;

namespace Frapid.SchemaUpdater.ViewModels
{
    public sealed class HomeViewModel
    {
        public IEnumerable<UpdateCandidate> Updatables { get; set; }
        public DateTimeOffset? LastInstalledOn { get; set; }
    }
}