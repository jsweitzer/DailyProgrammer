using System;
using DailyProgrammer.Solutions;

namespace DailyProgrammer
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = "";
            while (input != "quit")
            {
                Console.WriteLine("Ready...");
                input = Console.ReadLine();
                switch (input)
                {
                    case "help": Help.EnumerateCmds(); break;
                    case "scales": MajorScales.Go(); break;
                    case "ASCII": ASCII85Utility.Go(); break;
                    case "Polynomials": PolynomialDivision.Go(); break;
                }
            }
        }
    }
}
