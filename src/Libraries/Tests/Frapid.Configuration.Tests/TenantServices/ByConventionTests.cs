using Frapid.Configuration.TenantServices;
using Frapid.Configuration.Tests.TenantServices.Fakes;
using Xunit;

namespace Frapid.Configuration.Tests.TenantServices
{
    public sealed class ByConventionTests
    {
        //See the following files for more info:
        // Fakes/FakeDomainSerializer.cs
        // Fakes/FakeLogger.cs

        [Theory]
        [InlineData("www.mixerp.org", "mixerp_org")]
        [InlineData("demo.mixerp.org", "demo_mixerp_org")]
        [InlineData("frapid.demo.mixerp.org", "frapid_demo_mixerp_org")]
        public void TestConvention(string domain, string expected)
        {
            var logger = new FakeLogger();
            var serializer = new FakeDomainSerializer();

            var convention = new ByConvention(logger, serializer);
            string result = convention.GetTenantName(domain);

            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData("", "www.mixerp.org", "mixerp_org")]
        [InlineData("", "demo.mixerp.org", "demo_mixerp_org")]
        [InlineData("", "frapid.demo.mixerp.org", "frapid_demo_mixerp_org")]
        public void TestOrConvention(string domain, string or, string expected)
        {
            var logger = new FakeLogger();
            var serializer = new FakeDomainSerializer();

            var convention = new ByConvention(logger, serializer);
            string result = convention.GetTenantName(domain, or);

            Assert.Equal(expected, result);
        }
    }
}