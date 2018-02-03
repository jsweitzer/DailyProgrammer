using System;
using System.Collections.Generic;
using System.Text;

namespace DailyProgrammer.Extensions
{
    public static class IntExtensions
    {
        public static int Factorial(this int input) {
            int result = 0;

            for(var i = 1; i < input - 1; i++)
            {
                result += i;
            }

            return result;
        }
    }
}
