using Xunit;

namespace Frapid.Framework.Tests
{
    public sealed class OrStringTest
    {
        [Fact]
        public void ShouldFallBack()
        {
            var or = new OrString();
            string expected = "that";
            string result = or.Get("", "that");

            Assert.Equal(expected, result);
        }
    }
}