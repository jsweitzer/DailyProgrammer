using System;
using System.Collections.Generic;
using System.Text;
using DailyProgrammer.Enums;

namespace DailyProgrammer.UI
{
    public static class Parser
    {
        
        public static Cmd GetCmd(string input)
        {
            object cmd;
            return Enum.TryParse(typeof(Cmd), input, true, out cmd) ? (Cmd)cmd : Cmd.unrecognized;
        }
    }
}
