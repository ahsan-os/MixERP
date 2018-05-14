using Frapid.Configuration.TenantServices;
using Frapid.Configuration.Tests.TenantServices.Fakes;
using Xunit;

namespace Frapid.Configuration.Tests.TenantServices
{
    public sealed class BaseDomainLocatorTests
    {
        //See the following files for more info:
        // Fakes/FakeDomainSerializer.cs
        // Fakes/FakeLogger.cs

        [Fact]
        public void ShouldLocateNonSecureDomainWithSchemeTest()
        {
            var domains = new FakeDomainSerializer();
            var locator = new BaseDomainLocator(domains);
            string domain = "www.example.com";

            string expected = "http://example.com";
            string result = locator.Get(domain, false, true);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void ShouldLocateSecureDomainWithSchemeTest()
        {
            var domains = new FakeDomainSerializer();
            var locator = new BaseDomainLocator(domains);
            string domain = "www.example.com";

            string expected = "https://example.com";
            string result = locator.Get(domain, true, true);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void ShouldLocateDomainWithoutSchemeTest()
        {
            var domains = new FakeDomainSerializer();
            var locator = new BaseDomainLocator(domains);
            string domain = "www.example.com";

            string expected = "example.com";
            string result = locator.Get(domain, false, false);

            Assert.Equal(expected, result);
        }
    }
}