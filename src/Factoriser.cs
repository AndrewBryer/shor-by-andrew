using System;

namespace Shor
{
    public class Factoriser
    {
        // Find GCD of a and N
        // If not 1 then we are done
        // Do Quantum stuff until result is not 0
        // Find r from the dyadic numerator using continued fractions
        // If r is odd pick a new a
        // If a ^ (r / 2) is -1 mod n then pick a new a
        // Find GCD of (a^(r/2) and N) and of (a^(r/2) and N) these are the factors
        private GreatestCommonDenominator gcd;
        public Factoriser() {
            gcd = new GreatestCommonDenominator();
        }
        internal (int, int) factorise(int numberToFactorise)
        {
            if (numberToFactorise < 2)
            {
                throw new ArgumentException();
            }
            else if (numberToFactorise % 2 == 0)
            {
                return (2, numberToFactorise / 2);
            }
            else 
            {
                return factoriseWithShors(numberToFactorise);
            }
        }

        private (int, int) factoriseWithShors(int numberToFactorise)
        {
            int a = new Random().Next(3, numberToFactorise);
            int gcdOfAAndN = gcd.findGCD(a, numberToFactorise);
            if (gcdOfAAndN != 1) {
                return (gcdOfAAndN, numberToFactorise / gcdOfAAndN);
            }
            else {
                throw new NotImplementedException();
            }
        }
    }
}