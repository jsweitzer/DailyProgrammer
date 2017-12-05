using System;

namespace DailyProgrammer.Solutions
{
    //https://www.reddit.com/r/dailyprogrammer/comments/7hhyin/20171204_challenge_343_easy_major_scales/
    public static class MajorScales
    {
        private enum Solfege { Do = 0, Re = 2, Mi = 4, Fa = 5, So = 7, La = 9, Ti = 11 };
        private enum ChromaticScale { C = 0, Cs = 1, D = 2, Ds = 3, E = 4, F = 5, Fs = 6, G = 7, Gs = 8, A = 9, As = 10, B = 11 };

        public static void Go()
        {
            var input = "";
            var scaleName = "";
            var solfegeNote = "";
            Console.WriteLine("Entering scales function");
            Console.WriteLine("This function returns the solfege note corresponding to the major scale of the note provided");
            Console.WriteLine("Type quit to exit to the main menu");
            Help();
            while(input != "quit")
            {
                Console.WriteLine("input scale name");
                scaleName = Console.ReadLine();
                Console.WriteLine("input solfege note");
                solfegeNote = Console.ReadLine();
                PrintMajorScaleByNote(scaleName, solfegeNote);
            }
        }

        public static void Help()
        {
            Console.WriteLine("Valid scale names:");
            var scaleNames = Enum.GetNames(typeof(ChromaticScale));
            foreach(var n in scaleNames)
            {
                Console.WriteLine(n);
            }
            Console.WriteLine("Valid solfege notes:");
            var solfegeNotes = Enum.GetNames(typeof(Solfege));
            foreach(var s in solfegeNotes)
            {
                Console.WriteLine(s);
            }
        }

        public static void PrintMajorScaleByNote(string scaleName, string solfegeNote)
        {
            try
            {
                var deltaIndex = Enum.Parse<Solfege>(solfegeNote);
                var initialIndex = Enum.Parse<ChromaticScale>(scaleName);
                var choiceIndex = (int)deltaIndex + (int)initialIndex;

                while (choiceIndex > 11)
                {
                    choiceIndex = choiceIndex - 12;
                }

                Console.WriteLine((ChromaticScale)choiceIndex);
            }catch(Exception e)
            {
                Console.WriteLine("An error occurred processing the scale");
                Console.WriteLine(e.Message);
            }
        }
    }
}
