using System;
using System.Collections.Generic;
using System.Text;
using DailyProgrammer.Extensions;

namespace DailyProgrammer.Solutions
{
    /// <summary>
    /// //https://www.reddit.com/r/dailyprogrammer/comments/7ubc70/20180130_challenge_349_intermediate_packing/
    /// Takes a string of numbers and attempts to split them into n collections with each collection have the same sum
    /// Algorithm Source: A. Bogomolny, Counting And Listing 
    /// All Permutations from Interactive Mathematics Miscellany and Puzzles
    /// http://www.cut-the-knot.org/do_you_know/AllPerm.shtml, Accessed 11 June 2009
    /// </summary>
    public static class StackingBoxes
    {
        private static int numBoxes = 0;
        private static int elementLevel = -1;
        private static int numberOfElements;
        private static int[] permutationValue = new int[0];
        private static char[] InputSet { get; set; }
        private static int PermutationCount { get; set; } = 0;
        private static bool foundSolution = false;

        public static void Go()
        {
            Console.WriteLine("Input a string of characters 0-9");
            var input = Console.ReadLine();
            Console.WriteLine("Input number of boxes");
            numBoxes = int.Parse(Console.ReadLine());

            InputSet = null;
            foundSolution = false;
            elementLevel = -1;
            numberOfElements = 0;
            permutationValue = new int[0];
            PermutationCount = 0;
            InputSet = MakeCharArray(input);
            CalcPermutation(0);
        }
        /// <summary>
        /// Attempts to split permutation evenly into numStacks number of boxes.
        /// This definitely misses possible solutions but does the job for all of the challenge input.
        /// </summary>
        /// <param name="permutation"></param>
        /// <param name="numStacks"></param>
        /// <returns></returns>
        private static List<char[]> Stack(char[] permutation, int numStacks) {
            var result = new List<char[]>();
            double rawAvg = permutation.Length / (double)numStacks;
            rawAvg = Math.Round(rawAvg, MidpointRounding.AwayFromZero);
            int avg = (int)rawAvg;

            for (int i = 0; i < numStacks; i++) {
                if(i < numStacks - 1)
                {
                    var avgStack = new char[avg];
                    for(var j = 0; j < avg; j++)
                    {
                        avgStack.SetValue(permutation[(avg * i) + j], j);
                    }
                    result.Add(avgStack);
                }
                else
                {
                    var final = new char[permutation.Length - avg*(numStacks-1)];
                    for(var j = 0; j < final.Length; j++)
                    {
                        final.SetValue(permutation[avg * (numStacks - 1) + j], j);
                    }
                    result.Add(final);
                }
            }
            return result;
        }
        /// <summary>
        /// Checks to see if a collection of char[] sum to the same value
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private static bool CheckSums(List<char[]> input)
        {
            int oneSum = 0;
            for (var i = 0; i < input.Count; i++)
            {
                int thisSum = 0;
                foreach (var c in input[i])
                {
                    thisSum += (int)Char.GetNumericValue(c);
                }
                if(i == 0)
                {
                    oneSum = thisSum;
                }
                else
                {
                    if(thisSum != oneSum)
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        /// <summary>
        /// Converts input string to char[] and sets up dependent properties
        /// </summary>
        /// <param name="InputString"></param>
        /// <returns></returns>
        private static char[] MakeCharArray(string InputString)
        {
            char[] charString = InputString.ToCharArray();
            Array.Resize(ref permutationValue, charString.Length);
            numberOfElements = charString.Length;
            return charString;
        }
        /// <summary>
        /// Recursively calculate permutations of InputString 
        /// </summary>
        /// <param name="k"></param>
        private static void CalcPermutation(int k)
        {
            if (!foundSolution)
            {
                elementLevel++;
                permutationValue.SetValue(elementLevel, k);

                if (elementLevel == numberOfElements)
                {
                    CheckPermutation(permutationValue);
                }
                else
                {
                    for (int i = 0; i < numberOfElements; i++)
                    {
                        if (permutationValue[i] == 0)
                        {
                            CalcPermutation(i);
                        }
                    }
                }
                elementLevel--;
                permutationValue.SetValue(0, k);
            }
        }
        /// <summary>
        /// Check if Stack() will return a solution for the current permutation
        /// </summary>
        /// <param name="value"></param>
        private static void CheckPermutation(int[] value)
        {
            var newPerm = new char[value.Length];
            for (int i = 0; i < value.Length; i++)
            {
                newPerm.SetValue(InputSet.GetValue(value[i] - 1), i);
            }
            var stacks = Stack(newPerm, numBoxes);
            if (CheckSums(stacks))
            {
                Console.WriteLine("Found a solution!");
                foundSolution = true;
                foreach (var c in stacks)
                {
                    foreach (var v in c)
                    {
                        Console.Write(v);
                    }
                    Console.WriteLine();
                };
            }
            PermutationCount++;
        }
    }
}
