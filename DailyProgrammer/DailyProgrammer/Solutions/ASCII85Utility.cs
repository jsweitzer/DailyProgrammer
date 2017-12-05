using System;
using System.Collections.Generic;
using System.Text;
using DailyProgrammer.Extensions;

namespace DailyProgrammer.Solutions
{
    //https://www.reddit.com/r/dailyprogrammer/comments/7gdsy4/20171129_challenge_342_intermediate_ascii85/
    public static class ASCII85Utility
    {
        private static int numPad = 0;

        public static void Go()
        {
            var input = "";
            while(input != "quit")
            {
                input = Console.ReadLine();
                switch (input)
                {
                    case "encode":
                        Console.WriteLine("input text to be encoded");
                        Encode(Console.ReadLine());
                        break;
                    case "decode":
                        Console.WriteLine("input text to be decoded");
                        Decode(Console.ReadLine());
                        break;
                }
            }
        }

        public static void Decode(string input)
        {
            while (input.Length % 5 != 0)
            {
                input += "u";
                numPad++;
            }
            var charArrays = input.ToCharArrays(5);
            var result = "";

            foreach(var c in charArrays)
            {
                var current = DecodeFiveByteCharArray(c);
                result += current;
            }
            
            Console.WriteLine(result.Substring(0, result.Length-numPad));

            numPad = 0;
        }

        public static void Encode(string input)
        {
            var sic = input;
            while(sic.Length % 4 != 0)
            {
                sic = sic + " ";
                numPad++;
            }
            var charArrays = input.ToCharArrays(4);
            var result = "";

            foreach(var a in charArrays)
            {
                result += EncodeFourByteCharArray(a);
            }

            result = result.Substring(0, result.Length - numPad);
            numPad = 0;

            Console.WriteLine(result);
        }

        private static List<char[]> GetCharArraysFromEncodedString(string input)
        {
            var bigCharArray = input.ToCharArray();
            var result = new List<char[]>();

            for(var i = 0; i < input.Length/5; i++)
            {
                var current = new char[5] { bigCharArray[i * 5 + 0], bigCharArray[i * 5 + 1], bigCharArray[i * 5 + 2], bigCharArray[i * 5 + 3], bigCharArray[i * 5 + 4] };
                result.Add(current);
            }

            return result;
        }
        
        private static string EncodeFourByteCharArray(char[] input)
        {
            var result = "";
            var concat = "";

            for (var i = 0; i < input.Length; i++)
            {
                concat += Convert.ToString(Convert.ToByte(input[i]), 2).PadLeft(8, '0');
            }

            var value = Convert.ToInt32(concat, 2);

            var v1 = (value % 85) + 33;
            var v2 = ((value / 85) % 85) + 33;
            var v3 = (((value / 85) / 85) % 85) + 33;
            var v4 = ((((value / 85) / 85) / 85) % 85) + 33;
            var v5 = (((((value / 85) / 85) / 85) / 85) % 85) + 33;

            result += Convert.ToChar(v5);
            result += Convert.ToChar(v4);
            result += Convert.ToChar(v3);
            result += Convert.ToChar(v2);
            result += Convert.ToChar(v1);

            return result;
        }

        private static string DecodeFiveByteCharArray(char[] input)
        {
            var result = "";
            var value = 0;
            var radixEF = new List<int>();
            var recomposeMid = new List<int>();
            var charrs = new List<byte>();

            for(var i = 0; i < 5; i++)
            {
                var test = (int)input[i];
                radixEF.Add(test-33);
                value += ((test - 33) * (int)Math.Pow(85, i));
            }

            radixEF.Reverse();

            for(var i = 0; i < radixEF.Count; i++)
            {
                var recomposeMidVal = (radixEF[i]*((int)Math.Pow(85, i)));
                recomposeMid.Add(recomposeMidVal);
            }

            var final = 0;
            recomposeMid.ForEach(v => final += v);

            var tcharr = Convert.ToString(final, 2);

            while (tcharr.Length % 8 != 0)
            {
                tcharr = "0" + tcharr;
            }

            int numOfBytes = tcharr.Length / 8;
            byte[] bytes = new byte[numOfBytes];

            for (int i = 0; i < numOfBytes; ++i)
            {
                bytes[i] = Convert.ToByte(tcharr.Substring(8 * i, 8), 2);
            }

            result = Encoding.ASCII.GetString(bytes);
            return result;
        }
    }
}
