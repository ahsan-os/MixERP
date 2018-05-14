using Frapid.Configuration.TenantServices;
using Frapid.Configuration.Tests.TenantServices.Fakes;
using Xunit;

namespace Frapid.Configuration.Tests.TenantServices
{
    public sealed class DomainFinderTests
    {
        //See the following files for more info:
        // Fakes/FakeDomainSerializer.cs
        // Fakes/FakeLogger.cs

        [Theory]
        [InlineData("localhost", "mixerp.net", "mixerp.org")]
        [InlineData("localhost", "www.mixerp.net", "mixerp.org")]
        [InlineData("localhost", "mixerp.com", "mixerp.org")]
        [InlineData("localhost", "www.mixerp.com", "mixerp.org")]
        [InlineData("localhost", "static.mixerp.org", "mixerp.org")]
        [InlineData("localhost", "www.frapid.com", "frapid.com")]
        [InlineData("localhost", "cdn.frapid.com", "frapid.com")]
        [InlineData("localhost", "www.example.com", "example.com")]
        [InlineData("localhost", "cdn.example.com", "example.com")]
        public void ShouldFindDomainByTenantName(string defaultSite, string synonym, string expectedDomain)
        {
            var logger = new FakeLogger();
            var serializer = new FakeDomainSerializer();
            var convention = new ByConvention(logger, serializer);

            var finder = new DomainFinder(serializer, convention);

            var found = finder.FindBySynonym(synonym, defaultSite);
            string result = found.DomainName;

            Assert.Equal(expectedDomain, result);
        }
    }
}