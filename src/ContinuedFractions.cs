using System;
using System.Collections.Generic;

namespace Shor
{
    public class ContinuedFractions
    {
        public int findS(int numerator, int denominator, int modulus)
        {
            List<int> numbers = new List<int>();
            while (calculateS(numbers) < modulus && numerator > 0)
            {
                (int, int, int) next = findNext(numerator, denominator);
                numbers.Add(next.Item1);
                numerator = next.Item2;
                denominator = next.Item3;
            }
            if (calculateS(numbers) >= modulus)
            {
                numbers.RemoveAt(numbers.Count - 1);
            }
            return calculateS(numbers);
        }

        private int calculateS(List<int> numbers)
        {
            int numerator = 0;
            int denominator = 1;
            for (int i = numbers.Count - 1; i >= 0; i--)
            {
                int tempDenominator = denominator;
                denominator = numerator + numbers[i] * denominator;
                numerator = tempDenominator;

            }
            return numerator;
        }

        internal (int, int, int) findNext(int numerator, int denominator)
        {
            if (numerator == 0)
            {
                return (0, 0, 0);
            }
            else if (numerator < 0 || denominator <= 0)
            {
                throw new ArgumentException();
            }
            else if (denominator == 1)
            {
                return (numerator, 0, 0);
            }
            else
            {
                int gcd = new GreatestCommonDenominator().findGCD(denominator, numerator % denominator);
                return (numerator / denominator, denominator / gcd, (numerator % denominator) / gcd);
            }
        }
    }
}