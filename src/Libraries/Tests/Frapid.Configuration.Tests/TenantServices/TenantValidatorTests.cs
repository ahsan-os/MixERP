using Frapid.Configuration.TenantServices;
using Frapid.Configuration.Tests.TenantServices.Fakes;
using Xunit;

namespace Frapid.Configuration.Tests.TenantServices
{
    public sealed class TenantValidatorTests
    {
        [Theory]
        [InlineData("mixerp_org", true)]
        [InlineData("static_mixerp_org", false)]//Because static.mixerp.org is a CDN domain. A CDN domain points back the default tenant.
        [InlineData("www_mixerp_org", false )]//Because www.mixerp.org is a synonym for mixerp.org.
        [InlineData("mixerp_com", false)]//Because mixerp.com is a synonym for mixerp.org.
        [InlineData("www_mixerp_com", false)]//Because www.mixerp.com is a synonym for mixerp.org.
        [InlineData("mixerp_net", false)]//Because mixerp.net is a synonym for mixerp.org.
        [InlineData("www_mixerp_net", false)]//Because www.mixerp.net is a synonym for mixerp.org.
        [InlineData("frapid_com", true)]
        [InlineData("cdn_frapid_com", false)]
        [InlineData("www_frapid_com", false)]
        [InlineData("example_com", true)]
        [InlineData("cdn_example_com", false)]
        [InlineData("www_example_com", false)]
        [InlineData("microsoft_com", false)]
        [InlineData("github_com", false)]
        public void ValidatorTest(string tenantName, bool expected)
        {
            var logger = new FakeLogger();
            var seriazlier = new FakeDomainSerializer();
            var convention = new ByConvention(logger, seriazlier);

            var validator = new TenantValidator(logger, seriazlier, convention);
            bool result = validator.IsValid(tenantName);

            Assert.Equal(expected, result);
        }
    }
}