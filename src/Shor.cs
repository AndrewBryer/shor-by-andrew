using System;

namespace Shor
{
    class Shor
    {
        static void Main(string[] args)
        {
            (int, int) result = new Factoriser().factorise(15);
            Console.WriteLine(result);
        }
    }
}