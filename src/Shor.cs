using System;

namespace Shor
{
    class Shor
    {
        public static void Main(string[] args)
        {
            int numberToFactorise = 15;
            (int, int) result = new Factoriser().factorise(numberToFactorise);
            Console.WriteLine($"The factors of {numberToFactorise} are {result.Item1} and {result.Item2}");
        }
    }
}