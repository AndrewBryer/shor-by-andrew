namespace Shor
{
    open Microsoft.Quantum.Canon;
    open Microsoft.Quantum.Primitive;
    open Microsoft.Quantum.Extensions.Math;
    open Microsoft.Quantum.Extensions.Convert;

    operation FindNumerator(a : Int, modulus : Int) : Int
    {
        body (...)
        {
            let bits = BitSize(modulus);
            mutable result = 0;

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

                    QuantumFourierTranform(inputRegister);

                    set result = MeasureInteger(LittleEndian(inputRegister));
                    ResetAll(outputRegister);
                }
                ResetAll(inputRegister);
            }
            return result;
        }
    }

    operation QuantumFourierTranform(inputRegister : Qubit[]) : Unit
    {
        body (...)
        {
            SwapReverseRegister(inputRegister);

            for (i in 0..Length(inputRegister) - 1)
            {    
                H(inputRegister[i]);                          
                for (j in i + 1..Length(inputRegister) - 1) {
                    Controlled R1Frac([inputRegister[j]], (1, j - i, inputRegister[i]));
                }
            }
        }
    }
}
