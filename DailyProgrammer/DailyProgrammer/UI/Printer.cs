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
    }
}