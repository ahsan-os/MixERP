using Frapid.Configuration.TenantServices;
using Frapid.Configuration.Tests.TenantServices.Fakes;
using Xunit;

namespace Frapid.Configuration.Tests.TenantServices
{
    public sealed class DomainValidatorTests
    {
        //See the following files for more info:
        // Fakes/FakeDomainSerializer.cs
        // Fakes/FakeLogger.cs

        [Theory]
        [InlineData("mixerp.org", true)]
        [InlineData("static.mixerp.org", true)]
        [InlineData("www.mixerp.org", true)]
        [InlineData("mixerp.com", true)]
        [InlineData("www.mixerp.com", true)]
        [InlineData("mixerp.net", true)]
        [InlineData("www.mixerp.net", true)]
        [InlineData("frapid.com", true)]
        [InlineData("cdn.frapid.com", true)]
        [InlineData("www.frapid.com", true)]
        [InlineData("example.com", true)]
        [InlineData("cdn.example.com", true)]
        [InlineData("www.example.com", true)]
        [InlineData("microsoft.com", false)]
        [InlineData("github.com", false)]
        public void ValidatorTest(string domainName, bool expected)
        {
            var logger = new FakeLogger();
            var seriazlier = new FakeDomainSerializer();

            var validator = new DomainValidator(logger, seriazlier);
            bool result = validator.IsValid(domainName);

            Assert.Equal(expected, result);
        }
    }
}