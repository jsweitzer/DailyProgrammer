using System;
using System.Collections.Generic;
using System.Text;
using DailyProgrammer.Extensions;

namespace DailyProgrammer.Solutions
{
    //https://www.reddit.com/r/dailyprogrammer/comments/7gdsy4/20171129_challenge_342_intermediate_ascii85/
    public static class ASCII85Utility
    {
        /// <summary>
        /// Process the daily programmer challenge inputs
        /// </summary>
        public static void Go()
        {
            Encode("Attack at dawn");
            Decode("87cURD_*#TDfTZ)+T");
            Decode("06/^V@;0P'E,ol0Ea`g%AT@");
            Decode("7W3Ei+EM%2Eb-A%DIal2AThX&+F.O,EcW@3B5\\nF/hR");
            Encode("Mom, send dollars!");
            Decode("6#:?H$@-Q4EX`@b@<5ud@V'@oDJ'8tD[CQ-+T");
            Decode("<+oue+DGm>@3B#nB-;8(D/a<&+EMXFBl7Q9+BNK*+DGpFF!+q+B.b;uF=2,PBQ[s!+Ws`tBlbD2F!+t5@=!2O/hTM\"DC9NK@V'@iAThW-BlksM6uR/iA8cQ4A8cQB");
        }
        /// <summary>
        /// Decode input to ascii text
        /// </summary>
        /// <param name="input">Must be a valid ASCII85 encoded string</param>
        public static void Decode(string input)
        {
            //pad tracks how many characters are added to input
            var pad = 0;
            //Pad input so it can be divided evenly into char[5] arrays
            while (input.Length % 5 != 0)
            {
                input += "u";
                pad++;
            }
            //Split input into char[5] arrays
            var charArrays = input.ToCharArrays(5);
            var result = "";
            //Decode each encoded char[5] and add it to result
            foreach(var c in charArrays)
            {
                var current = DecodeFiveByteCharArray(c);
                result += current;
            }
            //Output result trimmed by pad
            Console.WriteLine(result.Substring(0, result.Length - pad));
        }
        /// <summary>
        /// Encode input to a ASCII85 string
        /// </summary>
        /// <param name="input"></param>
        public static void Encode(string input)
        {
            //pad tracks how many characters are added to input
            var pad = 0;
            //Pad input so it can be divided evenly into char[4] arrays
            while (input.Length % 4 != 0)
            {
                input = input + " ";
                pad++;
            }
            //Split input into char[5] arrays
            var charArrays = input.ToCharArrays(4);
            var result = "";
            //Encode each char[4] to an ASCII85 string and add it to result
            foreach(var a in charArrays)
            {
                result += EncodeFourByteCharArray(a);
            }
            //Output result trimmed by pad
            Console.WriteLine(result.Substring(0, result.Length - pad));
        }
        /// <summary>
        /// Encode a char[4] to an ASCII85 string
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private static string EncodeFourByteCharArray(char[] input)
        {
            //Concat will hold the concatenated binary string build from input
            var concat = "";
            //Extract input bytes and add to concat
            for (var i = 0; i < input.Length; i++)
            {
                concat += Convert.ToString(Convert.ToByte(input[i]), 2).PadLeft(8, '0');
            }
            //Convert to integer
            var value = Convert.ToInt32(concat, 2);
            //Decompose by 85 to get radix85 numbers
            //Ascii encode by adding 33
            var v1 = (value % 85) + 33;
            var v2 = ((value / 85) % 85) + 33;
            var v3 = (((value / 85) / 85) % 85) + 33;
            var v4 = ((((value / 85) / 85) / 85) % 85) + 33;
            var v5 = (((((value / 85) / 85) / 85) / 85) % 85) + 33;
            //Convert to ascii and add to result
            var result = "";
            result += Convert.ToChar(v5);
            result += Convert.ToChar(v4);
            result += Convert.ToChar(v3);
            result += Convert.ToChar(v2);
            result += Convert.ToChar(v1);

            return result;
        }
        /// <summary>
        /// Decode ASCII85 char[5] to ASCII string
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private static string DecodeFiveByteCharArray(char[] input)
        {
            var value = 0;
            var radix = new List<int>();
            var recomposeVals = new List<int>();
            //Extract ASCII values from input and subtract 33 to decode to radix85
            for(var i = 0; i < 5; i++)
            {
                var current = (int)input[i];
                radix.Add(current - 33);
                value += ((current - 33) * (int)Math.Pow(85, i));
            }

            radix.Reverse();
            //Get values to recompose radix85 into integer
            for(var i = 0; i < radix.Count; i++)
            {
                var recomposeVal = (radix[i]*((int)Math.Pow(85, i)));
                recomposeVals.Add(recomposeVal);
            }
            //Sum recompose values to get integer
            var final = 0;
            recomposeVals.ForEach(v => final += v);
            //Convert to binary
            var binaryString = Convert.ToString(final, 2);
            //Convert bit string to byte array
            var bytes = GetBytesFromBitString(binaryString);
            //Convert to ASCII
            return Encoding.ASCII.GetString(bytes);
        }
        /// <summary>
        /// Splits input into a byte[]
        /// </summary>
        /// <param name="input">Must be a binary string</param>
        /// <returns>byte[]</returns>
        private static byte[] GetBytesFromBitString(string input)
        {
            //Pad input to ensure we have an even number of bytes
            while (input.Length % 8 != 0)
            {
                input = "0" + input;
            }

            int numOfBytes = input.Length / 8;
            byte[] bytes = new byte[numOfBytes];

            for (int i = 0; i < numOfBytes; ++i)
            {
                bytes[i] = Convert.ToByte(input.Substring(8 * i, 8), 2);
            }

            return bytes;
        }
    }
}
