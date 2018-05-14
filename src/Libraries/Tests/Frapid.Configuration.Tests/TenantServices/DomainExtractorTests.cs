using Frapid.Configuration.TenantServices;
using Frapid.Configuration.Tests.TenantServices.Fakes;
using Xunit;

namespace Frapid.Configuration.Tests.TenantServices
{
    public sealed class DomainExtractorTests
    {
        //See the following files for more info:
        // Fakes/FakeDomainSerializer.cs
        // Fakes/FakeLogger.cs

        [Theory]
        [InlineData("mixerp.org", "mixerp.org")]
        [InlineData("http://mixerp.org", "mixerp.org")]
        [InlineData("https://mixerp.org", "mixerp.org")]
        [InlineData("https://www.mixerp.org", "mixerp.org")]
        [InlineData("https://demo.mixerp.org", "demo.mixerp.org")]
        [InlineData("https://frapid.demo.mixerp.org", "frapid.demo.mixerp.org")]
        [InlineData("https://www.frapid.demo.mixerp.org", "frapid.demo.mixerp.org")]
        public void ShouldExtractDomainNameTest(string url, string expected)
        {
            var logger = new FakeLogger();
            var extractor = new DomainNameExtractor(logger);
            string result = extractor.GetDomain(url);
            Assert.Equal(expected, result);
        }
    }
}