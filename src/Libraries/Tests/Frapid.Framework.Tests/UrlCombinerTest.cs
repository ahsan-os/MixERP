using Xunit;

namespace Frapid.Framework.Tests
{
    public sealed class UrlCombinerTest
    {
        [Fact]
        public void ShouldCombineUrl()
        {
            var combiner = new UrlCombiner();
            string expected = "mixerp.org/erp";
            string result = combiner.Combine("mixerp.org", "erp");
            Assert.Equal(expected, result);
        }

        [Fact]
        public void ShouldCombineWellFormedUrl()
        {
            var combiner = new UrlCombiner();
            string expected = "https://mixerp.org/erp";
            string result = combiner.Combine("https://mixerp.org", "erp");
            Assert.Equal(expected, result);
        }
    }
}