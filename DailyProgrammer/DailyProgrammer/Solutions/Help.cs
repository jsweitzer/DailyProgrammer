using System;
using System.Collections.Generic;

namespace DailyProgrammer.Solutions
{
    public static class Help
    {
        public static Dictionary<string, string> cmds = new Dictionary<string, string>()
        {
            { "help", "Returns a list of available commands" },
            { "scales", "A function that takes the name of a major scale and the solfège name of a note, and returns the corresponding note in that scale." }
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
