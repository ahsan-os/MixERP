using System.Linq;

namespace Frapid.Areas.SpamTrap
{
    public sealed class IpAddressReverser : IIpAddressReverser
    {
        public string Reverse(string ipAddress)
        {
            var segments = ipAddress.Split('.');
            return string.Join(".", segments.Reverse());
        }
    }
}