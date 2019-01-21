using System;

namespace Shor
{
    class Shor
    {
        static void Main(string[] args)
        {
            (int, int) result = new Factoriser().factorise(21);
            Console.WriteLine(result);
        }
    }
}