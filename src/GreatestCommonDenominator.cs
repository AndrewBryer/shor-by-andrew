using System;

namespace Shor
{
    public class GreatestCommonDenominator
    {
        internal int findGCD(int firstNumber, int secondNumber)
        {
            if (firstNumber < 0 || secondNumber < 0)
            {
                throw new ArgumentException();
            }
            else if (firstNumber == 1 || secondNumber == 1) 
            {
                return 1;
            }
            else if (firstNumber == 0) 
            {
                return secondNumber;
            }
            else if (secondNumber == 0) 
            {
                return firstNumber;
            }
            else
            {
                return findGCD(secondNumber, firstNumber % secondNumber);
            }
        }
    }
}