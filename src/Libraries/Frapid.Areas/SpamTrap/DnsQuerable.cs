using System;
using System.Linq;

namespace Frapid.Areas.SpamTrap
{
    public sealed class DnsQueryable : IDnsQueryable
    {
        public DnsQueryable(IHostEntryResolver resolver)
        {
            this.Resolver = resolver;
        }

        public IHostEntryResolver Resolver { get; set; }

        public bool Query(string address)
        {
            try
            {
                var result = this.Resolver.GetHostEntry(address);
                return result.AddressList.Any();
            }
            catch (Exception)
            {
                //Swallow
            }

            return false;
        }
    }
}