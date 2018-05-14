using System.Collections.Generic;
using System.Threading.Tasks;

namespace Frapid.Dashboard
{
    public interface ICustomJavascriptVariable
    {
        Task<Dictionary<string, string>> GetAsync(string tenant, int officeId);
    }
}