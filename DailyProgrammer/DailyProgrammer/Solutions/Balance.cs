using System;
using System.Linq;
using System.Collections.Generic;
using DailyProgrammer.UI;
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
        private int totalWeight;
        private int leftWeight = 0;
        private int rightWeight;
        private Printer write = new Printer();
        private List<int> list = new List<int>();
        private List<int> solutions = new List<int>();
        private const string list1 = "0 -3 5 -4 -2 3 1 0";
        private const string list2 = "3 -2 2 0 3 4 -6 3 5 -4 8";
        private const string list3 = "9 0 -5 -4 1 4 -4 -9 0 -7 -1";
        private const string list4 = "9 -7 6 -8 3 -9 -5 3 -6 -8 5";

        public void Go()
        {
            list.AddRange(list1.Split(' ').Map(ParseInts));
            Solve();
            list.Clear();
            solutions.Clear();
            list.AddRange(list2.Split(' ').Map(ParseInts));
            Solve();
            list.Clear();
            solutions.Clear();
            list.AddRange(list3.Split(' ').Map(ParseInts));
            Solve();
            list.Clear();
            solutions.Clear();
            list.AddRange(list4.Split(' ').Map(ParseInts));
            Solve();
        }
        private void Solve()
        {
            totalWeight = list.Sum(x => x);
            rightWeight = totalWeight - list.ElementAt(0);
            leftWeight = 0;
            for(var i = 0; i < list.Count(); i++)
            {
                if(rightWeight == leftWeight)
                {
                    solutions.Add(i);
                }
                rightWeight = i == list.Count() - 1 ? rightWeight : rightWeight - list.ElementAt(i + 1);
                leftWeight = leftWeight + list.ElementAt(i);
            }
            foreach(var i in solutions)
            {
                write.Inline(i);
            }
            write.Line(" ");
        }
        private int ParseInts(string s)
        {
            return int.TryParse(s, out int y) ? y : 0;
        }
    }
}
