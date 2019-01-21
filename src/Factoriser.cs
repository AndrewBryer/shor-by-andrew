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

            Console.WriteLine($"Selected {a} as our random value a < N");

            int gcdOfAAndN = gcd.findGCD(a, numberToFactorise);
            if (gcdOfAAndN != 1)
            {
                Console.WriteLine("We got lucky! GCD(a, N) != 1");
                return (gcdOfAAndN, numberToFactorise / gcdOfAAndN);
            }
            else
            {
                int r = 1;
                while ((int)Math.Pow(a, r) % numberToFactorise != 1)
                {
                    Console.WriteLine("Using Quantum Period Finding to find the period of a ^ x mod N");
                    int y = 0;
                    while (y == 0)
                    {
                        using (var qsim = new QuantumSimulator())
                        {
                            y = (int)FindNumerator.Run(qsim, a, numberToFactorise).Result;
                        }
                    }
                    int s = cf.findS(y, (int)Math.Pow(2, 2 * Math.Ceiling(Math.Log(numberToFactorise, 2))), numberToFactorise);
                    r = r * s / gcd.findGCD(r, s);
                    Console.WriteLine($"Found s = {s}, this is either a factor of the period or the period itself");
                }

                if (r % 2 == 1 || (int) Math.Pow(a, r / 2) % numberToFactorise == numberToFactorise - 1)
                {
                    Console.WriteLine("Unfortunately, r was odd or a ^ (r/2) mod N = -1 so retrying for a new value of a");
                    return factoriseWithShors(numberToFactorise);
                }
                else
                {
                    return (gcd.findGCD((int)Math.Pow(a, r / 2) - 1, numberToFactorise), gcd.findGCD((int)Math.Pow(a, r / 2) + 1, numberToFactorise));
                }
            }
        }
    }
}