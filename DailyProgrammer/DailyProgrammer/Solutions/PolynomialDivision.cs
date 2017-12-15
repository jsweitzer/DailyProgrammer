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
        //Magic?
        public static void Divide(string numerator, string denominator)
        {
            var numNoms = BuildNomList(numerator);
            var dNoms = BuildNomList(denominator);
        }
        //Build a Nom based on input. DOES NOT SET THE SIGN
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
        //Divide two Noms
        public static Nom DivideNoms(Nom numerator, Nom denominator)
        {
            if(numerator.HasX && denominator.HasX)
            { 
                numerator.Power -= denominator.Power;
            }
            numerator.Constant = numerator.Constant / denominator.Constant;
            return numerator;
        }
        //Takes a string of Noms with powers and returns a List<Nom>
        public static List<Nom> BuildNomList(string input)
        {
            var nArray = input.Split(" ");
            var nomArray = new List<Nom>();
            //Tempsign keeps track of the previous sign in the array so we can assign in to the
            //Nom objects built in the loop below. Default to true since positive is implied for
            //The first expression if no sign is shown.
            var tempSign = true;
            //Pack all of the numerator expressions into Nom objects
            for (int i = 0; i < nArray.Length; i++)
            {
                //The current value at index i of nArray
                var currentVal = nArray[i];
                //The nom object we will be adding for this loop (if we're not on a + or - sign)
                var currentNom = new Nom();
                //If we don't have a sign, we have a Nom
                if (currentVal != "+" && currentVal != "-")
                {
                    currentNom = ParseNom(currentVal);
                    currentNom.IsPositive = tempSign;
                    nomArray.Add(currentNom);
                    //If we have a sign, set tempSign for the next Nom
                }
                else if (currentVal == "+" || currentVal == "-")
                {
                    tempSign = currentVal.Equals("+");
                }
            }
            return nomArray;
        }
    }
    //The Nom object. Nice.
    public class Nom
    {
        public int Constant { get; set; }
        public bool HasX { get; set; }
        public int Power { get; set; }
        public bool IsPositive { get; set; }
    }
}
