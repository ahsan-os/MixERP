using System.Collections.Generic;
using System.Threading.Tasks;
using Frapid.Configuration.Models;

namespace Frapid.i18n.ResourceBuilder
{
    public interface IResourceReader
    {
        Task<Dictionary<string, string>> GetResourcesAsync(string tenant, Installable app, string path);
    }
}