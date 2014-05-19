What is ChromosomeLibrary?
--------------------------

ChromosomeLibrary provides bit-string functionality for use in genetic algorithm projects:
* Creation of Chromosomes (from random or given data)
* Reproduction (including single-point crossover and mutation operations)

Usage
-----

To create initial random chromosomes use the following code:
```c#
int bitStringSize = 8;
Chromosome init = GenerateRandomChromosome(bitStringSize);
```

Chromosomes can also be created from given data:
```c#
string data = "01101001";
Chromosome init = new Chromosome(data);
```

Genetic data from two parents can be combined to produce child chromosomes:
```c#
float mutationRate = 0.001f;
float crossoverRate = 0.3f;

Chromosome[] children = Reproduce(ChromosomeA, ChromosomeB, mutationRate, crossoverRate);
```