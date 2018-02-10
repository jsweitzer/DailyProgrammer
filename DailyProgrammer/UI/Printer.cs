using System;
using DailyProgrammer.Enums;
using DailyProgrammer.Resources;

namespace DailyProgrammer.UI
{
    public class Printer
    {
        public void Line(Out input)
        {
            Console.WriteLine(Res.ResourceManager.GetString(input.ToString()));
        }
        public void Line(object input)
        {
            Console.WriteLine(input);
        }
        public void Line(string input)
        {
            Console.WriteLine(input);
        }
        public void Inline(Out input)
        {
            Console.Write(Res.ResourceManager.GetString(input.ToString()));
        }
        public void Inline(object input)
        {
            Console.Write(input);
        }
        public void Inline(string input)
        {
            Console.Write(input);
        }
    }
}