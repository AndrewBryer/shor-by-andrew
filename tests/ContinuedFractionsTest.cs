using System;
using Xunit;

namespace Shor
{
    public class ContinuedFractionsTest
    {
        [Theory]
        [InlineData(-1, 1)]
        public void ContinuedFractionsShouldThrowErrorWhenNumeratorIsLessThanZero(int numerator, int denominator)
        {
            ContinuedFractions cf = new ContinuedFractions();
            Action act = () => cf.findNext(numerator, denominator);
            Assert.Throws<ArgumentException>(act);
        }

        [Theory]
        [InlineData(1, 0)]
        [InlineData(1, -1)]
        public void ContinuedFractionsShouldThrowErrorWhenDenominatorIsZeroOrLess(int numerator, int denominator)
        {
            ContinuedFractions cf = new ContinuedFractions();
            Action act = () => cf.findNext(numerator, denominator);
            Assert.Throws<ArgumentException>(act);
        }

        [Theory]
        [InlineData(0, 1)]
        [InlineData(0, 10)]
        public void ContinuedFractionsShouldReturnValueOfZeroToNumeratorAndNewValuesAsZero(int numerator, int denominator)
        {
            ContinuedFractions cf = new ContinuedFractions();
            (int, int, int) value = cf.findNext(numerator, denominator);
            Assert.Equal((0, 0, 0), value);
        }

        [Theory]
        [InlineData(1, 1)]
        [InlineData(5, 1)]
        [InlineData(10, 1)]
        public void ContinuedFractionsShouldReturnValueAsNumeratorIfDenominatorIsOne(int numerator, int denominator)
        {
            ContinuedFractions cf = new ContinuedFractions();
            (int, int, int) value = cf.findNext(numerator, denominator);
            Assert.Equal((numerator, 0, 0), value);
        }

        [Theory]
        [InlineData(2, 3)]
        [InlineData(5, 10)]
        [InlineData(34, 101)]
        public void ContinuedFractionsShouldReturnValueAsZeroIfDenominatorIsBiggerThanNumerator(int numerator, int denominator)
        {
            ContinuedFractions cf = new ContinuedFractions();
            (int, int, int) value = cf.findNext(numerator, denominator);
            Assert.Equal(0, value.Item1);
        }

        [Theory]
        [InlineData(3, 2)]
        [InlineData(10, 5)]
        [InlineData(101, 34)]
        public void ContinuedFractionsShouldReturnCorrectNextValues(int numerator, int denominator)
        {
            ContinuedFractions cf = new ContinuedFractions();
            (int, int, int) value = cf.findNext(numerator, denominator);
            int gcd = new GreatestCommonDenominator().findGCD(denominator, numerator % denominator);
            Assert.Equal((numerator / denominator, denominator / gcd, (numerator % denominator) / gcd), value);
        }
    }
}