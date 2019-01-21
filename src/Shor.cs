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
                int result = (int)FindNumerator.Run(qsim, 2, 15).Result;
                Console.WriteLine(result);
            }
        }
    }
}