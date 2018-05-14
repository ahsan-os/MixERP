using System.Collections.Generic;

namespace Frapid.Framework.StaticContent
{
    public interface IRegisterableAssets
    {
        string AppName { get; }
        string AssetGroupName { get; }
        IEnumerable<Asset> Assets { get; }
    }
}