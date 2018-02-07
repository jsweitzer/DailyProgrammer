using System;
using DailyProgrammer.Solutions;
using DailyProgrammer.Enums;
using DailyProgrammer.UI;

namespace DailyProgrammer
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = new Cmd();

            while (input != Cmd.quit)
            {
                Console.WriteLine("Ready...");

                switch (Parser.GetCmd(Console.ReadLine()))
                {
                    case Cmd.help: Help.EnumerateCmds(); break;
                    case Cmd.scales: MajorScales.Go(); break;
                    case Cmd.ascii: ASCII85Utility.Go(); break;
                    case Cmd.polynomials: PolynomialDivision.Go(); break;
                    case Cmd.webclient: WebClient.Go(); break;
                    case Cmd.boxes: StackingBoxes.Go(); break;
                }
            }
        }
    }
}
