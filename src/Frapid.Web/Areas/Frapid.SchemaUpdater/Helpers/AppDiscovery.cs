using System.Collections.Generic;
using System.Linq;
using Frapid.Configuration;
using Frapid.Configuration.Models;

namespace Frapid.SchemaUpdater.Helpers
{
    public static class AppDiscovery
    {
        public static IEnumerable<Installable> Discover()
        {
            string root = PathMapper.MapPath("~/");
            var installables = new List<Installable>();

            if (root == null)
            {
                return installables;
            }

            var apps = AppResolver.Installables;

            foreach (var app in apps)
            {
                app.SetDependencies();
                installables.Add(app);
            }

            return installables;
        }

        public static IEnumerable<UpdateCandidate> GetUpdatables(string tenant)
        {
            var apps = Discover();

            return apps.Select(app => new UpdateCandidate(app, tenant))
                .Where(candidate => !string.IsNullOrWhiteSpace(candidate.PathToUpdateFile))
                .ToList();
        }
    }
}