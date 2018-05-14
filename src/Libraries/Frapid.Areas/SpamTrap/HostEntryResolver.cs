using System.Net;

namespace Frapid.Areas.SpamTrap
{
    public sealed class HostEntryResolver : IHostEntryResolver
    {
        public IPHostEntry GetHostEntry(string address)
        {
            return Dns.GetHostEntry(address);
        }
    }
}