using Frapid.Configuration.TenantServices;
using Frapid.Configuration.Tests.TenantServices.Fakes;
using Xunit;

namespace Frapid.Configuration.Tests.TenantServices
{

    public sealed class TenantLocatorTests
    {
        //See the following files for more info:
        // Fakes/FakeDomainSerializer.cs
        // Fakes/FakeLogger.cs

        [Theory]
        [InlineData("mixerp.org", "mixerp_org")]
        [InlineData("static.mixerp.org", "mixerp_org")]
        [InlineData("www.mixerp.org", "mixerp_org")]
        [InlineData("mixerp.com", "mixerp_org")]
        [InlineData("www.mixerp.com", "mixerp_org")]
        [InlineData("mixerp.net", "mixerp_org")]
        [InlineData("www.mixerp.net", "mixerp_org")]
        [InlineData("frapid.com", "frapid_com")]
        [InlineData("cdn.frapid.com", "frapid_com")]
        [InlineData("www.frapid.com", "frapid_com")]
        [InlineData("example.com", "example_com")]
        [InlineData("cdn.example.com", "example_com")]
        [InlineData("www.example.com", "example_com")]
        //The following domains should resolve to "localhost" tenant because these domains are not present in the list of synonyms or approved domains.
        [InlineData("demo.mixerp.org", "localhost")]
        [InlineData("frapid.demo.mixerp.org", "localhost")]
        [InlineData("microsoft.com", "localhost")]
        public void TestConvention(string url, string expected)
        {
            string defaultTenant = "localhost";
            var logger = new FakeLogger();
            var serializer = new FakeDomainSerializer();
            var extractor = new DomainNameExtractor(logger);
            var convention = new ByConvention(logger, serializer);
            var validator = new TenantValidator(logger, serializer, convention);

            var locator = new TenantLocator(logger, extractor, convention, validator, serializer);

            string result = locator.FromUrl(url, defaultTenant);

            Assert.Equal(expected, result);
        }
    }
}