using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace ChromosomeLibrary
{
    public class Chromosome
    {
        private static Random r = new Random((int)DateTime.Now.Ticks);

        #region Data Members

        // Chromosome's bit-string
        private BitArray _data;
        public BitArray Data { get { return _data; } }

        #endregion

        #region Constructors

        /// <summary>
        /// Constructs a Chromosome from a given bit-string.
        /// </summary>
        /// <param name="Data">Bit-string data</param>
        public Chromosome(BitArray Data)
        {
            _data = Data;
        }

        /// <summary>
        /// Constructs a Chromosome from a string containing bits.
        /// </summary>
        /// <param name="Data">Bit-string Data</param>
        public Chromosome(string Data)
        {
            // TODO validate string input

            // Initialise the bit-string with the default value of 0.
            _data = new BitArray(Data.Length, false);

            // Loop through the string, swap the 0s to 1s wherever necessary.
            for (int i = 0; i < Data.Length; i++)
            {
                Char bit = Data[i];
                if (bit == '1')
                {
                    _data[i] = true;
                }
            }
        }

        /// <summary>
        /// Creates a Chromosome using random data.
        /// </summary>
        /// <param name="bitStringSize">The length of the bit-string</param>
        /// <returns>Chromosome with random data</returns>
        public static Chromosome GenerateRandomChromosome(int bitStringSize)
        {
            BitArray bitString = generateRandomBitArray(bitStringSize);
            return new Chromosome(bitString);
        }

        #endregion

        #region Genetic Operations

        public static Chromosome[] Reproduce(Chromosome ParentA, Chromosome ParentB, float MutationRate, float CrossoverRate)
        {
            // TODO change return type to IEnumerable

            Chromosome[] children = new Chromosome[2];

            // Perform a biased coin flip.
            if (CrossoverRate > r.NextDouble())
            {
                // Crossover
                children = Crossover(ParentA, ParentB);
            }
            else
            {
                // No crossover
                children[0] = new Chromosome(ParentA.Data);
                children[1] = new Chromosome(ParentB.Data);
            }

            // Mutate each gene
            foreach (Chromosome c in children)
            {
                c.Mutate(MutationRate);
            }

            return children;
        }

        /// <summary>
        /// Combines two chromosomes
        /// </summary>
        /// <param name="Genes">Parent Genes</param>
        /// <returns>Child Genes</returns>
        private static Chromosome[] Crossover(Chromosome GeneA, Chromosome GeneB)
        {
            // TODO change return type to IEnumerable
            // TODO change method to per-instance instead of static

            // Generate a crossover point between 0 and the chromosome length
            int chromosomeSize = GeneA._data.Length;
            int crossoverPoint = r.Next(0, chromosomeSize);

            BitArray ChildA = new BitArray(chromosomeSize);
            BitArray ChildB = new BitArray(chromosomeSize);

            for (int i = 0; i < chromosomeSize; i++)
            {
                // If we're before the crossover point copy bits normally
                // If we're after the crossover point, swap the bits when copying
                if (i < crossoverPoint)
                {
                    ChildA[i] = GeneA.Data[i];
                    ChildB[i] = GeneB.Data[i];
                }
                else
                {
                    ChildA[i] = GeneB.Data[i];
                    ChildB[i] = GeneA.Data[i];
                }
            }

            // package the results together and return them
            Chromosome[] result = new Chromosome[2];
            result[0] = new Chromosome(ChildA);
            result[1] = new Chromosome(ChildB);

            return result;
        }

        /// <summary>
        /// Flips bits in the bit-string
        /// </summary>
        /// <param name="MutationRate">The probability a random bit will become flipped</param>
        private void Mutate(float MutationRate)
        {
            // TODO Validate MutationRate

            // For every bit.
            for (int i = 0; i < _data.Length; i++)
            {
                // Perform a biased coin filp.
                if (MutationRate > r.NextDouble())
                {
                    // Flip the bit.
                    _data[i] = !_data[i];
                }
            }
        }

        #endregion

        /// <summary>
        /// Creates a bit-array filled with random data.
        /// </summary>
        /// <param name="bitArraySize">The bit-array's length</param>
        /// <returns>Bit-array with random data</returns>
        private static BitArray generateRandomBitArray(int bitArraySize)
        {
            // Calculate number of bytes required to store bitArraySize amount of bits
            int numberOfBytes = (int)Math.Ceiling((double)(bitArraySize / 8));

            // Create random data to populate the BitArray
            Int32[] dummyData = new Int32[numberOfBytes];
            for (int i = 0; i < dummyData.Length; i++)
            {
                dummyData[i] = r.Next(Int32.MinValue, Int32.MaxValue);
            }

            // Create random data to populate the BitArray
            bool[] bits = new bool[bitArraySize];
            for(int i = 0; i < bitArraySize; i++)
            {
                // Set each bit to a random bool
                bits[i] = r.NextDouble() > 0.5 ? true : false;
            }

            // Convert bit[] to bitarray
            BitArray data = new BitArray(bits);
            
            return data;
        }

        /// <summary>
        /// Produces a string containing the bit-string data
        /// </summary>
        /// <returns>A string of bits</returns>
        public override string ToString()
        {
            StringBuilder result = new StringBuilder();

            // Build a string from the bits
            foreach (bool bit in Data)
            {
                result.Append(bit ? 1 : 0);
            }

            return result.ToString();
        }
    }
}
