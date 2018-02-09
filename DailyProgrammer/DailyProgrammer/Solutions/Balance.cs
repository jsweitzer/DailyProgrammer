using System;
using System.Collections;
using System.Collections.Generic;
using DailyProgrammer.Extensions;

namespace DailyProgrammer.Solutions
{
    #region driver
    public static class Balance
    {
        public static void Go()
        {
            var i = new BalanceInstance();
            i.Go();
        }
    }
    #endregion
    //https://www.reddit.com/r/dailyprogrammer/comments/7vx85p/20180207_challenge_350_intermediate_balancing_my/
    public class BalanceInstance
    {
        private int length;
        private int leftWeight;
        private int rightWeight;
        private List<int> list = new List<int>();

        public void Go()
        {
            Console.WriteLine("Input length");
            length = int.TryParse(Console.ReadLine(), out int x) ? x : 0;

            Console.WriteLine("Input int csv");
            list.AddRange(Console.ReadLine().Split(',').Map(ParseInts));
        }
        private int ParseInts(string s)
        {
            return int.TryParse(s, out int y) ? y : 0;
        }
    }
}
