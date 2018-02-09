using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace DailyProgrammer.Solutions
{
    //https://www.reddit.com/r/dailyprogrammer/comments/7fvy7z/20171127_challenge_342_easy_polynomial_division/
    public static class Polynomials
    {
        public static void Go()
        {
            Console.WriteLine("Entering PolynomialDivision");

            var numerator = "4x3 + 2x2 - 6x + 3";
            var denominator = "x - 3";

            Divide(numerator, denominator);

            numerator = "2x4 - 9x3 + 21x2 - 26x + 12";
            denominator = "2x - 3";

            Divide(numerator, denominator);

            numerator = "10x4 - 7x2 - 1";
            denominator = "x2 - x + 3";

            Divide(numerator, denominator);
        }
        //Long division method
        public static void Divide(string numerator, string denominator)
        {
            Console.WriteLine("Dividing " + numerator + " by " + denominator);

            //minuend is the current expression being acted on by the denominator and subtrahend
            var minuend = BuildNomList(numerator);
            //dNoms is the denominator expression
            var dNoms = BuildNomList(denominator);
            //subtrahend is the expression to be subtracted from minuend
            var subtrahend = new List<Nom>();
            //result holds the quotient less the remainder
            var result = new List<Nom>();
            //counter tracks the index of the result expression to be multiplied across dNoms
            var counter = 0;
            //isComplete is set to true when the calculation is complete
            var isComplete = false;
            //The primary calculation loop
            while (!isComplete)
            {
                //Add the first expression in minuend over the first expression in dNoms to result
                result.Add(DivideNoms(minuend[0], dNoms[0]));
                //Multiply the result of the previous calculation across dNoms
                subtrahend = MultiplyAcrossNoms(dNoms, result[counter]);
                //Increment the counter
                counter++;
                //Set minuend = minued - subtrahend
                minuend = SubtractAcrossNoms(minuend, subtrahend);
                //Clear expressions with a constant of 0
                minuend = ClearZeroNoms(minuend);
                //Check for completion
                isComplete = IsComplete(dNoms, minuend);
            }
            //Write results to console
            PrintResults(result, minuend);
        }
        //Multiply two Noms
        public static Nom MultiplyNoms(Nom input1, Nom input2)
        {
            var result = new Nom();
            result.Constant = input1.Constant * input2.Constant;
            result.HasX = input1.HasX || input2.HasX;
            result.Power = input1.Power + input2.Power;
            return result;
        }
        //Divide two Noms
        public static Nom DivideNoms(Nom numerator, Nom denominator)
        {
            var result = new Nom();
            result.HasX = numerator.HasX;
            if (numerator.HasX && denominator.HasX)
            {
                result.Power = numerator.Power - denominator.Power;
            }
            result.Constant = numerator.Constant / denominator.Constant;
            return result;
        }
        //Subtract Nom
        public static Nom SubtractNom(Nom top, Nom bottom)
        {
            if (top.Power != bottom.Power) throw new InvalidOperationException("Cannot get difference of different powers");

            var result = new Nom();
            result.Constant = top.Constant - bottom.Constant;
            result.HasX = top.HasX && bottom.HasX;
            result.Power = top.Power;
            return result;
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
                result.Power = 0;
                result.HasX = false;
            }

            return result;
        }
        //Takes a string of Noms and returns a List<Nom>
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
                    currentNom.Constant = tempSign ? currentNom.Constant : currentNom.Constant * -1;
                    nomArray.Add(currentNom);
                }
                //If we have a sign, set tempSign for the next Nom
                else if (currentVal == "+" || currentVal == "-")
                {
                    tempSign = currentVal.Equals("+");
                }
            }
            return nomArray;
        }
        //Multiple all input Noms by multipler
        public static List<Nom> MultiplyAcrossNoms(List<Nom> input, Nom multiplier)
        {
            var result = new List<Nom>();
            foreach(var n in input)
            {
                result.Add(MultiplyNoms(n, multiplier));
            }
            return result;
        }
        //Returns the result of top - bottom
        public static List<Nom> SubtractAcrossNoms(List<Nom> top, List<Nom> bottom)
        {
            var result = new List<Nom>();
            for(int i = 0; i < top.Count; i ++)
            {
                if(bottom.FindIndex(n => n.Power == top[i].Power) != -1)
                {
                    result.Add(SubtractNom(top[i], bottom[bottom.FindIndex(n => n.Power == top[i].Power)]));
                }
                else
                {
                    result.Add(top[i]);
                }
            }
            foreach(var n in bottom)
            {
                if(result.FindIndex(r => r.Power == n.Power) == -1)
                {
                    n.Constant = n.Constant * -1;
                    result.Add(n);
                }
            }
            result = result.OrderByDescending(r => r.Power).ToList();
            return result;
        }
        //Check so see if calculations can be continued
        public static bool IsComplete(List<Nom> denoms, List<Nom> curnoms)
        {
            return curnoms.Count == 0 || (denoms[0].Power > curnoms[0].Power);
        }
        //Clear out Noms that have a constant of 0
        public static List<Nom> ClearZeroNoms(List<Nom> input)
        {
            var result = new List<Nom>();
            foreach(var n in input)
            {
                if(n.Constant != 0)
                {
                    result.Add(n);
                }
            }
            return result;
        }
        //Print results
        public static void PrintResults(List<Nom> quotient, List<Nom> remainder)
        {
            string result = "Result: ";
            for(int i = 0; i < quotient.Count; i++)
            {
                result += quotient[i].Constant;
                result = quotient[i].HasX ? result + "x" + quotient[i].Power : result;
                result = (i == quotient.Count - 1) ? result + " ": result + " + ";
            }
            result += "Remainder: ";
            for(int i = 0; i < remainder.Count; i++)
            {
                result += remainder[i].Constant;
                result = remainder[i].HasX ? result + "x" + remainder[i].Power : result;
                result = (i == remainder.Count - 1) ? result : result + " + ";
            }
            Console.WriteLine(result);
        }
    }
    //The Nom object. Nice.
    public class Nom
    {
        public int Constant { get; set; }
        public bool HasX { get; set; }
        public int Power { get; set; }
    }
}
