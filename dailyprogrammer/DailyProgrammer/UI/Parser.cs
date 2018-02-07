using System;
using DailyProgrammer.Enums;

namespace DailyProgrammer.UI
{
    public static class Parser
    {
        
        public static Cmd GetCmd(string input)
        {
            return Enum.TryParse(typeof(Cmd), input, true, out object cmd) ? (Cmd)cmd : Cmd.unrecognized;
        }
    }
}
