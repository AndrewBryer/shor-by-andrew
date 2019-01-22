using System;
using Xunit;

namespace Shor
{
    public class GreatestCommonDenominatorTest
    {
        [Theory]
        [InlineData(-1, 1)]
        [InlineData(-1, 34)]
        [InlineData(-34, 1)]
        [InlineData(-101, 1)]
        public void GCDThrowsErrorWhenGivenNonNaturalNumber(int firstNumber, int secondNumber) {
            GreatestCommonDenominator gcd = new GreatestCommonDenominator();
            Action act = () => gcd.findGCD(firstNumber, secondNumber);
            Assert.Throws<ArgumentException>(act);
        }

        [Theory]
        [InlineData(1, 2)]
        [InlineData(1, 34)]
        [InlineData(2, 1)]
        [InlineData(34, 1)]
        public void GCDIsOneWhenOneParameterIsOne(int firstNumber, int secondNumber) {
            GreatestCommonDenominator gcd = new GreatestCommonDenominator();
            int value = gcd.findGCD(firstNumber, secondNumber);
            Assert.Equal(1, value);
        }

        [Theory]
        [InlineData(2)]
        [InlineData(5)]
        [InlineData(7)]
        [InlineData(34)]
        public void GCDIsSecondParameterWhenFirstParameterIsZero(int nonZeroNumber) {
            GreatestCommonDenominator gcd = new GreatestCommonDenominator();
            int value= gcd.findGCD(0, nonZeroNumber);
            Assert.Equal(nonZeroNumber, value);
        }
        
        [Theory]
        [InlineData(2)]
        [InlineData(5)]
        [InlineData(7)]
        [InlineData(34)]
        public void GCDIsFirstParameterWhenSecondParameterIsZero(int nonZeroNumber) {
            GreatestCommonDenominator gcd = new GreatestCommonDenominator();
            int value= gcd.findGCD(nonZeroNumber, 0);
            Assert.Equal(nonZeroNumber, value);
        }

        [Theory]
        [InlineData(2, 3, 1)]
        [InlineData(100, 25, 25)]
        [InlineData(63, 21, 21)]
        [InlineData(823, 527, 1)]
        public void GCDIsCorrectForSampleTests(int firstNumber, int secondNumber, int expectedResult) {
            GreatestCommonDenominator gcd = new GreatestCommonDenominator();
            int value = gcd.findGCD(firstNumber, secondNumber);
            Assert.Equal(expectedResult, value);
        }
    }
}