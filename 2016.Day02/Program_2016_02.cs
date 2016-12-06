using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2016.sDay02
{
    class Program
    {
        static void Main(string[] args)
        {
            string input = File.ReadAllText("input.txt");
            //input = "ULL\n" + //#1 TEST: Should show 1985 #2: 5DB3
            //        "RRDDD\n" +
            //        "LURDL\n" +
            //        "UUUUD";

            string[,] keypad1 = new string[,] {
                { "1", "2", "3" },
                { "4", "5", "6" },
                { "7", "8", "9" }};

            string[,] keypad2 = new string[,] {
                { null, null, "1", null, null },
                { null, "2",  "3", "4",  null },
                { "5",  "6",  "7", "8",  "9"  },
                { null, "A",  "B", "C",  null },
                { null, null, "D", null, null }};

            string[] lines = input.Split('\n');

            string code1 = GetCode(keypad1, lines, 1, 1);
            string code2 = GetCode(keypad2, lines, 0, 2);

            Console.WriteLine("Code #1 is: " + code1);
            Console.WriteLine("Code #2 is: " + code2);
            Console.ReadKey();
        }

        private static string GetCode(string[,] keypad, string[] lines, int posX, int posY)
        {
            int keypadWidth = keypad.GetLength(1);
            int keypadHeight = keypad.GetLength(0);

            return lines
                .Aggregate("", (codeSoFar, line) =>
                {
                    line.ToList().ForEach(c =>
                    {
                        switch (c)
                        {
                            case 'U':
                                posY += posY >= 1 ? -1 : 0;
                                posY = keypad[posY, posX] != null ? posY : posY + 1;
                                break;
                            case 'R':
                                posX += posX < (keypadWidth - 1) ? 1 : 0;
                                posX = keypad[posY, posX] != null ? posX : posX - 1;
                                break;
                            case 'D':
                                posY += posY < (keypadHeight - 1) ? 1 : 0;
                                posY = keypad[posY, posX] != null ? posY : posY - 1;
                                break;
                            case 'L':
                                posX += posX >= 1 ? -1 : 0;
                                posX = keypad[posY, posX] != null ? posX : posX + 1;
                                break;
                        }
                    });

                    string nextCodeNumber = keypad[posY, posX];

                    return codeSoFar + nextCodeNumber;
                });
        }
    }
}
