using Frapid.Configuration.TenantServices;
using Frapid.Configuration.Tests.TenantServices.Fakes;
using Xunit;

namespace Frapid.Configuration.Tests.TenantServices
{
    public sealed class StaticDomainCheckTests
    {
        //See the following files for more info:
        // Fakes/FakeDomainSerializer.cs
        // Fakes/FakeLogger.cs
        [Theory]
        [InlineData("mixerp.org", false)]
        [InlineData("static.mixerp.org", true)]
        [InlineData("www.mixerp.org", false)]
        [InlineData("mixerp.com", false)]
        [InlineData("www.mixerp.com", false)]
        [InlineData("mixerp.net", false)]
        [InlineData("www.mixerp.net", false)]
        [InlineData("frapid.com", false)]
        [InlineData("cdn.frapid.com", true)]
        [InlineData("www.frapid.com", false)]
        [InlineData("example.com", false)]
        [InlineData("cdn.example.com", true)]
        [InlineData("www.example.com", false)]
        [InlineData("microsoft.com", false)]
        [InlineData("github.com", false)]
        public void CheckIfStaticDomainTest(string domain, bool isStatic)
        {
            var logger = new FakeLogger();
            var serializer = new FakeDomainSerializer();

            var check = new StaticDomainCheck(logger, serializer);
            bool result = check.IsStaticDomain(domain);

            Assert.Equal(isStatic, result);
        }
    }
}