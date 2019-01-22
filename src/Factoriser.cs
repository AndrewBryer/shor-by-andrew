using System;
using Microsoft.Quantum.Simulation.Simulators;

namespace Shor
{
    public class Factoriser
    {
        // If r is odd pick a new a
        // If a ^ (r / 2) is -1 mod n then pick a new a
        private GreatestCommonDenominator gcd;
        private ContinuedFractions cf;
        public Factoriser()
        {
            gcd = new GreatestCommonDenominator();
            cf = new ContinuedFractions();
        }
        public (int, int) factorise(int numberToFactorise)
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
            int a = selectRandomALessThanNGreaterThanTwo(numberToFactorise);
            Console.WriteLine($"1. Selected {a} as our random value a < {numberToFactorise}");

            int gcdOfAAndN = gcd.findGCD(a, numberToFactorise);
            Console.WriteLine($"2. GCD({a}, {numberToFactorise}) = {gcdOfAAndN}");
            if (gcdOfAAndN != 1)
            {
                Console.WriteLine($"3. As GCD({a}, {numberToFactorise}) != 1, we are done");
                Console.WriteLine();
                return (gcdOfAAndN, numberToFactorise / gcdOfAAndN);
            }
            else
            {
                Console.WriteLine($"3. As GCD({a}, {numberToFactorise}) = 1, we need to continue");
                Console.WriteLine($"4. Using Quantum Period Finding to find the period of {a} ^ x mod {numberToFactorise}");
                int r = findPeriod(a, numberToFactorise);
                Console.WriteLine($"     - The period of {a} ^ x mod {numberToFactorise} is {r}");

                if (r % 2 == 1)
                {
                    Console.WriteLine($"5. Unfortunately, {a} mod 2 = 1 so retrying for a new value of a");
                    Console.WriteLine();
                    return factoriseWithShors(numberToFactorise);
                }
                else if ((int)Math.Pow(a, r / 2) % numberToFactorise == numberToFactorise - 1)
                {
                    Console.WriteLine($"5. As {a} mod 2 != 1 we can continue");
                    Console.WriteLine($"6. Unfortunately, {a} ^ ({r} / 2) mod {numberToFactorise} = -1 so retrying for a new value of a");
                    Console.WriteLine();
                    return factoriseWithShors(numberToFactorise);
                }
                else
                {
                    Console.WriteLine($"5. As {a} mod 2 != 1 we can continue");
                    Console.WriteLine($"6. As {a} ^ ({r} / 2) mod {numberToFactorise} != -1 we can continue");
                    Console.WriteLine($"7. The factors of {numberToFactorise} are therefore GCD({a} ^ ({r} / 2) + 1, {numberToFactorise}) and GCD({a} ^ ({r} / 2) - 1, {numberToFactorise})");
                    Console.WriteLine();
                    int factor1 = gcd.findGCD((int)Math.Pow(a, r / 2) - 1, numberToFactorise);
                    int factor2 = gcd.findGCD((int)Math.Pow(a, r / 2) + 1, numberToFactorise);
                    return (factor1, factor2);
                }
            }
        }

        private int findPeriod(int a, int numberToFactorise, int r = 1)
        {
            int y = 0;
            while (y == 0)
            {
                using (var qsim = new QuantumSimulator())
                {
                    y = (int)FindNumerator.Run(qsim, a, numberToFactorise).Result;
                }
            }
            int numberOfBits = (int)Math.Ceiling(Math.Log(numberToFactorise, 2));
            int s = cf.findS(y, (int)Math.Pow(2, 2 * numberOfBits), numberToFactorise);
            r = r * s / gcd.findGCD(r, s);
            Console.WriteLine($"     - Found estimate for r as {r}, this is either the period or a factor of the period");

            if ((int)Math.Pow(a, r) % numberToFactorise != 1)
            {
                Console.WriteLine($"     - As {a} ^ {r} mod {numberToFactorise} != 1, we have only found a factor of the period, retrying period finding routine");
                return findPeriod(a, numberToFactorise, r);
            }
            else
            {
                return s;
            }
        }

        private int selectRandomALessThanNGreaterThanTwo(int numberToFactorise)
        {
            return new Random().Next(3, numberToFactorise);
        }
    }
}