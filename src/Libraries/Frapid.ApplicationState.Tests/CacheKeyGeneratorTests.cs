using System;
using Frapid.ApplicationState.Tests.Fakes;
using Frapid.Areas.Caching;
using Frapid.Configuration.TenantServices;
using Xunit;

namespace Frapid.ApplicationState.Tests
{
    public sealed class CacheKeyGeneratorTests
    {
        [Theory]
        [InlineData("http://mixerp.org/erp/image.png", "mixerp_org.erp.image.png")]
        [InlineData("http://www.mixerp.org/erp/image.png", "mixerp_org.erp.image.png")]
        [InlineData("http://mixerp.com/erp/image.png", "mixerp_org.erp.image.png")]
        [InlineData("http://www.mixerp.com/erp/image.png", "mixerp_org.erp.image.png")]
        [InlineData("http://mixerp.net/erp/image.png", "mixerp_org.erp.image.png")]
        [InlineData("http://www.mixerp.net/erp/image.png", "mixerp_org.erp.image.png")]
        [InlineData("http://static.mixerp.org/erp/image.png", "mixerp_org.erp.image.png")]
        [InlineData("https://mixerp.org/erp/image.png", "mixerp_org.erp.image.png")]
        [InlineData("https://www.mixerp.org/erp/image.png", "mixerp_org.erp.image.png")]
        [InlineData("https://mixerp.com/erp/image.png", "mixerp_org.erp.image.png")]
        [InlineData("https://www.mixerp.com/erp/image.png", "mixerp_org.erp.image.png")]
        [InlineData("https://mixerp.net/erp/image.png", "mixerp_org.erp.image.png")]
        [InlineData("https://www.mixerp.net/erp/image.png", "mixerp_org.erp.image.png")]
        [InlineData("https://static.mixerp.org/erp/image.png", "mixerp_org.erp.image.png")]
        [InlineData("http://frapid.com/applications/all/result.json", "frapid_com.applications.all.result.json")]
        [InlineData("http://www.frapid.com/applications/all/result.json", "frapid_com.applications.all.result.json")]
        [InlineData("http://cdn.frapid.com/applications/all/result.json", "frapid_com.applications.all.result.json")]
        [InlineData("https://frapid.com/applications/all/result.json", "frapid_com.applications.all.result.json")]
        [InlineData("https://www.frapid.com/applications/all/result.json", "frapid_com.applications.all.result.json")]
        [InlineData("https://cdn.frapid.com/applications/all/result.json", "frapid_com.applications.all.result.json")]
        [InlineData("http://example.com/music/store/albums.json", "example_com.music.store.albums.json")]
        [InlineData("http://www.example.com/music/store/albums.json", "example_com.music.store.albums.json")]
        [InlineData("http://cdn.example.com/music/store/albums.json", "example_com.music.store.albums.json")]
        [InlineData("https://example.com/music/store/albums.json", "example_com.music.store.albums.json")]
        [InlineData("https://www.example.com/music/store/albums.json", "example_com.music.store.albums.json")]
        [InlineData("https://cdn.example.com/music/store/albums.json", "example_com.music.store.albums.json")]
        [InlineData("https://microsoft.com/image.png", "localhost.image.png")]
        [InlineData("http://microsoft.com/image.png", "localhost.image.png")]
        public void TestGenerateMethod(string url, string expected)
        {
            var logger = new FakeLogger();
            var extractor = new DomainNameExtractor(logger);
            var serializer = new FakeDomainSerializer();
            var byConvention = new ByConvention(logger, serializer);
            var validator = new TenantValidator(logger, serializer, byConvention);
            const string defaultTenant = "localhost";


            var locator = new TenantLocator(logger, extractor, byConvention, validator, serializer);
            var uri = new Uri(url);
            var keyGen = new CacheKeyGenerator(uri, locator, defaultTenant);

            string result = keyGen.Generate();

            Assert.Equal(expected, result);
        }
    }
}
