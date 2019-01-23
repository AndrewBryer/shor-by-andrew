using System;
using Microsoft.Quantum.Simulation.Simulators;

namespace Shor
{
    public class Factoriser
    {
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
                    Console.WriteLine($"5. As {r} mod 2 != 1 we can continue");
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
                    return (gcd.findGCD((int)Math.Pow(a, r / 2) - 1, numberToFactorise), gcd.findGCD((int)Math.Pow(a, r / 2) + 1, numberToFactorise));
                }
            }
        }

        private int selectRandomALessThanNGreaterThanTwo(int numberToFactorise)
        {
            return new Random().Next(3, numberToFactorise);
        }

        private int findPeriod(int a, int numberToFactorise, int r = 1)
        {
            int y = findNumeratorOfDyadicFraction(a, numberToFactorise);

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
                return r;
            }
        }

        private int findNumeratorOfDyadicFraction(int a, int numberToFactorise)
        {
            int y;
            using (var qsim = new QuantumSimulator())
            {
                y = (int)FindNumerator.Run(qsim, a, numberToFactorise).Result;
            }
            if (y == 0)
            {
                return findNumeratorOfDyadicFraction(a, numberToFactorise);
            }
            else
            {
                return y;
            }
        }
    }
}