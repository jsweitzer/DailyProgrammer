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
            var read = new Cmd();
            var write = new Printer();

            while (read != Cmd.Quit)
            {
                write.Line(Out.Ready);

                switch (Parser.GetCmd(Console.ReadLine()))
                {
                    case Cmd.Help: Help.EnumerateCmds(); break;
                    case Cmd.Scales: MajorScales.Go(); break;
                    case Cmd.Ascii: ASCII85Utility.Go(); break;
                    case Cmd.Polynomials: Polynomials.Go(); break;
                    case Cmd.Webclient: WebClient.Go(); break;
                    case Cmd.Boxes: StackingBoxes.Go(); break;
                    case Cmd.Balance: Balance.Go(); break;
                }
            }
        }
    }
}
