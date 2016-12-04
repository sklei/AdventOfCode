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
            //input = "ULL\n" + //#1 TEST: Should show 1985
            //        "RRDDD\n" +
            //        "LURDL\n" +
            //        "UUUUD";

            int posX = 1;
            int posY = 1;
            string[,] keypad = new string[,] {
                { "1", "2", "3" },
                { "4", "5", "6" },
                { "7", "8", "9" }};

            string[] lines = input.Split('\n');

            int keypadWidth = keypad.GetLength(1);
            int keypadHeight = keypad.GetLength(0);

            string code = lines
                .Aggregate("", (codeSoFar, line) =>
                {
                    line.ToList().ForEach(c =>
                    {
                        switch(c)
                        {
                            case 'U':
                                posY += posY >= 1 ? -1 : 0; 
                                break;
                            case 'R':
                                posX += posX < (keypadWidth - 1) ? 1 : 0;
                                break;
                            case 'D':
                                posY += posY < (keypadHeight - 1) ? 1 : 0;
                                break;
                            case 'L':
                                posX += posX >= 1 ? -1 : 0;
                                break;
                        }
                    });

                    string nextCodeNumber = keypad[posY, posX];

                    return codeSoFar + nextCodeNumber;
                });

            Console.WriteLine("Code is: " + code);
            Console.ReadKey();
        }
    }
}
