using System.Web.Mvc;
using Frapid.Areas.CSRF;
using Frapid.Areas.Tests.Fakes;
using Frapid.Configuration.TenantServices;
using Xunit;

namespace Frapid.Areas.Tests
{
    public sealed class AntiforgeryTokenGeneratorTests
    {
        [Theory]
        [InlineData("mixerp.org", "Token")]
        [InlineData("static.mixerp.org", "")]
        [InlineData("www.mixerp.org", "Token")]
        [InlineData("mixerp.com", "Token")]
        [InlineData("www.mixerp.com", "Token")]
        [InlineData("mixerp.net", "Token")]
        [InlineData("www.mixerp.net", "Token")]
        [InlineData("frapid.com", "Token")]
        [InlineData("cdn.frapid.com", "")]
        [InlineData("www.frapid.com", "Token")]
        [InlineData("example.com", "Token")]
        [InlineData("cdn.example.com", "")]
        [InlineData("www.example.com", "Token")]
        public void Test(string currentDomain, string expectedToken)
        {
            var logger = new FakeLogger();
            var serializer = new FakeDomainSerializer();
            var check = new StaticDomainCheck(logger, serializer);
            var tokenizer = new FakeTokenizer();

            var generator = new AntiforgeryTokenGenerator(check, tokenizer, currentDomain);
            var result = generator.GetAntiForgeryToken();
            var expected = new MvcHtmlString(expectedToken);

            Assert.Equal(expected.ToString(), result.ToString());
        }
    }
}