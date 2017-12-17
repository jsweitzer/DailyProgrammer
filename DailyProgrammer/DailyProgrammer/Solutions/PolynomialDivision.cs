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
            var workingNoms1 = new List<Nom>();
            var workingNoms2 = new List<Nom>();
            var result = new List<Nom>();
            //Step one - divide the first nom in numNoms by the first Nom in dNoms and add it to the result set
            result.Add(DivideNoms(numNoms[0], dNoms[0]));
            //Step two - multiply the result of step one across all dNoms
            workingNoms1 = MultiplyAcrossNoms(dNoms, result[0]);
            //Step three - subtract each result from step two from it's corresponding value in numNoms by index. Carry down any extra Noms in numNoms
            //Step four - assign the result of the previous step to workingNoms2
            workingNoms2 = SubtractAcrossNoms(numNoms, workingNoms1);
            workingNoms2 = ClearZeroNoms(workingNoms2);
            //Step five - divide the first nom in workingNoms by the firstNom in dNoms and add it to the result set
            result.Add(DivideNoms(workingNoms2[0], dNoms[0]));
            //Step six - multiply the result of the previous step across all dNoms
            workingNoms1 = MultiplyAcrossNoms(dNoms, result[1]);
            //Step seven - subtract each result of the previous step from it's corresponding value in workingNoms by index. Carry down any extra Noms in workingNoms
            if(!IsComplete(workingNoms2, workingNoms1))
                workingNoms2 = SubtractAcrossNoms(workingNoms2, workingNoms1);
            workingNoms2 = ClearZeroNoms(workingNoms2);
            result.Add(DivideNoms(workingNoms2[0], dNoms[0]));
            workingNoms1 = MultiplyAcrossNoms(dNoms, result[2]);
            workingNoms2 = SubtractAcrossNoms(workingNoms2, workingNoms1);
            workingNoms2 = ClearZeroNoms(workingNoms2);
            //Step eight - repeat steps four -> seven until the power of x in dNom is greater than the power of x at workingNoms[0]
            //The solution is the result * workingNoms/numNoms
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
                    //If we have a sign, set tempSign for the next Nom
                }
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
        public static List<Nom> SubtractAcrossNoms(List<Nom> dividend, List<Nom> subtractor)
        {
            var result = new List<Nom>();
            for(int i = 0; i < dividend.Count; i ++)
            {
                if(i < subtractor.Count)
                {
                    result.Add(SubtractNom(dividend[i], subtractor[i]));
                }
                else
                {
                    result.Add(dividend[i]);
                }
            }
            return result;
        }
        //Check so see if calculations can be continued
        public static bool IsComplete(List<Nom> denoms, List<Nom> curnoms)
        {
            return denoms[0].Power > curnoms[0].Power;
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
    }
    //The Nom object. Nice.
    public class Nom
    {
        public int Constant { get; set; }
        public bool HasX { get; set; }
        public int Power { get; set; }
    }
}
