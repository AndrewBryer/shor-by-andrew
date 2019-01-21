using System;

namespace Shor
{
    public class Factoriser
    {
        // Pick a < n
        // Find GCD of a and N
        // If not 1 then we are done
        // Do Quantum stuff until result is not 0
        // Find r from the dyadic numerator using continued fractions
        // If r is odd pick a new a
        // If a ^ (r / 2) is -1 mod n then pick a new a
        // Find GCD of (a^(r/2) and N) and of (a^(r/2) and N) these are the factors
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
                throw new NotImplementedException();
            }
        }
    }
}