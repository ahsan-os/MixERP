using Frapid.AssetBundling;
using Frapid.Framework.Extensions;
using Frapid.Framework.StaticContent;
using Serilog;

namespace Frapid.Web.Application
{
    public static class AssetConfig
    {
        public static void Register()
        {
            //You can register your own assets on each of your module by implementing IRegisterableAssets.
            //You can also override default assets found in "/Resources/Configs/Assets" directory by the key "BundleName".
            PerformDiscovery();
        }

        private static void PerformDiscovery()
        {
            Log.Verbose("Discovering registrable assets.");
            var type = typeof(IRegisterableAssets);
            var members = type.GetTypeMembers<IRegisterableAssets>();

            foreach (var registerable in members)
            {
                if (registerable == null)
                {
                    continue;
                }

                Log.Information("Registering asset group \"{AssetGroupName}\" of App \"{AppName}\"", registerable.AssetGroupName, registerable.AppName);

                foreach (var asset in registerable.Assets)
                {
                    Log.Information("Registering asset bundle \"{BundleName}\" from group \"{AssetGroupName}\" of App \"{AppName}\"", asset.BundleName, registerable.AssetGroupName,
                        registerable.AppName);
                    var bundle = new Registration(asset);
                    bundle.Resiter();
                }
            }
        }
    }
}