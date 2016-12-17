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
            //Test for #1
            //input = "ADVENT"; //ADVENT 6
            //input = "A(1x5)BC"; //ABBBBBC 7
            //input = "(3x3)XYZ"; //XYZXYZXYZ 9
            //input = "A(2x2)BCD(2x2)EFG"; //ABCBCDEFEFG 11
            //input = "(6x1)(1x3)A"; //(1x3)A 6
            //input = "X(8x2)(3x3)ABCY"; //X(3x3)ABC(3x3)ABCY 18

            for (int i=0; i<input.Length;)
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
                    var repeatedPart = Enumerable.Range(0, repeatXTimes).Aggregate("", (was, add) => { return was + partToRepeat; });
                    input = input.Remove(i, marker.Length + 2 + charsToTake).Insert(i, repeatedPart);

                    i += repeatedPart.Length;
                }
                else
                {
                    i++;
                }
            }

            var count = input.Count(ci => ci != ' ');

            Console.WriteLine($"Decompressed length: {count}");
            Console.ReadKey();
        }
    }
}
