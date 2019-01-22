# shor-by-andrew

This is my implementation of Shor's algorithm in Q#, I made use of the Microsoft Canon library but implemented the Quantum Fourier Transform myself.

## Getting Started

### Prerequisites

This project requires Microsoft's Quantum Development Kit, instructions for installation can be found on the [Microsoft Q# installation guide](https://docs.microsoft.com/en-us/quantum/install-guide/?view=qsharp-preview).

### Running

Build the project

```
dotnet build
```

Run the code

```
dotnet run
```

Example output:
```
1. Selected 11 as our random value a < 15
2. GCD(11, 15) = 1
3. As GCD(11, 15) = 1, we need to continue
4. Using Quantum Period Finding to find the period of 11 ^ x mod 15
     - Found estimate for r as 2, this is either the period or a factor of the period
     - The period of 11 ^ x mod 15 is 2
5. As 11 mod 2 != 1 we can continue
6. As 11 ^ (2 / 2) mod 15 != -1 we can continue
7. The factors of 15 are therefore GCD(11 ^ (2 / 2) + 1, 15) and GCD(11 ^ (2 / 2) - 1, 15)

The factors of 15 are 5 and 3
```

The number to be factorised can be modified in [Shor.cs](https://github.com/AndrewBryer/shor-by-andrew/blob/master/src/Shor.cs), suggested choices are 15 and 21.

## Running the tests

Build the project

```
dotnet build
```

Run the tests

```
dotnet test
```


## Authors

* **Andrew Bryer** - *Initial work* - [AndrewBryer](https://github.com/AndrewBryer)

## Acknowledgments

* Thanks to James Birnie - [JimBobBirnie](https://github.com/JimBobBirnie) for introducing me to Quantum programming, and access to his implementation for reference to the stages of Shor's algorithm.
