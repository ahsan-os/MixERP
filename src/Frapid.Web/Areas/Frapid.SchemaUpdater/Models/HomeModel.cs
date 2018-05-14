using System.Linq;
using Frapid.SchemaUpdater.Helpers;
using Frapid.SchemaUpdater.ViewModels;

namespace Frapid.SchemaUpdater.Models
{
    public static class HomeModel
    {
        public static HomeViewModel GetViewModel(string tenant)
        {
            var updatables = AppDiscovery.GetUpdatables(tenant).ToList();
            var lastInstalledOn = updatables.Max(x => x.InstalledVersions.Max(y => y.LastInstalledOn));

            return new HomeViewModel
            {
                Updatables = updatables,
                LastInstalledOn = lastInstalledOn
            };
        }
    }
}