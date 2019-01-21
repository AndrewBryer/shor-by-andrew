namespace Shor
{
    open Microsoft.Quantum.Canon;
    open Microsoft.Quantum.Primitive;

    operation HelloQ () : Unit {
        Message("Hello quantum world!");
    }

    operation FindPeriod(a : Int, modulus : Int) : Int
    {
        // Apply H gate to all in input register
        // Make output register equal 1
        // Apply modular exponent to output register
        // Apply QFT to input register
        // Read result from input register
        // Find the period from this value
    }
}
