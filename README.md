What is ChromosomeLibrary?
--------------------------

ChromosomeLibrary provides bit-string functionality for use in genetic algorithm projects

Usage
-----

To create initial random chromosomes use the following code:
```c#
int bitStringSize = 8;
Chromosome init = GenerateRandomChromosome(bitStringSize);
```

Genetic data from two parents can be combined to produce child chromosomes:
```c#
float mutationRate = 0.001f;
float crossoverRate = 0.3f;

Chromosome[] children = Reproduce(ChromosomeA, ChromosomeB, mutationRate, crossoverRate);
```