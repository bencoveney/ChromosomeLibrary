What is ChromosomeLibrary?
--------------------------

ChromosomeLibrary provides bit-string functionality for use in genetic algorithm projects

Usage
-----

To create initial random chromosomes use the following code:
```
int bitStringSize = 8;
Chromosome init = GenerateRandomChromosome(bitStringSize);
```

Genetic data can be combined to produce child chromosomes:
```
float mutationRate = 0.001f;
float crossoverRate = 0.3f;

Chromosome[] children = Reproduce(ChromosomeA, ChromosomeB, mutationRate, crossoverRate);
```