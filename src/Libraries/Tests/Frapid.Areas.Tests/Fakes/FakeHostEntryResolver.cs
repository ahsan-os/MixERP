using System.Collections.Generic;
using System.Net;
using Frapid.Areas.SpamTrap;

namespace Frapid.Areas.Tests.Fakes
{
    public sealed class FakeHostEntryResolver : IHostEntryResolver
    {
        public List<IPAddress> Spammers { get; }
        public List<IPAddress> FakeRblServers { get; }

        public FakeHostEntryResolver()
        {
            this.Spammers = new List<IPAddress>
                           {
                               this.Get("192.168.0.2"),
                               this.Get("10.2.2.1"),
                               this.Get("10.1.1.1")
                           };

            this.FakeRblServers = new List<IPAddress>
                           {
                               this.Get("104.31.95.40")
                           };

        }
        private IPAddress Get(string address)
        {
            IPAddress ipAddress;
            IPAddress.TryParse(address, out ipAddress);

            return ipAddress;
        }

        public IPHostEntry GetHostEntry(string address)
        {
            var reverser = new IpAddressReverser();
            string server = string.Empty;

            foreach (var ipAddress in this.Spammers)
            {
                string reversed = reverser.Reverse(ipAddress.ToString()) + "." + server;
                if (reversed.Equals(address))
                {
                    return new IPHostEntry
                    {
                        AddressList = this.FakeRblServers.ToArray()
                    };
                }
            }

            return new IPHostEntry();
        }
    }
}