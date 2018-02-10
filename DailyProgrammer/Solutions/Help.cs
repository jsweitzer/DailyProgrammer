using System;
using System.Collections.Generic;
using DailyProgrammer.Enums;

namespace DailyProgrammer.Solutions
{
    public static class Help
    {
        public static Dictionary<Cmd, string> cmds = new Dictionary<Cmd, string>()
        {
            { Cmd.Help, "Returns a list of available commands" },
            { Cmd.Scales, "Returns the name of a major scale, the solfège name of a note, and the corresponding note in that scale." },
            { Cmd.Webclient, "Lower level web client" },
            { Cmd.Quit, "Closes the console" },
            { Cmd.Boxes, "Trys to sort a weighted collection into evenly weighted sub collections" },
            { Cmd.Ascii, "ASCII85 encoder/decoder" },
            { Cmd.Polynomials, "Polynomial long division" }
        };
        public static void EnumerateCmds()
        {
            foreach(var c in cmds)
            {
                Console.WriteLine(c.Key + ": " + c.Value);
            }
        }
    }
}
