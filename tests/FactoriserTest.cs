using System;
using Xunit;

namespace Shor
{
    public class FactoriserTest
    {
        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        [InlineData(1)]
        public void FactoriseReturnsErrorWhenLessThanTwo(int numberLessThanTwo) {
            Factoriser factoriser = new Factoriser();
            Action act = () => factoriser.factorise(numberLessThanTwo);
            Assert.Throws<ArgumentException>(act);
        }

        [Theory]
        [InlineData(2)]
        [InlineData(10)]
        [InlineData(16)]
        public void FactoriseReturnsTwoAndOtherFactorWhenNumberIsEven(int evenNumber) {
            Factoriser factoriser = new Factoriser();
            (int, int) factors = factoriser.factorise(evenNumber);
            Assert.Equal(factors, (2, evenNumber / 2));
        }
    }
}