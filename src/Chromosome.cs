﻿using System;
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
        public int Length { get { return _data.Length; } }
        public bool this[int Index]
        {
            get
            {
                if (Index < 0 || Index >= _data.Length) throw new IndexOutOfRangeException("Invalid Index");
                return _data[Index];
            }
        }

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
            // Initialise the bit-string with the default value of 0.
            _data = new BitArray(Data.Length, false);

            // Loop through the string, swap the 0s to 1s wherever necessary.
            for (int i = 0; i < Data.Length; i++)
            {
                Char bit = Data[i];

                // Check for invalid characters
                if ( (!bit.Equals('0')) && (!bit.Equals('1'))) throw new ArgumentException("Only '1' and '0' allowed for bit-string creation", Data);

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

        /// <summary>
        /// Performs single-point crossover and mutation operations on parents, giving 2 child chromosomes
        /// </summary>
        /// <param name="ParentA">The first Parent chromosome to use</param>
        /// <param name="ParentB">The second Parent chromosome to use</param>
        /// <param name="CrossoverRate">The percentage chance of crossover</param>
        /// <param name="MutationRate">The percentage chance of mutation</param>
        /// <returns>Child Chromosomes</returns>
        public static IEnumerable<Chromosome> Reproduce(Chromosome ParentA, Chromosome ParentB, float CrossoverRate, float MutationRate)
        {
            // Parameter Validation
            if (MutationRate > 1 || MutationRate < 0) throw new ArgumentOutOfRangeException("MutationRate", "Invalid mutation rate, must be a value between 0 and 1");
            if (CrossoverRate > 1 || CrossoverRate < 0) throw new ArgumentOutOfRangeException("CrossoverRate", "Invalid crossover rate, must be a value between 0 and 1");

            List<Chromosome> children;

            // Perform a biased coin flip.
            if (CrossoverRate > r.NextDouble())
            {
                // Crossover
                children = crossover(ParentA, ParentB);
            }
            else
            {
                // No crossover
                children = new List<Chromosome>();
                children.Add(new Chromosome(ParentA.Data));
                children.Add(new Chromosome(ParentB.Data));
            }

            // Mutate each gene
            foreach (Chromosome c in children)
            {
                c.mutate(MutationRate);
            }

            return (IEnumerable<Chromosome>)children;
        }

        /// <summary>
        /// Combines two chromosomes
        /// </summary>
        /// <param name="Genes">Parent Genes</param>
        /// <returns>Child Genes</returns>
        private static List<Chromosome> crossover(Chromosome GeneA, Chromosome GeneB)
        {
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
            List<Chromosome> result = new List<Chromosome>();
            result.Add(new Chromosome(ChildA));
            result.Add(new Chromosome(ChildB));

            return result;
        }

        /// <summary>
        /// Flips bits in the bit-string
        /// </summary>
        /// <param name="MutationRate">The probability a random bit will become flipped</param>
        private void mutate(float MutationRate)
        {
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

        /// <summary>
        /// Produces a boolean array representing the bit-string data
        /// </summary>
        /// <returns>An array of booleans</returns>
        public bool[] ToBoolArray()
        {
            bool[] returnArray = new bool[Length];
            for (int i = 0; i < Length; i++)
            {
                returnArray[i] = this[i];
            }
            return returnArray;
        }
    }
}
