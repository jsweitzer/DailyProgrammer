using System;
using System.Collections.Generic;
using System.Text;
using DailyProgrammer.Extensions;

namespace DailyProgrammer.Solutions
{
    //https://www.reddit.com/r/dailyprogrammer/comments/7ubc70/20180130_challenge_349_intermediate_packing/
    public static class StackingBoxes
    {
        public static void Go()
        {
            
            Console.WriteLine("Input a string of characters 0-9");
            var sinput = Console.ReadLine();
            var permutations = sinput.Permutations();
            for (var i = 0; i < permutations.Count; i++) {
                for (var j = 0; j < permutations[i].Length; j++) {
                    Console.Write(permutations[i][j]);
                }
                Console.WriteLine();
            }
        }
    }
}
