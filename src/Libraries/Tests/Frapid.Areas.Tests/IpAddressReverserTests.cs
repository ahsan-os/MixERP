using Frapid.Areas.SpamTrap;
using Xunit;

namespace Frapid.Areas.Tests
{
    public sealed class IpAddressReverserTests
    {
        [Theory]
        [InlineData("192.168.0.1", "1.0.168.192")]
        [InlineData("10.11.12.13", "13.12.11.10")]
        [InlineData("10.0.0.1", "1.0.0.10")]
        public void ReverseTests(string ipAddress, string expected)
        {
            var reverser = new IpAddressReverser();

            string result = reverser.Reverse(ipAddress);

            Assert.Equal(expected, result);
        }
    }
}