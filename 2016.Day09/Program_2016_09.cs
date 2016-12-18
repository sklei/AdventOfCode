using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2016.Day09
{
    class Program_2016_09
    {
        static void Main(string[] args)
        {
            string input = File.ReadAllText("input.txt");
            //Tests for #1
            //input = "ADVENT"; //ADVENT 6
            //input = "A(1x5)BC"; //ABBBBBC 7
            //input = "(3x3)XYZ"; //XYZXYZXYZ 9
            //input = "A(2x2)BCD(2x2)EFG"; //ABCBCDEFEFG 11
            //input = "(6x1)(1x3)A"; //(1x3)A 6
            //input = "X(8x2)(3x3)ABCY"; //X(3x3)ABC(3x3)ABCY 18

            //Tests for #2
            //input = "(3x3)XYZ"; //XYZXYZXYZ
            //input = "X(8x2)(3x3)ABCY"; //XABCABCABCABCABCABCY
            //input = "(27x12)(20x12)(13x14)(7x10)(1x12)A"; //A*241920
            //input = "(25x3)(3x3)ABC(2x3)XY(5x2)PQRSTX(18x9)(3x2)TWO(5x7)SEVEN"; //445

            Console.WriteLine($"Decompressed length #1: {Process(input, false)}"); //98135
            Console.WriteLine($"Decompressed length #2: {Process(input, true)}"); //10964557606 
            Console.ReadKey();
        }

        private static long Process(string input, bool processMarkersInDecompressedData)
        {
            long result = 0;

            for (int i = 0; i < input.Length;)
            {
                char c = input[i];

                if (c == '(')
                {
                    var marker = input
                        .Substring(i + 1)
                        .TakeWhile((c2, i2) => { return c2 != ')'; })
                        .Aggregate("", (was, add) => { return was + add; });
                    var markerInstructions = marker.Split('x');
                    int charsToTake = Int32.Parse(markerInstructions[0]);
                    int repeatXTimes = Int32.Parse(markerInstructions[1]);

                    var partToRepeat = input.Substring(i + marker.Length + 2, charsToTake);

                    if (processMarkersInDecompressedData && partToRepeat[0] == '(')
                    {
                        result += Process(partToRepeat, processMarkersInDecompressedData) * repeatXTimes;
                    }
                    else
                    {
                        var validChars = partToRepeat.Count(ci => ci != ' ');
                        result += validChars * repeatXTimes;
                    }

                    i += marker.Length + 2 + charsToTake;
                }
                else
                {
                    i++;
                    result++;
                }
            }

            return result;
        }
    }
}
