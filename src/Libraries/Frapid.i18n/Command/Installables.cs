using System;
using System.Collections.Generic;
using System.Linq;
using Frapid.Configuration;
using Frapid.Configuration.Models;
using Frapid.i18n.InternalCache;

namespace Frapid.i18n.Command
{
    public static class Installables
    {
        public static IEnumerable<Installable> GetInstallables()
        {
            string cacheKey = $"installables_apps";
            var factory = new RedisCacheFactory();
            var installables = factory.Get<IEnumerable<Installable>>(cacheKey);

            if (installables == null)
            {
                var fromStore = FromStore().ToList();
                factory.Add(cacheKey, fromStore, DateTimeOffset.Now.AddMinutes(2));

                installables = fromStore;
            }

            return installables;
        }

        private static IEnumerable<Installable> FromStore()
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