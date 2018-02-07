using System;
using System.Collections.Generic;
using System.Text;

namespace DailyProgrammer.Extensions
{
    public static class StringExtensions
    {
        /// <summary>
        /// Returns a list of char arrays the length of arraySize. input is padded so input.Length % arraySize == 0
        /// </summary>
        public static List<char[]> ToCharArrays(this String input, int arraySize)
        {
            if (arraySize <= 0) throw new ArgumentOutOfRangeException("arraySize", "arraySize must be greater than 0");

            var result = new List<char[]>();

            while (input.Length % arraySize != 0)
            {
                input += " ";
            }

            var bigCharArray = input.ToCharArray();

            for (int i = 0; i < bigCharArray.Length / arraySize; i++)   
            {
                var currentString = "";
                for(int z = 0; z < arraySize; z++)
                {
                    currentString += bigCharArray[(i * arraySize) + z];
                }
                var currentChars = currentString.ToCharArray();
                result.Add(currentChars);
            }

            return result;
        }

        /// <summary>
        /// Get all permutations of a string. Don't use this with large strings or your computer will explode.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static List<char[]> Permutations(this String input) {
            Permutate(input);
            return permutations;
        }
        #region Permutation stuff
        private static int elementLevel = -1;
        private static int numberOfElements;
        private static int[] permutationValue = new int[0];
        private static List<char[]> permutations = new List<char[]>();

        private static char[] InputSet { get; set; }
        private static int PermutationCount { get; set; } = 0;

        public static void Permutate(string input)
        {
            InputSet = null;
            permutations = new List<char[]>();
            elementLevel = -1;
            numberOfElements = 0;
            permutationValue = new int[0];
            PermutationCount = 0;
            InputSet = MakeCharArray(input);
            CalcPermutation(0);
        }

        private static char[] MakeCharArray(string InputString)
        {
            char[] charString = InputString.ToCharArray();
            Array.Resize(ref permutationValue, charString.Length);
            numberOfElements = charString.Length;
            return charString;
        }

        private static void CalcPermutation(int k)
        {
            elementLevel++;
            permutationValue.SetValue(elementLevel, k);

            if (elementLevel == numberOfElements)
            {
                OutputPermutation(permutationValue);
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

        private static void OutputPermutation(int[] value)
        {
            var newPerm = new char[value.Length];
            for (int i = 0; i < value.Length; i++)
            {
                newPerm.SetValue(InputSet.GetValue(value[i] - 1), i);
            }
            permutations.Add(newPerm);
            PermutationCount++;
        }
        #endregion
    }
}
