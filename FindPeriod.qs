namespace Shor
{
    open Microsoft.Quantum.Canon;
    open Microsoft.Quantum.Primitive;
    open Microsoft.Quantum.Extensions.Math;
    open Microsoft.Quantum.Extensions.Convert;

    operation HelloQ () : Unit {
        Message("Hello quantum world!");
    }

    operation FindPeriod(a : Int, modulus : Int) : Int
    {
        let bits = BitSize(modulus);

        using (inputRegister = Qubit[2 * bits]) 
        {
            using (outputRegister = Qubit[bits])
            {
                ApplyToEach(H, inputRegister);

                X(outputRegister[0]);

                for (i in 0..2 * bits - 1)
                {
                    let multiplier = ExpMod(a, Round(PowD(2.0, ToDouble(i))), modulus);

                    Controlled ModularMultiplyByConstantLE([inputRegister[i]], (multiplier, modulus, LittleEndian(outputRegister)));
                }



                ResetAll(outputRegister);
            }
            ResetAll(inputRegister);
        }
        // Apply H gate to all in input register
        // Make output register equal 1
        // Apply modular exponent to output register
        // Apply QFT to input register
        // Read result from input register
        // Find the period from this value

        return 0;
    }
}
