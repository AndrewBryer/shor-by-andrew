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
    }
}