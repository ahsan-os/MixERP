using System.Collections.Generic;
using Frapid.Configuration.Models;

namespace Frapid.Configuration
{
    public static class Installables
    {
        public static IEnumerable<Installable> GetInstallables(string tenant)
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

                if (app.AutoInstall)
                {
                    installables.Add(app);
                }
            }

            return installables;
        }
    }
}