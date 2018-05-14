using Xunit;

namespace Frapid.Framework.Tests
{
    public class EnumConverterTest
    {
        public enum FooEnum
        {
            All = 1,
            None = 0
        }

        [Fact]
        public void ShouldParseEnumFromString()
        {
            var converter = new EnumConverter();
            var expected = FooEnum.All;
            var result = converter.ToEnum("All", FooEnum.None);
            Assert.Equal(expected, result);
        }
    }
}