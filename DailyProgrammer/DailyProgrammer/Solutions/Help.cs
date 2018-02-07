using System;
using System.Collections.Generic;
using DailyProgrammer.Enums;

namespace DailyProgrammer.Solutions
{
    public static class Help
    {
        public static Dictionary<Cmd, string> cmds = new Dictionary<Cmd, string>()
        {
            { Cmd.help, "Returns a list of available commands" },
            { Cmd.scales, "Returns the name of a major scale, the solfège name of a note, and the corresponding note in that scale." },
            { Cmd.webclient, "Lower level web client" },
            { Cmd.quit, "Closes the console" },
            { Cmd.boxes, "Trys to sort numbers evenly into containers. ex input: 235 2 output: 23 5" },
            { Cmd.ascii, "ASCII85 encoder/decoder" },
            { Cmd.polynomials, "Polynomial long division" }
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
