using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2016.Day08
{
    class Program_2016_08
    {
        static void Main(string[] args)
        {
            string[] input = File.ReadAllLines("input.txt");
            bool[,] display = new bool[6, 50]; //height, width

            //Demo input, should result in: 
            //.#..#.#
            //#.#....
            //.#.....
            //display = new bool[3, 7];
            //input = new string[]
            //{
            //    "rect 3x2",
            //    "rotate column x=1 by 1",
            //    "rotate row y=0 by 4",
            //    "rotate column x=1 by 1"
            //};

            //IEnumerable<Instruction> instructions = input.Select(ins => CreateInstruction(ins));
            foreach(string line in input)
            {
                Console.WriteLine(line);

                Instruction ins = CreateInstruction(line);
                ins.Execute(ref display);
                RenderDisplay(display);
            }

            Console.WriteLine($"Pixels lit: {display.LitPixels()}");
            Console.ReadKey();
        }

        private static Instruction CreateInstruction(string instruction)
        {
            string[] parts = instruction.Split(' ');

            switch(parts[0])
            {
                case "rect":
                    string[] dimensionParts = parts[1].Split('x');
                    return new Rect(Int32.Parse(dimensionParts[0]), Int32.Parse(dimensionParts[1]));
                case "rotate":
                    switch(parts[1])
                    {
                        case "column":
                            return new RotateColumn(Int32.Parse(parts[2].Substring(2)), Int32.Parse(parts[4]));
                        case "row":
                            return new RotateRow(Int32.Parse(parts[2].Substring(2)), Int32.Parse(parts[4]));
                        default:
                            throw new Exception($"Invalid Rotate input for: {instruction}");
                    }
                default:
                    throw new Exception($"Invalid input for: {instruction}");
            }
        }

        private static void RenderDisplay(bool[,] display)
        {
            for(int y = 0; y < display.Height(); y++)
            {
                for (int x = 0; x < display.Width(); x++)
                {
                    Console.Write(display[y, x] ? 'X' : '.');
                }

                Console.WriteLine();
            }
        }
    }
    internal abstract class Instruction
    {
        internal abstract void Execute(ref bool[,] display);
    }

    internal class Rect : Instruction
    {
        private int width;
        private int height;

        internal Rect(int width, int height)
        {
            this.width = width;
            this.height = height;
        }

        internal override void Execute(ref bool[,] display)
        {
            for(int y = 0; y <= height; y++)
            {
                for(int x = 0; x <= width; x++)
                {
                    if(y < height && x < width)
                        display[y, x] = true;
                }
            }
        }
    }

    internal class RotateColumn : Instruction
    {
        private int columnPosToRotate;
        private int by;

        internal RotateColumn(int columnPosToRotate, int by)
        {
            this.columnPosToRotate = columnPosToRotate;
            this.by = by;
        }

        internal override void Execute(ref bool[,] display)
        {
            bool[,] newDisplay = new bool[display.GetLength(0), display.GetLength(1)];

            for (int y = 0; y < display.Height(); y++)
            {
                for (int x = 0; x < display.Width(); x++)
                {
                    if(x == columnPosToRotate)
                    {
                        newDisplay[(y + by) % display.Height(), x] = display[y, x];
                    }
                    else
                    {
                        newDisplay[y,x] = display[y,x];
                    }
                }
            }

            display = newDisplay;
        }
    }

    internal class RotateRow : Instruction
    {
        private int rowPosToRotate;
        private int by;

        internal RotateRow(int rowPosToRotate, int by)
        {
            this.rowPosToRotate = rowPosToRotate;
            this.by = by;
        }

        internal override void Execute(ref bool[,] display)
        {
            bool[,] newDisplay = new bool[display.GetLength(0), display.GetLength(1)];

            for (int y = 0; y < display.Height(); y++)
            {
                for (int x = 0; x < display.Width(); x++)
                {
                    if (y == rowPosToRotate)
                    {
                        newDisplay[y, (x + by) % display.Width()] = display[y, x];
                    }
                    else
                    {
                        newDisplay[y, x] = display[y, x];
                    }
                }
            }

            display = newDisplay;
        }
    }

    public static class ArrayExtension
    {
        public static int Height(this bool[,] array)
        {
            return array.GetLength(0);
        }

        public static int Width(this bool[,] array)
        {
            return array.GetLength(1);
        }

        public static int LitPixels(this bool[,] array)
        {
            int result = 0;

            for (int y = 0; y < array.Height(); y++)
            {
                for (int x = 0; x < array.Width(); x++)
                {
                    if (array[y, x])
                        result++;
                }
            }

            return result;
        }
    }
}
