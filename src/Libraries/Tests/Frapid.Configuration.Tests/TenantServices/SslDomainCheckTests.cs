using Frapid.Configuration.TenantServices;
using Frapid.Configuration.Tests.TenantServices.Fakes;
using Xunit;

namespace Frapid.Configuration.Tests.TenantServices
{
    public sealed class SslDomainCheckTests
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
        [InlineData("frapid.com", false)]
        [InlineData("cdn.frapid.com", false)]
        [InlineData("www.frapid.com", false)]
        [InlineData("example.com", true)]
        [InlineData("cdn.example.com", true)]
        [InlineData("www.example.com", true)]
        [InlineData("microsoft.com", false)]
        [InlineData("github.com", false)]
        public void CheckIfSslIsEnforcedTest(string domain, bool shouldBeEnforced)
        {
            var logger = new FakeLogger();
            var serializer = new FakeDomainSerializer();

            var check = new SslDomainCheck(logger, serializer);
            bool result = check.EnforceSsl(domain);

            Assert.Equal(shouldBeEnforced, result);
        }
    }
}