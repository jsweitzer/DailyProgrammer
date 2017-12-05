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
    }
}
