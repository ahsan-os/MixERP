using System.Collections.Generic;
using System.Threading.Tasks;

namespace Frapid.Framework
{
    public interface ISiteMapGenerator
    {
        Task<List<SiteMapUrl>> GenerateAsync(string tenant);
    }
}