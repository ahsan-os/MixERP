namespace Frapid.Areas.SpamTrap
{
    public sealed class DnsSpamLookup : IDnsSpamLookup
    {
        public DnsSpamLookup(IIpAddressReverser reverser, IDnsQueryable queryable, string[] rblServers)
        {
            this.Reverser = reverser;
            this.Queryable = queryable;
            this.RblServers = rblServers;
        }

        public IIpAddressReverser Reverser { get; set; }
        public IDnsQueryable Queryable { get; set; }
        public string[] RblServers { get; set; }

        public DnsSpamLookupResult IsListedInSpamDatabase(string ipAddress)
        {
            string prefix = this.Reverser.Reverse(ipAddress);

            foreach (string server in this.RblServers)
            {
                string address = prefix + "." + server;
                bool result = this.Queryable.Query(address);

                if (result)
                {
                    return new DnsSpamLookupResult
                    {
                        IpAddress = ipAddress,
                        RblServer = server,
                        IsListed = true
                    };
                }
            }

            return new DnsSpamLookupResult();
        }
    }
}