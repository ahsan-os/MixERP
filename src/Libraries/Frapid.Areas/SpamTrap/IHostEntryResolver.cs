using System.Net;

namespace Frapid.Areas.SpamTrap
{
    public interface IHostEntryResolver
    {
        IPHostEntry GetHostEntry(string address);
    }
}