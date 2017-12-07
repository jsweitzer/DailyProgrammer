using System;
using System.Collections.Generic;
using System.Text;

namespace DailyProgrammer.Solutions
{
    public static class PolynomialDivision
    {
        public static void Go()
        {
            Console.WriteLine("Entering PolynomialDivision");

            var numerator = "4x3 + 2x2 - 6x + 3";
            var denominator = "x - 3";

            Divide(numerator, denominator);
        }

        public static void Divide(string numerator, string denominator)
        {
            var nArray = numerator.Split(" ");
            var dArray = denominator.Split(" ");
            var dFirst = nArray[0];
            var nomArray = new List<Nom>();

            for(var i = 0; i < nomArray.Count; i++)
            {
                
            }

            foreach(var n in nArray)
            {

            }
        }
        public static Nom ParseNom(string input)
        {
            var result = new Nom();

            if (input.Contains("x"))
            {
                var power = input.Substring(input.IndexOf("x") + 1, input.Length - input.IndexOf("x") - 1);
                var constant = input.Substring(0, input.IndexOf("x"));
                
                result.Power = (power == string.Empty) ? 1 : int.Parse(power);
                result.Constant = (constant == string.Empty) ? 1 : int.Parse(constant);
                result.HasX = true;
            }
            else
            {
                result.Constant = int.Parse(input);
                result.HasX = false;
            }

            return result;
        }

        public static Nom DivideNoms(Nom numerator, Nom denominator)
        {
            if(numerator.HasX && denominator.HasX)
            {
                numerator.Power -= denominator.Power;
            }
            numerator.Constant = numerator.Constant / denominator.Constant;
            return numerator;
        }
    }

    public class Nom
    {
        public int Constant { get; set; }
        public bool HasX { get; set; }
        public int Power { get; set; }
        public bool Positive { get; set; }
    }
}
