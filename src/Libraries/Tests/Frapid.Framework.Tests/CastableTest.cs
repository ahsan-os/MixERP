using System;
using Xunit;

namespace Frapid.Framework.Tests
{
    public sealed class CastableTest
    {
        [Fact]
        public void ShouldCastToDate()
        {
            var castable = new Castable();
            var expected = new DateTime(2000, 1, 1, 1, 1, 1);
            var result = castable.To<DateTime>("2000/1/1 01:01:01");
            Assert.Equal(expected, result);
        }

        [Fact]
        public void ShouldCastToDecimal()
        {
            var castable = new Castable();
            decimal expected = 2.3m;
            decimal result = castable.To<decimal>("2.3");
            Assert.Equal(expected, result);
        }

        [Fact]
        public void ShouldCastToDouble()
        {
            var castable = new Castable();
            double expected = 2.3;
            double result = castable.To<double>("2.3");
            Assert.Equal(expected, result);
        }

        [Fact]
        public void ShouldCastToInt()
        {
            var castable = new Castable();
            int expected = 2;
            int result = castable.To<int>("2");
            Assert.Equal(expected, result);
        }
    }
}