using System;

using Microsoft.Quantum.Simulation.Core;
using Microsoft.Quantum.Simulation.Simulators;

namespace Shor
{
    class Shor
    {
        static void Main(string[] args)
        {
            using (var qsim = new QuantumSimulator())
            {
                (int, int) result = new Factoriser().factorise(15);
                Console.WriteLine(result);
            }
        }
    }
}