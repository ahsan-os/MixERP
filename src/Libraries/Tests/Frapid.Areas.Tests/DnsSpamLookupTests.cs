using Frapid.Areas.SpamTrap;
using Frapid.Areas.Tests.Fakes;
using Xunit;

namespace Frapid.Areas.Tests
{
    public sealed class DnsSpamLookupTests
    {
        [Theory]
        [InlineData("192.168.0.1", false)]
        [InlineData("192.168.0.2", true)]
        [InlineData("8.8.8.8", false)]
        [InlineData("10.1.1.1", true)]
        [InlineData("10.2.2.1", true)]
        public void IsListedInSpamDatabaseTest(string ipAddress, bool isSpammer)
        {
            var lookupServers = new []{""};
            var reverser = new IpAddressReverser();
            var resolver = new FakeHostEntryResolver();
            var queryable = new DnsQueryable(resolver);

            var lookup = new DnsSpamLookup(reverser, queryable, lookupServers);
            var result = lookup.IsListedInSpamDatabase(ipAddress);

            Assert.Equal(isSpammer, result.IsListed);
        }
    }
}